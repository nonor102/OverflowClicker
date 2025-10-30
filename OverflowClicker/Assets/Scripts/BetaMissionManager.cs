using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BetaMissionManager : MonoBehaviour
{
    public static BetaMissionManager Instance { get; private set; }
    public int LatestCompletedMissionID { get; private set; } = 0; // �Ō�Ɋ��������~�b�V������ID (���� + 1��BF�l���ʂɏ�Z������)

    public List<BetaMission> allMissions; // �Q�[������Mission�A�Z�b�g��o�^
    public Dictionary<int, BetaMissionStatus> BetaMissionsStatuses { get; private set; } = new(); // �eMisiion�̏�Ԃ�ۑ����鎫��
    public bool IsBetaMission2NowExecuting { get; private set; } = false; // ���������ݎ��s�����ǂ���

    private double AFPerClickBeforeMission = 1; // �������s�O��AF�N���b�N�l���ʂ�ۑ����Ă����ϐ�
    private double AFMultiBeforeMission = 1; // �������s�O��AF�l���搔��ۑ����Ă����ϐ�
    private double BFBeforeMission = 0; // �������s�O��BF�̒l��ۑ����Ă����ϐ�
    private double BFMultiBeforeMission = 1; // �������s�O��BF�l���搔��ۑ����Ă����ϐ�

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);

            InitializeMissions();
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddBetaFactorMulti(LatestCompletedMissionID + 1); // �Z�[�u�f�[�^�ɂ͕ۑ����Ȃ��̂ŁA�����œK�p���Ă���
    }
    
    // Update is called once per frame
    void Update()
    {
        foreach(var mission in allMissions) // �����B���̔��������
        {
            if (BetaMissionsStatuses[mission.missionID] == BetaMissionStatus.Now)
            {
                switch (mission.missionID)
                {
                    case 1:
                        if (GameManager.Instance.IsAlpha2BetaExecuted)
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                            GameManager.Instance.ResetA2BExecutedFlag();
                        }
                        break;
                    case 2:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 15) // ��x�Ɋl������BF��15�ȏ�Ȃ�
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // �X�V
                        }
                            break;
                    case 3:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 1) // ��x�Ɋl������BF��1�ȏ�Ȃ�
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // �X�V
                        }
                        break;
                    case 4:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 500) // ��x�Ɋl������BF��500�ȏ�Ȃ�
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // �X�V
                        }
                        break;
                    case 5:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 1000) // ��x�Ɋl������BF��1000�ȏ�Ȃ�
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // �X�V
                        }
                        break;
                    case 6:
                        if (GameManager.Instance.IsAlpha2BetaExecuted)
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                            GameManager.Instance.ResetA2BExecutedFlag();
                        }
                        break;
                    case 7:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 1000) // ��x�Ɋl������BF��1000�ȏ�Ȃ�
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // �X�V
                        }
                        break;
                    case 8:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 1000) // ��x�Ɋl������BF��1000�ȏ�Ȃ�
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // �X�V
                        }
                        break;
                    case 9:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 10) // ��x�Ɋl������BF��10�ȏ�Ȃ�
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // �X�V
                        }
                        break;
                    case 10:
                        if (GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 500) // ��x�Ɋl������BF��500�ȏ�Ȃ�
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                            GameManager.Instance.ResetA2BExecutedFlag();
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }

    private void InitializeMissions()
    {
        foreach (var mission in allMissions)
        {
            if(mission.preRequiredMissions == null)
            {
                BetaMissionsStatuses[mission.missionID] = BetaMissionStatus.Available; // �O��������Ȃ��~�b�V�����͍ŏ����璧��\
                Debug.Log("Available Mission: " + mission.missionID);
            }
            else
            {
                BetaMissionsStatuses[mission.missionID] = BetaMissionStatus.Locked; // �������b�N
                Debug.Log("Locked Mission: " + mission.missionID);
            }
        }
    }

    public void SetMissionsFromSaveData(SaveData saveData) // �Z�[�u�f�[�^��������Ă���֐�
    {
        LatestCompletedMissionID = saveData.LatestCompletedMissionID;
        CompleteMissions(LatestCompletedMissionID);
    }

    private void CompleteMissions(int missionID) // �B������
    {
        if(missionID <= 0)
        {
            Debug.LogWarning("Mission doesnt exist");
            return;
        }

        LatestCompletedMissionID = missionID;
        Debug.Log("LatestCompletedMissionID: " + LatestCompletedMissionID);
        for (int i = 0; i < missionID; i++)
        {
            BetaMissionsStatuses[i + 1] = BetaMissionStatus.Completed; // �n���ꂽID�܂�Completed�ɂ��Ƃ� (�f�[�^�̃Z�[�u�ƌ݊�������������������)
            Debug.Log("Mission Completed: " +(i + 1));
        }

        if (BetaMissionsStatuses.ContainsKey(missionID + 1)) // ����No.1~9�܂�
        {
            if (BetaMissionsStatuses[missionID + 1] == BetaMissionStatus.Locked)
            {
                BetaMissionsStatuses[missionID + 1] = BetaMissionStatus.Available; // ���̂��Available��
                Debug.Log("Mission Available: " + (missionID + 1));
            }
        }
    }

    public BetaMissionStatus GetBetaMissionStatus(int missionID) // missionID�̏�Ԃ�Ԃ��֐�
    {
        if(missionID > 0)
        {
            return BetaMissionsStatuses[missionID];
        }
        else
        {
            return BetaMissionStatus.Locked;
        }
    }

    public void EnterOrExitBetaMission(int tryMissionID, bool enter) // �����ɒ��킷�鏈��
    {
        if (enter) // �����ɒ��킷��Ȃ�
        {
            AFPerClickBeforeMission = GameManager.Instance.AlphaFactorPerClick; // �������s�O��AF�N���b�N�l���ʂ�ۑ�
            AFMultiBeforeMission = GameManager.Instance.AlphaFactorMulti; // �������s�O��AF�l���搔��ۑ�
            BFMultiBeforeMission = GameManager.Instance.BetaFactorMulti; // �������s�O��BF�l���搔��ۑ�

            BetaMissionsStatuses[tryMissionID] = BetaMissionStatus.Now; // �n���ꂽ��𒧐풆�ɂ���
            Debug.Log("Now: " + tryMissionID);
            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // �������s�O��BF��ۑ�
            GameManager.Instance.ResetAlpha(); // Alpha�����Z�b�g
            switch (tryMissionID)
            {
                case 1:
                    GameManager.Instance.AlphaFactorPerClickDecreaser(100); // �N���b�N���Ƃɓ�����AF�̊l���ʂ�1%�ɐ���
                    break;
                case 2:
                    IsBetaMission2NowExecuting = true;
                    break;
                case 3:
                    GameManager.Instance.BetaFactorPerGainDecreaser(1000); // �l������BF�̊�{�̐���0.1%�ɐ���
                    break;
                case 4:
                    GameManager.Instance.AlphaFactorMultiDisabled(); // AF�l���搔�𖳌���
                    break;
                case 5:
                    BetaUpgradeManager.Instance.DisableFlagsAndMultiEtc(); // �����𖳌���
                    break;
                case 6:
                    GameManager.Instance.AlphaFactorPerClickDecreaser(1000); // AF�̃N���b�N�œ�����l���ʂ�0.1%�ɐ���
                    GameManager.Instance.AlphaFactorMultiDecreaser(1000); // AF�l���搔��0.1%�ɐ���
                    break;
                case 7:
                    GameManager.Instance.BetaFactorMultiDisabled(); // BF�l���搔��1�ɂ���
                    break;
                case 8:
                    GameManager.Instance.AlphaFactorMultiDisabled(); // AF�l���搔�𖳌���
                    BetaUpgradeManager.Instance.DisableFlagsAndMultiEtc(); // �����𖳌���
                    break;
                case 9:
                    GameManager.Instance.SetAlphaFactorExp(0.1); // AF�l���w����0.1�ɐ���
                    break;
                case 10:
                    GameManager.Instance.AlphaFactorGainDisabled(); // AF�l���ʂ�1�ɂ���
                    GameManager.Instance.AddBetaFactorExp(0.5); // BF�l���w����0.5�ɐ���
                    break;

                //�ȉ��ɒǋL���Ă���
                default:
                    break;
            }
        }
        else // ��������߂�Ȃ�
        {
            BetaMissionsStatuses[tryMissionID] = BetaMissionStatus.Available; // �n���ꂽ��𒧐�\��
            Debug.Log("Now -> Available: " + tryMissionID);
            BFBeforeMission = 0; // �������s�O��BF�����Z�b�g
            switch (tryMissionID)
            {
                case 1:
                    GameManager.Instance.AddAlphaFactorPerClick(100); // �N���b�N���Ƃɓ�����AF�̊l���ʂ����Ƃɂ��ǂ�
                    break;
                case 2:
                    IsBetaMission2NowExecuting = false;
                    break;
                case 3:
                    GameManager.Instance.AddBetaFactorPerGain(1000); // �l������BF�̊�{�̐������Ƃɂ��ǂ�
                    break;
                case 4:
                    GameManager.Instance.AddAlphaFactorMulti(AFMultiBeforeMission); // AF�l���搔�����Ƃɂ��ǂ�
                    break;
                case 5:
                    foreach(var upgrade in BetaUpgradeManager.Instance.allUpgrades)
                    {
                        if(BetaUpgradeManager.Instance.GetBetaUpgradeStatus(upgrade.upgradeID) == BetaUpgradeStatus.Completed)
                        {
                            BetaUpgradeManager.Instance.CompleteUpgrade(upgrade.upgradeID); // �����ς݂̋����̌��ʂ��ēK�p
                        }
                    }
                    break;
                case 6:
                    GameManager.Instance.AddAlphaFactorPerClick(1000); // AF�̃N���b�N�œ�����l���ʂ����Ƃɂ��ǂ�
                    GameManager.Instance.AddAlphaFactorMulti(1000); // AF�l���搔�����Ƃɂ��ǂ�
                    break;
                case 7:
                    GameManager.Instance.AddBetaFactorMulti(BFMultiBeforeMission); // BF�l���搔�����Ƃɂ��ǂ�
                    break;
                case 8:
                    GameManager.Instance.AddAlphaFactorMulti(AFMultiBeforeMission); // AF�l���搔�����Ƃɂ��ǂ�
                    foreach (var upgrade in BetaUpgradeManager.Instance.allUpgrades)
                    {
                        if (BetaUpgradeManager.Instance.GetBetaUpgradeStatus(upgrade.upgradeID) == BetaUpgradeStatus.Completed)
                        {
                            BetaUpgradeManager.Instance.CompleteUpgrade(upgrade.upgradeID); // �����ς݂̋����̌��ʂ��ēK�p
                        }
                    }
                    break;
                case 9:
                    GameManager.Instance.SetAlphaFactorExp(1); // AF�l���w�������Ƃɂ��ǂ�
                    break;
                case 10:
                    GameManager.Instance.AddAlphaFactorPerClick(AFPerClickBeforeMission); // AF�l���ʂ����Ƃɂ��ǂ�
                    GameManager.Instance.AddAlphaFactorMulti(AFMultiBeforeMission); // AF�l���搔�����Ƃɂ��ǂ�
                    GameManager.Instance.AddBetaFactorExp(2); // BF�l���w�������Ƃɂ��ǂ�
                    break;

                //�ȉ��ɒǋL���Ă���
                default:
                    break;
            }
        }
    }

    public void ResetAllMissions()
    {
        LatestCompletedMissionID = 0;
        InitializeMissions();
    }
}
