using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BetaMissionManager : MonoBehaviour
{
    public static BetaMissionManager Instance { get; private set; }
    public int LatestCompletedMissionID { get; private set; } = 0; // �Ō�Ɋ��������~�b�V������ID (���� + 1��BF�l���ʂɏ�Z������)

    public List<BetaMission> allMissions; // �Q�[������Mission�A�Z�b�g��o�^
    public Dictionary<int, BetaMissionStatus> BetaMissionsStatuses { get; private set; } = new(); // �eMisiion�̏�Ԃ�ۑ����鎫��

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
            BetaMissionsStatuses[i] = BetaMissionStatus.Completed; // �n���ꂽID�܂�Completed�ɂ��Ƃ� (�f�[�^�̃Z�[�u�ƌ݊�������������������)
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
            BetaMissionsStatuses[tryMissionID] = BetaMissionStatus.Now; // �n���ꂽ��𒧐풆�ɂ���
            Debug.Log("Now: " + tryMissionID);
            switch (tryMissionID)
            {
                case 1:
                    GameManager.Instance.ResetAlpha();
                    GameManager.Instance.AddAlphaFactorPerClick(0.01); // �N���b�N���Ƃɓ�����AF�̊l���ʂ�1%�ɐ���
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
            switch (tryMissionID)
            {
                case 1:
                    GameManager.Instance.AddAlphaFactorPerClick(100); // �N���b�N���Ƃɓ�����AF�̊l���ʂ����Ƃɂ��ǂ�
                    break;

                //�ȉ��ɒǋL���Ă���
                default:
                    break;
            }
        }
    }
}
