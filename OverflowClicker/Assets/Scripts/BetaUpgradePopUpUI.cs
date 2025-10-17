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
        // 閉じるボタンがクリックされたら、Hideメソッドを呼ぶ
        closeButton.onClick.AddListener(Hide);
        // 最初は非表示にしておく
        gameObject.SetActive(false);
    }

    // パネルを表示し、内容を更新するメソッド
    public void Show(BetaUpgrade upgrade)
    {
        titleText.text = upgrade.title;
        descriptionText.text = upgrade.description;
        gameObject.SetActive(true);
    }

    // パネルを非表示にするメソッド
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}