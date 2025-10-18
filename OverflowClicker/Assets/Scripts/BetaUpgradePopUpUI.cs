using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BetaUpgradePopUpUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button closeButton;

    void Awake()
    {
        // ����{�^�����N���b�N���ꂽ��AHide���\�b�h���Ă�
        closeButton.onClick.AddListener(Hide);
        // �ŏ��͔�\���ɂ��Ă���
        gameObject.SetActive(false);
    }

    // �p�l����\�����A���e���X�V���郁�\�b�h
    public void Show(BetaUpgrade upgrade)
    {
        if (BetaUpgradeManager.Instance.GetBetaUpgradeStatus(upgrade.upgradeID) == BetaUpgradeStatus.Available)
        {
            titleText.text = upgrade.title;
            descriptionText.text = upgrade.description;

            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = upgrade.needBetaFactor + "BF�ŋ���";

            // �ȑO�̃��X�i�[��S�č폜���Ă���V�������X�i�[��ǉ�����
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(() => BetaUpgrade(upgrade));

            gameObject.SetActive(true);
        }
        else if (BetaUpgradeManager.Instance.GetBetaUpgradeStatus(upgrade.upgradeID) == BetaUpgradeStatus.Completed)
        {
            upgradeButton.onClick.RemoveAllListeners();
            titleText.text = upgrade.title;
            descriptionText.text = upgrade.description;
            gameObject.SetActive(true);
            Debug.LogWarning("Upgrade" + upgrade.upgradeID + " is Completed");
        }
        else
        {
            upgradeButton.onClick.RemoveAllListeners();
            Debug.LogWarning("Upgrade" + upgrade.upgradeID + " is Locked");
        }
    }

    // �p�l�����\���ɂ��郁�\�b�h
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void BetaUpgrade(BetaUpgrade upgrade)
    {
        if (GameManager.Instance.BetaFactorForDisplay >= upgrade.needBetaFactor)
        {
            GameManager.Instance.SubBetaFactor(upgrade.needBetaFactor);
            BetaUpgradeManager.Instance.CompleteUpgrade(upgrade.upgradeID);
            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "�����ς�";
        }
        else
        {
            if(BetaUpgradeManager.Instance.GetBetaUpgradeStatus(upgrade.upgradeID) == BetaUpgradeStatus.Available) // Available�̂Ƃ��ɂ����x�������o���Ă�
            {
                descriptionText.text = upgrade.description + "\n" + "BF������܂���!";
            }
        }
    }
}