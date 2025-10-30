using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetaRevolution : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI warningText;

    private double RevolutionExpnum = 0;

    void Start()
    {
        warningText.color = Color.black;
    }

    void Update()
    {
        RevolutionExpnum = 1 + Mathf.Sqrt((float)GameManager.Instance.AllBetaFactorGetInThisTerm) * 0.001; // 総獲得BFのルートをとって0.001倍したものを1と足して強化量とする
        descriptionText.text = "AFとBFの獲得量を " + $"{RevolutionExpnum:F2}" + " 乗にする";
        warningText.text = "!警告!" + "\n" + "革命を実行すると、AF、BFの指数以外の全ての要素が初期化されます。" + "\n" + "革命は指数の値を上書きします。実行すると今よりも弱くなることがあります。";
    }

    public void OnSubmitRevolution() // 革命の実行ボタンが押されたら
    {
        GameManager.Instance.ResetBetaByRevolution();
        BetaUpgradeManager.Instance.ResetAllUpgrades();
        BetaMissionManager.Instance.ResetAllMissions();
        BetaBankManager.Instance.ResetBank();

        GameManager.Instance.SetAlphaFactorExp(RevolutionExpnum);
    }
}
