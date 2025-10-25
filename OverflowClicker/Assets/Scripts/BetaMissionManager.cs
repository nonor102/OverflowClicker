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
    public Dictionary<int, BetaMissionStatus> BetaMissionsStatuses { get; private set; } = new Dictionary<int, BetaMissionStatus>(); // 各Misiionの状態を保存する辞書

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

    }

    private void InitializeMissions()
    {
        foreach (var mission in allMissions)
        {
            if(mission.preRequiredMissions != null)
            {
                BetaMissionsStatuses[mission.missionID] = BetaMissionStatus.Available; // 前提条件がないミッションは最初から挑戦可能
                Debug.Log("Available: " + mission.missionID);
            }
            else
            {
                BetaMissionsStatuses[mission.missionID] = BetaMissionStatus.Locked; // 他をロック
                Debug.Log("Locked: " + mission.missionID);
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
        if(missionID > 0)
        {
            LatestCompletedMissionID = missionID;
            Debug.Log("LatestCompletedMissionID: " + LatestCompletedMissionID);
            for (int i = 0; i < missionID; i++)
            {
                BetaMissionsStatuses[missionID] = BetaMissionStatus.Completed; // 渡されたIDまでCompletedにしとく (データのセーブと互換性を持たせたいだけ)
            }
        }
        BetaMissionsStatuses[missionID + 1] = BetaMissionStatus.Available; // 次のやつをAvailableに
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

    //TODO 各ミッションの内容と報酬とか処理する関数をつくる
}
