using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BetaMissionManager : MonoBehaviour
{
    public static BetaMissionManager Instance { get; private set; }
    public int LatestCompletedMissionID { get; private set; } = 0; // 最後に完了したミッションのID (これ + 1をBF獲得量に乗算したい)

    public List<BetaMission> allMissions; // ゲーム内のMissionアセットを登録
    public Dictionary<int, BetaMissionStatus> BetaMissionsStatuses { get; private set; } = new(); // 各Misiionの状態を保存する辞書
    public bool IsBetaMission2NowExecuting { get; private set; } = false; // 試練が現在実行中かどうか

    private double AFPerClickBeforeMission = 1; // 試練実行前のAFクリック獲得量を保存しておく変数
    private double AFMultiBeforeMission = 1; // 試練実行前のAF獲得乗数を保存しておく変数
    private double BFBeforeMission = 0; // 試練実行前のBFの値を保存しておく変数
    private double BFMultiBeforeMission = 1; // 試練実行前のBF獲得乗数を保存しておく変数

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
        GameManager.Instance.AddBetaFactorMulti(LatestCompletedMissionID + 1); // セーブデータには保存しないので、ここで適用しておく
    }
    
    // Update is called once per frame
    void Update()
    {
        foreach(var mission in allMissions) // 試練達成の判定をする
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
                            GameManager.Instance.ResetA2BExecutedFlag();
                        }
                        break;
                    case 2:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 15) // 一度に獲得したBFが15以上なら
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // 更新
                        }
                            break;
                    case 3:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 1) // 一度に獲得したBFが1以上なら
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // 更新
                        }
                        break;
                    case 4:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 500) // 一度に獲得したBFが500以上なら
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // 更新
                        }
                        break;
                    case 5:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 1000) // 一度に獲得したBFが1000以上なら
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // 更新
                        }
                        break;
                    case 6:
                        if (GameManager.Instance.IsAlpha2BetaExecuted)
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                            GameManager.Instance.ResetA2BExecutedFlag();
                        }
                        break;
                    case 7:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 1000) // 一度に獲得したBFが1000以上なら
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // 更新
                        }
                        break;
                    case 8:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 1000) // 一度に獲得したBFが1000以上なら
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // 更新
                        }
                        break;
                    case 9:
                        if(GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 10) // 一度に獲得したBFが10以上なら
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                        }
                        else
                        {
                            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // 更新
                        }
                        break;
                    case 10:
                        if (GameManager.Instance.BetaFactorForCalc - BFBeforeMission >= 500) // 一度に獲得したBFが500以上なら
                        {
                            EnterOrExitBetaMission(mission.missionID, false);
                            CompleteMissions(mission.missionID);
                            GameManager.Instance.ResetA2BExecutedFlag();
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
                BetaMissionsStatuses[mission.missionID] = BetaMissionStatus.Available; // 前提条件がないミッションは最初から挑戦可能
                Debug.Log("Available Mission: " + mission.missionID);
            }
            else
            {
                BetaMissionsStatuses[mission.missionID] = BetaMissionStatus.Locked; // 他をロック
                Debug.Log("Locked Mission: " + mission.missionID);
            }
        }
    }

    public void SetMissionsFromSaveData(SaveData saveData) // セーブデータからもってくる関数
    {
        LatestCompletedMissionID = saveData.LatestCompletedMissionID;
        CompleteMissions(LatestCompletedMissionID);
    }

    private void CompleteMissions(int missionID) // 達成処理
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
            BetaMissionsStatuses[i + 1] = BetaMissionStatus.Completed; // 渡されたIDまでCompletedにしとく (データのセーブと互換性を持たせたいだけ)
            Debug.Log("Mission Completed: " +(i + 1));
        }

        if (BetaMissionsStatuses.ContainsKey(missionID + 1)) // 試練No.1~9まで
        {
            if (BetaMissionsStatuses[missionID + 1] == BetaMissionStatus.Locked)
            {
                BetaMissionsStatuses[missionID + 1] = BetaMissionStatus.Available; // 次のやつをAvailableに
                Debug.Log("Mission Available: " + (missionID + 1));
            }
        }
    }

    public BetaMissionStatus GetBetaMissionStatus(int missionID) // missionIDの状態を返す関数
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

    public void EnterOrExitBetaMission(int tryMissionID, bool enter) // 試練に挑戦する処理
    {
        if (enter) // 試練に挑戦するなら
        {
            AFPerClickBeforeMission = GameManager.Instance.AlphaFactorPerClick; // 試練実行前のAFクリック獲得量を保存
            AFMultiBeforeMission = GameManager.Instance.AlphaFactorMulti; // 試練実行前のAF獲得乗数を保存
            BFMultiBeforeMission = GameManager.Instance.BetaFactorMulti; // 試練実行前のBF獲得乗数を保存

            BetaMissionsStatuses[tryMissionID] = BetaMissionStatus.Now; // 渡されたやつを挑戦中にする
            Debug.Log("Now: " + tryMissionID);
            BFBeforeMission = GameManager.Instance.BetaFactorForCalc; // 試練実行前のBFを保存
            GameManager.Instance.ResetAlpha(); // Alphaをリセット
            switch (tryMissionID)
            {
                case 1:
                    GameManager.Instance.AlphaFactorPerClickDecreaser(100); // クリックごとに得られるAFの獲得量を1%に制限
                    break;
                case 2:
                    IsBetaMission2NowExecuting = true;
                    break;
                case 3:
                    GameManager.Instance.BetaFactorPerGainDecreaser(1000); // 獲得するBFの基本の数を0.1%に制限
                    break;
                case 4:
                    GameManager.Instance.AlphaFactorMultiDisabled(); // AF獲得乗数を無効化
                    break;
                case 5:
                    BetaUpgradeManager.Instance.DisableFlagsAndMultiEtc(); // 強化を無効化
                    break;
                case 6:
                    GameManager.Instance.AlphaFactorPerClickDecreaser(1000); // AFのクリックで得られる獲得量を0.1%に制限
                    GameManager.Instance.AlphaFactorMultiDecreaser(1000); // AF獲得乗数を0.1%に制限
                    break;
                case 7:
                    GameManager.Instance.BetaFactorMultiDisabled(); // BF獲得乗数を1にする
                    break;
                case 8:
                    GameManager.Instance.AlphaFactorMultiDisabled(); // AF獲得乗数を無効化
                    BetaUpgradeManager.Instance.DisableFlagsAndMultiEtc(); // 強化を無効化
                    break;
                case 9:
                    GameManager.Instance.SetAlphaFactorExp(0.1); // AF獲得指数を0.1に制限
                    break;
                case 10:
                    GameManager.Instance.AlphaFactorGainDisabled(); // AF獲得量を1にする
                    GameManager.Instance.AddBetaFactorExp(0.5); // BF獲得指数を0.5に制限
                    break;

                //以下に追記していく
                default:
                    break;
            }
        }
        else // 試練をやめるなら
        {
            BetaMissionsStatuses[tryMissionID] = BetaMissionStatus.Available; // 渡されたやつを挑戦可能に
            Debug.Log("Now -> Available: " + tryMissionID);
            BFBeforeMission = 0; // 試練実行前のBFをリセット
            switch (tryMissionID)
            {
                case 1:
                    GameManager.Instance.AddAlphaFactorPerClick(100); // クリックごとに得られるAFの獲得量をもとにもどす
                    break;
                case 2:
                    IsBetaMission2NowExecuting = false;
                    break;
                case 3:
                    GameManager.Instance.AddBetaFactorPerGain(1000); // 獲得するBFの基本の数をもとにもどす
                    break;
                case 4:
                    GameManager.Instance.AddAlphaFactorMulti(AFMultiBeforeMission); // AF獲得乗数をもとにもどす
                    break;
                case 5:
                    foreach(var upgrade in BetaUpgradeManager.Instance.allUpgrades)
                    {
                        if(BetaUpgradeManager.Instance.GetBetaUpgradeStatus(upgrade.upgradeID) == BetaUpgradeStatus.Completed)
                        {
                            BetaUpgradeManager.Instance.CompleteUpgrade(upgrade.upgradeID); // 完了済みの強化の効果を再適用
                        }
                    }
                    break;
                case 6:
                    GameManager.Instance.AddAlphaFactorPerClick(1000); // AFのクリックで得られる獲得量をもとにもどす
                    GameManager.Instance.AddAlphaFactorMulti(1000); // AF獲得乗数をもとにもどす
                    break;
                case 7:
                    GameManager.Instance.AddBetaFactorMulti(BFMultiBeforeMission); // BF獲得乗数をもとにもどす
                    break;
                case 8:
                    GameManager.Instance.AddAlphaFactorMulti(AFMultiBeforeMission); // AF獲得乗数をもとにもどす
                    foreach (var upgrade in BetaUpgradeManager.Instance.allUpgrades)
                    {
                        if (BetaUpgradeManager.Instance.GetBetaUpgradeStatus(upgrade.upgradeID) == BetaUpgradeStatus.Completed)
                        {
                            BetaUpgradeManager.Instance.CompleteUpgrade(upgrade.upgradeID); // 完了済みの強化の効果を再適用
                        }
                    }
                    break;
                case 9:
                    GameManager.Instance.SetAlphaFactorExp(1); // AF獲得指数をもとにもどす
                    break;
                case 10:
                    GameManager.Instance.AddAlphaFactorPerClick(AFPerClickBeforeMission); // AF獲得量をもとにもどす
                    GameManager.Instance.AddAlphaFactorMulti(AFMultiBeforeMission); // AF獲得乗数をもとにもどす
                    GameManager.Instance.AddBetaFactorExp(2); // BF獲得指数をもとにもどす
                    break;

                //以下に追記していく
                default:
                    break;
            }
        }
    }

    public void ResetAllMissions()
    {
        LatestCompletedMissionID = 0;
        InitializeMissions();
    }
}
