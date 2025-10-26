using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BetaMissionUI : MonoBehaviour
{
    [SerializeField] private List<Button> MissionList = new(); // 試練のボタンいれとく
    [SerializeField] private List<TextMeshProUGUI> MissionTitles = new(); // 試練のタイトルに追記するテキスト用のtmpいれる
    [SerializeField] private List<TextMeshProUGUI> MissionDescriptions = new(); // 試練の説明用のtmpいれる
    [SerializeField] private TextMeshProUGUI BetaMissionDescriptionText; // 試練画面の説明テキスト
    [SerializeField] private Sprite LockPic; // 錠前の画像
    [SerializeField] private Sprite AvailablePic; // 普通の時の画像

    private void Start()
    {
        
    }

    private void Update()
    {
        foreach (var item in BetaMissionManager.Instance.allMissions)
        {
            ColorChange(item);
        }

        BetaMissionDescriptionText.text = "今までの試練のクリア数: " + BetaMissionManager.Instance.LatestCompletedMissionID + "\n" + "現在、試練により" + (BetaMissionManager.Instance.LatestCompletedMissionID + 1) + "倍のBFを獲得中";
    }

    public void OnButtonClicked(BetaMission betaMission) // ボタン処理
    {
        if (BetaMissionManager.Instance.GetBetaMissionStatus(betaMission.missionID) == BetaMissionStatus.Available) // 挑戦可能なら
        {
            BetaMissionManager.Instance.EnterOrExitBetaMission(betaMission.missionID, true); // 挑戦
        }
        else if (BetaMissionManager.Instance.GetBetaMissionStatus(betaMission.missionID) == BetaMissionStatus.Now) // 今挑戦中なら
        {
            BetaMissionManager.Instance.EnterOrExitBetaMission(betaMission.missionID, false); // 挑戦をやめる
        }
        else
        {
            return;
        }
        ColorChange(betaMission); // 色適用
    }

    public void ColorChange(BetaMission betaMission) // 各試練の色を変える関数
    {
        BetaMissionStatus status = BetaMissionManager.Instance.GetBetaMissionStatus(betaMission.missionID);
        int missionIndex = betaMission.missionID - 1; // 試練リストのインデックス

        if (missionIndex < 0 || missionIndex >= MissionList.Count)
        {
            Debug.LogError("not found mission");
            return;
        }

        Button missionButton = MissionList[missionIndex];
        Image buttonImage = missionButton.GetComponentInChildren<Image>();
        TextMeshProUGUI titleText = MissionTitles[missionIndex];
        TextMeshProUGUI descriptionText = MissionDescriptions[missionIndex];

        if (status == BetaMissionStatus.Locked)
        {
            buttonImage.sprite = LockPic; // ロック中は錠前の画像
            buttonImage.color = Color.white; // 画像の色
            missionButton.interactable = false; // 押せないようにしとく
        }
        else if(status == BetaMissionStatus.Available)
        {
            buttonImage.sprite = AvailablePic; // ロック以外ならただの白い画像
            buttonImage.color = Color.white; // 挑戦可能なら白色
            titleText.text = "試練 No." + betaMission.missionID + " (挑戦可能)"; // "(挑戦可能)"と追記
            descriptionText.text = betaMission.description; // 説明文を設定
            missionButton.interactable = true; // 押せるように
        }
        else if(status == BetaMissionStatus.Now)
        {
            buttonImage.sprite = AvailablePic; // ロック以外ならただの白い画像
            buttonImage.color = Color.yellow; // 挑戦中なら黄色
            titleText.text = "試練 No." + betaMission.missionID + " (挑戦中)"; // "(挑戦中)"と追記
            descriptionText.text = betaMission.description; // 説明文を設定
            missionButton.interactable = true; // 押せるように
        }
        else if(status == BetaMissionStatus.Completed)
        {
            buttonImage.sprite = AvailablePic; // ロック以外ならただの白い画像
            buttonImage.color = Color.green; // 完了なら緑色
            titleText.text = "試練 No." + betaMission.missionID + " (完了)"; // "(完了)"と追記
            descriptionText.text = betaMission.description; // 説明文を設定
            missionButton.interactable = false; // 押せないようにしとく
        }
        else
        {
            return;
        }
    }
}
