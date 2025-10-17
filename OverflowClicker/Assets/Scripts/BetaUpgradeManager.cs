using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class BetaUpgradeManager : MonoBehaviour
{
    public static BetaUpgradeManager Instance { get; private set; }

    public List<BetaUpgrade> allUpgrades; // �Q�[�����ɑ��݂���S�Ă̋����̃A�Z�b�g��o�^

    private Dictionary<string, BetaUpgradeStatus> BetaUpgradeStatuses = new Dictionary<string, BetaUpgradeStatus>(); // �e�����̌��݂̏�Ԃ�ۑ�����Dictionary

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeUpgrades();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeUpgrades() // �Q�[���J�n���ɋ����̏�Ԃ�����������
    {
        foreach (var upgrade in allUpgrades)
        {
            if (upgrade.preRequiredUpgrade == null || upgrade.preRequiredUpgrade.Count == 0)
            {
                BetaUpgradeStatuses[upgrade.upgradeID] = BetaUpgradeStatus.Available; // �O��������Ȃ������͍ŏ����狭���\
            }
            else
            {
                BetaUpgradeStatuses[upgrade.upgradeID] = BetaUpgradeStatus.Locked; // ����ȊO�͖��J�����
            }
        }
    }

    public BetaUpgradeStatus GetBetaUpgradeStatus(string upgradeID) // �w�肳�ꂽID�̋����̏�Ԃ��擾����
    {
        if (BetaUpgradeStatuses.ContainsKey(upgradeID))
        {
            return BetaUpgradeStatuses[upgradeID];
        }
        return BetaUpgradeStatus.Locked;
    }

    public void CompleteMission(string upgradeID) // ���������������鏈��
    {
        if (BetaUpgradeStatuses.ContainsKey(upgradeID))
        {
            BetaUpgradeStatuses[upgradeID] = BetaUpgradeStatus.Completed; // ��Ԃ�����ςɍX�V
            Debug.Log($"�������: {upgradeID}");

            UnlockNewUpgrades(); // ���̋����ɂ���ăA�����b�N�����V�����������m�F
        }
    }

    private void UnlockNewUpgrades() // �V�����������A�����b�N����
    {
        foreach (var upgrade in allUpgrades)
        {
            if (BetaUpgradeStatuses[upgrade.upgradeID] == BetaUpgradeStatus.Locked) // �܂����b�N��Ԃ̋����̂݃`�F�b�N
            {
                bool allPrerequisitesCompleted = upgrade.preRequiredUpgrade.All(p => // �O����������ׂĊ������Ă��邩�`�F�b�N
                    GetBetaUpgradeStatus(p.upgradeID) == BetaUpgradeStatus.Completed
                );

                if (allPrerequisitesCompleted)
                {
                    BetaUpgradeStatuses[upgrade.upgradeID] = BetaUpgradeStatus.Available; // �S�Ċ������Ă���Ή���\�ɂ���
                    Debug.Log($"�~�b�V�����J��: {upgrade.title}");
                }
            }
        }
    }
}