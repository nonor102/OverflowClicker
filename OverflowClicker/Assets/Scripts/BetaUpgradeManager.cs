using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class BetaUpgradeManager : MonoBehaviour
{
    public static BetaUpgradeManager Instance { get; private set; }

    public List<BetaUpgrade> allMissions; // ゲーム内に存在する全ての強化のアセットを登録

    private Dictionary<string, BetaUpgradeStatus> BetaUpgradeStatuses = new Dictionary<string, BetaUpgradeStatus>(); // 各強化の現在の状態を保存するDictionary

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

    private void InitializeMissions() // ゲーム開始時に強化の状態を初期化する
    {
        foreach (var mission in allMissions)
        {
            if (mission.preRequiredUpgrade == null || mission.preRequiredUpgrade.Count == 0)
            {
                BetaUpgradeStatuses[mission.missionID] = BetaUpgradeStatus.Available; // 前提条件がない強化は最初から強化可能
            }
            else
            {
                BetaUpgradeStatuses[mission.missionID] = BetaUpgradeStatus.Locked; // それ以外は未開放状態
            }
        }
    }

    public BetaUpgradeStatus GetBetaUpgradeStatus(string missionID) // 指定されたIDの強化の状態を取得する
    {
        if (BetaUpgradeStatuses.ContainsKey(missionID))
        {
            return BetaUpgradeStatuses[missionID];
        }
        return BetaUpgradeStatus.Locked;
    }

    public void CompleteMission(string missionID) // 強化を完了させる処理
    {
        if (BetaUpgradeStatuses.ContainsKey(missionID))
        {
            BetaUpgradeStatuses[missionID] = BetaUpgradeStatus.Completed; // 状態を解放済に更新
            Debug.Log($"解放完了: {missionID}");

            UnlockNewUpgrades(); // この強化によってアンロックされる新しい強化を確認
        }
    }

    private void UnlockNewUpgrades() // 新しい強化をアンロックする
    {
        foreach (var mission in allMissions)
        {
            if (BetaUpgradeStatuses[mission.missionID] == BetaUpgradeStatus.Locked) // まだロック状態の強化のみチェック
            {
                bool allPrerequisitesCompleted = mission.preRequiredUpgrade.All(p => // 前提条件がすべて完了しているかチェック
                    GetBetaUpgradeStatus(p.missionID) == BetaUpgradeStatus.Completed
                );

                if (allPrerequisitesCompleted)
                {
                    BetaUpgradeStatuses[mission.missionID] = BetaUpgradeStatus.Available; // 全て完了していれば解放可能にする
                    Debug.Log($"ミッション開放: {mission.title}");
                }
            }
        }
    }
}