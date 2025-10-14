using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class BetaUpgradeManager : MonoBehaviour
{
    public static BetaUpgradeManager Instance { get; private set; }

    public List<BetaUpgrade> allMissions; // �Q�[�����ɑ��݂���S�Ă̋����̃A�Z�b�g��o�^

    private Dictionary<string, BetaUpgradeStatus> BetaUpgradeStatuses = new Dictionary<string, BetaUpgradeStatus>(); // �e�����̌��݂̏�Ԃ�ۑ�����Dictionary

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeMissions();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeMissions() // �Q�[���J�n���ɋ����̏�Ԃ�����������
    {
        foreach (var mission in allMissions)
        {
            if (mission.preRequiredUpgrade == null || mission.preRequiredUpgrade.Count == 0)
            {
                BetaUpgradeStatuses[mission.missionID] = BetaUpgradeStatus.Available; // �O��������Ȃ������͍ŏ����狭���\
            }
            else
            {
                BetaUpgradeStatuses[mission.missionID] = BetaUpgradeStatus.Locked; // ����ȊO�͖��J�����
            }
        }
    }

    public BetaUpgradeStatus GetBetaUpgradeStatus(string missionID) // �w�肳�ꂽID�̋����̏�Ԃ��擾����
    {
        if (BetaUpgradeStatuses.ContainsKey(missionID))
        {
            return BetaUpgradeStatuses[missionID];
        }
        return BetaUpgradeStatus.Locked;
    }

    public void CompleteMission(string missionID) // ���������������鏈��
    {
        if (BetaUpgradeStatuses.ContainsKey(missionID))
        {
            BetaUpgradeStatuses[missionID] = BetaUpgradeStatus.Completed; // ��Ԃ�����ςɍX�V
            Debug.Log($"�������: {missionID}");

            UnlockNewUpgrades(); // ���̋����ɂ���ăA�����b�N�����V�����������m�F
        }
    }

    private void UnlockNewUpgrades() // �V�����������A�����b�N����
    {
        foreach (var mission in allMissions)
        {
            if (BetaUpgradeStatuses[mission.missionID] == BetaUpgradeStatus.Locked) // �܂����b�N��Ԃ̋����̂݃`�F�b�N
            {
                bool allPrerequisitesCompleted = mission.preRequiredUpgrade.All(p => // �O����������ׂĊ������Ă��邩�`�F�b�N
                    GetBetaUpgradeStatus(p.missionID) == BetaUpgradeStatus.Completed
                );

                if (allPrerequisitesCompleted)
                {
                    BetaUpgradeStatuses[mission.missionID] = BetaUpgradeStatus.Available; // �S�Ċ������Ă���Ή���\�ɂ���
                    Debug.Log($"�~�b�V�����J��: {mission.title}");
                }
            }
        }
    }
}