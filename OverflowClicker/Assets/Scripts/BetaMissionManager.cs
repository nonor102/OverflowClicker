using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BetaMissionManager : MonoBehaviour
{
    public static BetaMissionManager Instance { get; private set; }
    public int LatestCompletedMissionID { get; private set; } = 0; // 最後に完了したミッションのID (これ + 1をBF獲得量に乗算したい)

    public List<BetaMission> allMissions; // ゲーム内のMissionアセットを登録
    public Dictionary<int, BetaMissionStatus> BetaMissionsStatuses { get; private set; } = new(); // 各Misiionの状態を保存する辞書

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
            BetaMissionsStatuses[i] = BetaMissionStatus.Completed; // 渡されたIDまでCompletedにしとく (データのセーブと互換性を持たせたいだけ)
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
            BetaMissionsStatuses[tryMissionID] = BetaMissionStatus.Now; // 渡されたやつを挑戦中にする
            Debug.Log("Now: " + tryMissionID);
            switch (tryMissionID)
            {
                case 1:
                    GameManager.Instance.ResetAlpha();
                    GameManager.Instance.AddAlphaFactorPerClick(0.01); // クリックごとに得られるAFの獲得量を1%に制限
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
            switch (tryMissionID)
            {
                case 1:
                    GameManager.Instance.AddAlphaFactorPerClick(100); // クリックごとに得られるAFの獲得量をもとにもどす
                    break;

                //以下に追記していく
                default:
                    break;
            }
        }
    }
}
