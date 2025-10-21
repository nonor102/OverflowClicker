using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class BetaUpgradeManager : MonoBehaviour
{
    public static BetaUpgradeManager Instance { get; private set; }

    public bool IsUpgrade0Completed { get; private set; } = false; // 計算式の表示とかで使うフラグ
    public bool IsUpgrade5Completed { get; private set; } = false; // AF取得の自動化が解放されてるかフラグ
    public bool IsUpgrade6Completed { get; private set; } = false; // AlphaがCollapseしてるかフラグ

    public List<BetaUpgrade> allUpgrades; // ゲーム内に存在する全ての強化のアセットを登録

    public Dictionary<int, BetaUpgradeStatus> BetaUpgradeStatuses { get; private set; } = new Dictionary<int, BetaUpgradeStatus>(); // 各強化の現在の状態を保存するDictionary

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

    private void InitializeUpgrades() // ゲーム開始時に強化の状態を初期化する
    {
        foreach (var upgrade in allUpgrades)
        {
            if (upgrade.preRequiredUpgrade == null || upgrade.preRequiredUpgrade.Count == 0)
            {
                BetaUpgradeStatuses[upgrade.upgradeID] = BetaUpgradeStatus.Available; // 前提条件がない強化は最初から強化可能
                Debug.Log("Available: " +  upgrade.upgradeID);
            }
            else
            {
                BetaUpgradeStatuses[upgrade.upgradeID] = BetaUpgradeStatus.Locked; // それ以外は未開放状態
                Debug.Log("Locked: " + upgrade.upgradeID);
            }
        }
    }

    public void InitializeFromSaveData(List<int> completedIDs)
    {
        if (completedIDs == null) return;

        // ロードした完了済みIDのリストを使って、効果を再適用していく
        foreach (int id in completedIDs)
        {
            // 効果適用と状態更新を同時に行うCompleteUpgradeを呼び出す
            CompleteUpgrade(id);
        }

        // 全ての効果を適用後、アンロック状態を再計算する
        UnlockNewUpgrades();

        Debug.Log("BetaUpgrade data initialized from save file.");
    }

    public BetaUpgradeStatus GetBetaUpgradeStatus(int upgradeID) // 指定されたIDの強化の状態を取得する
    {
        if (BetaUpgradeStatuses.ContainsKey(upgradeID))
        {
            return BetaUpgradeStatuses[upgradeID];
        }
        return BetaUpgradeStatus.Locked;
    }

    public void CompleteUpgrade(int upgradeID) // 強化を完了させる処理
    {
        if (BetaUpgradeStatuses.ContainsKey(upgradeID))
        {
            switch(upgradeID)
            {
                case 0:
                    GameManager.Instance.AddAlphaFactorPerClick(1.1); // AF獲得量1.1倍
                    IsUpgrade0Completed = true;
                    break;
                case 1:
                    GameManager.Instance.AddAlphaFactorPerClick(1.2); // AF獲得量1.2倍
                    break;
                case 2:
                    GameManager.Instance.AddBetaFactorPerGain(1.2); // BF獲得量1.2倍
                    break;
                case 3:
                    GameManager.Instance.AddBetaFactorPerGain(1.5); // BF獲得量1.5倍
                    break;
                case 4:
                    GameManager.Instance.AddAlphaFactorPerClick(5); // AF獲得量5倍
                    break;
                case 5:
                    IsUpgrade5Completed = true;
                    break;
                case 6:
                    IsUpgrade6Completed = true;
                    GameManager.Instance.CollapseAlpha();
                    break;

                // どんどん追加していく...
                default:
                    break;
            }
            BetaUpgradeStatuses[upgradeID] = BetaUpgradeStatus.Completed; // 状態を解放済に更新
            Debug.Log(BetaUpgradeStatuses[upgradeID]);
            Debug.Log($"解放完了: {upgradeID}");

            UnlockNewUpgrades(); // この強化によってアンロックされる新しい強化を確認
        }
    }

    private void UnlockNewUpgrades() // 新しい強化をアンロックする
    {
        foreach (var upgrade in allUpgrades)
        {
            if (BetaUpgradeStatuses[upgrade.upgradeID] == BetaUpgradeStatus.Locked) // まだロック状態の強化のみチェック
            {
                bool allPrerequisitesCompleted = upgrade.preRequiredUpgrade.All(p => // 前提条件がすべて完了しているかチェック
                    GetBetaUpgradeStatus(p.upgradeID) == BetaUpgradeStatus.Completed
                );

                if (allPrerequisitesCompleted)
                {
                    BetaUpgradeStatuses[upgrade.upgradeID] = BetaUpgradeStatus.Available; // 全て完了していれば解放可能にする
                    Debug.Log($"強化開放: {upgrade.upgradeID}");
                }
            }
        }
    }
}