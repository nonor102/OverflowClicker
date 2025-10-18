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
        // 閉じるボタンがクリックされたら、Hideメソッドを呼ぶ
        closeButton.onClick.AddListener(Hide);
        // 最初は非表示にしておく
        gameObject.SetActive(false);
    }

    // パネルを表示し、内容を更新するメソッド
    public void Show(BetaUpgrade upgrade)
    {
        if (BetaUpgradeManager.Instance.GetBetaUpgradeStatus(upgrade.upgradeID) == BetaUpgradeStatus.Available)
        {
            titleText.text = upgrade.title;
            descriptionText.text = upgrade.description;

            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = upgrade.needBetaFactor + "BFで強化";

            // 以前のリスナーを全て削除してから新しいリスナーを追加する
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

    // パネルを非表示にするメソッド
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
            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "強化済み";
        }
        else
        {
            if(BetaUpgradeManager.Instance.GetBetaUpgradeStatus(upgrade.upgradeID) == BetaUpgradeStatus.Available) // Availableのときにだけ警告文を出してる
            {
                descriptionText.text = upgrade.description + "\n" + "BFが足りません!";
            }
        }
    }
}