using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BetaUpgradePopUpUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
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
        titleText.text = upgrade.title;
        descriptionText.text = upgrade.description;
        gameObject.SetActive(true);
    }

    // �p�l�����\���ɂ��郁�\�b�h
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}