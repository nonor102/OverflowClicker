using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class BetaUpgradeManager : MonoBehaviour
{
    public static BetaUpgradeManager Instance { get; private set; }

    public bool IsUpgrade0Completed { get; private set; } = false; // �v�Z���̕\���Ƃ��Ŏg���t���O
    public bool IsUpgrade5Completed { get; private set; } = false; // AF�擾�̎��������������Ă邩�t���O
    public bool IsUpgrade6Completed { get; private set; } = false; // Alpha��Collapse���Ă邩�t���O

    public List<BetaUpgrade> allUpgrades; // �Q�[�����ɑ��݂���S�Ă̋����̃A�Z�b�g��o�^

    public Dictionary<int, BetaUpgradeStatus> BetaUpgradeStatuses { get; private set; } = new Dictionary<int, BetaUpgradeStatus>(); // �e�����̌��݂̏�Ԃ�ۑ�����Dictionary

    private void Awake()
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
                Debug.Log("Available: " +  upgrade.upgradeID);
            }
            else
            {
                BetaUpgradeStatuses[upgrade.upgradeID] = BetaUpgradeStatus.Locked; // ����ȊO�͖��J�����
                Debug.Log("Locked: " + upgrade.upgradeID);
            }
        }
    }

    public void InitializeFromSaveData(List<int> completedIDs)
    {
        if (completedIDs == null) return;

        // ���[�h���������ς�ID�̃��X�g���g���āA���ʂ��ēK�p���Ă���
        foreach (int id in completedIDs)
        {
            // ���ʓK�p�Ə�ԍX�V�𓯎��ɍs��CompleteUpgrade���Ăяo��
            CompleteUpgrade(id);
        }

        // �S�Ă̌��ʂ�K�p��A�A�����b�N��Ԃ��Čv�Z����
        UnlockNewUpgrades();

        Debug.Log("BetaUpgrade data initialized from save file.");
    }

    public BetaUpgradeStatus GetBetaUpgradeStatus(int upgradeID) // �w�肳�ꂽID�̋����̏�Ԃ��擾����
    {
        if (BetaUpgradeStatuses.ContainsKey(upgradeID))
        {
            return BetaUpgradeStatuses[upgradeID];
        }
        return BetaUpgradeStatus.Locked;
    }

    public void CompleteUpgrade(int upgradeID) // ���������������鏈��
    {
        if (BetaUpgradeStatuses.ContainsKey(upgradeID))
        {
            switch(upgradeID)
            {
                case 0:
                    GameManager.Instance.AddAlphaFactorPerClick(1.1); // AF�l����1.1�{
                    IsUpgrade0Completed = true;
                    break;
                case 1:
                    GameManager.Instance.AddAlphaFactorPerClick(1.2); // AF�l����1.2�{
                    break;
                case 2:
                    GameManager.Instance.AddBetaFactorPerGain(1.2); // BF�l����1.2�{
                    break;
                case 3:
                    GameManager.Instance.AddBetaFactorPerGain(1.5); // BF�l����1.5�{
                    break;
                case 4:
                    GameManager.Instance.AddAlphaFactorPerClick(5); // AF�l����5�{
                    break;
                case 5:
                    IsUpgrade5Completed = true;
                    break;
                case 6:
                    IsUpgrade6Completed = true;
                    GameManager.Instance.CollapseAlpha();
                    break;

                // �ǂ�ǂ�ǉ����Ă���...
                default:
                    break;
            }
            BetaUpgradeStatuses[upgradeID] = BetaUpgradeStatus.Completed; // ��Ԃ�����ςɍX�V
            Debug.Log(BetaUpgradeStatuses[upgradeID]);
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
                    Debug.Log($"�����J��: {upgrade.upgradeID}");
                }
            }
        }
    }
}