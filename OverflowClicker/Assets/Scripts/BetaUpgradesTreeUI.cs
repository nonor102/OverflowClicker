using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class BetaUpgradeTreeUI : MonoBehaviour
{
    [SerializeField] private GameObject betaUpgradeNodePrefab; // 強化表示用UIのプレハブ
    [SerializeField] private Transform container;          // UIを生成する親オブジェクト

    private Dictionary<string, Button> missionNodeObjects = new Dictionary<string, Button>();

    void Start()
    {
        GenerateMissionTree();
    }

    void Update()
    {
        UpdateMissionNodes();
    }

    void GenerateMissionTree() // 強化ツリーをUI上に生成する
    {
        foreach (var mission in BetaUpgradeManager.Instance.allMissions) // 全てのミッションに対してUIノードを生成
        {
            GameObject nodeObj = Instantiate(betaUpgradeNodePrefab, container);
            nodeObj.GetComponentInChildren<Text>().text = mission.title;
            // TODO: ここでノードの座標を設定する。手動か、自動レイアウトアルゴリズムを使う。
            // nodeObj.transform.localPosition = new Vector3( ... );

            Button nodeButton = nodeObj.GetComponent<Button>();
            string missionID = mission.missionID; // クロージャのためにローカル変数にコピー
            nodeButton.onClick.AddListener(() => OnMissionNodeClicked(missionID));

            missionNodeObjects[mission.missionID] = nodeButton;
        }

        // TODO: ここでミッションノード間を線で結ぶ処理を実装
    }

    void UpdateMissionNodes() // 定期的にUIの状態を更新する
    {
        foreach (var mission in BetaUpgradeManager.Instance.allMissions)
        {
            Button button = missionNodeObjects[mission.missionID];
            BetaUpgradeStatus status = BetaUpgradeManager.Instance.GetBetaUpgradeStatus(mission.missionID);

            switch (status) // 状態に応じてボタンの色やインタラクションを変更
            {
                case BetaUpgradeStatus.Locked:
                    button.interactable = false;
                    button.GetComponent<Image>().color = Color.gray;
                    break;
                case BetaUpgradeStatus.Available:
                    button.interactable = true;
                    button.GetComponent<Image>().color = Color.white;
                    break;
                case BetaUpgradeStatus.Completed:
                    button.interactable = false;
                    button.GetComponent<Image>().color = Color.green;
                    break;
            }
        }
    }

    void OnMissionNodeClicked(string missionID) // 強化ノードがクリックされたときの処理
    {
        Debug.Log($"クリックされたミッション: {missionID}");
        // ここでミッション詳細画面を開いたり、ミッションを開始する処理を呼び出す
        // 例：BetaUpgradeManager.Instance.StartMission(missionID);
    }
}