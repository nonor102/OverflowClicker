using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class BetaAmplification : MonoBehaviour
{
    [SerializeField] private TMP_InputField BFInputArea;
    [SerializeField] private TextMeshProUGUI UsedBFAndAFMultiText;

    private double AFAmplifyNum = 1;

    private void Start()
    {
        ResetAFAmplification();
        ShowDetails();
    }

    private void CalcAFAmplifyNum(double num)
    {
        AFAmplifyNum = 1; // 初期化
        GameManager.Instance.AddBetaFactorUsedInAmplification(num);
        double useBFNum = GameManager.Instance.BetaFactorUsedInAmplification; // 今回の実行で使用するBFの量を計算

        while (useBFNum > 0)
        {
            if (useBFNum > 10000)
            {
                double amountInThisTier = useBFNum - 10000;
                AFAmplifyNum += amountInThisTier * 0.00005;
                useBFNum = 10000;
            }
            if (useBFNum > 5000)
            {
                double amountInThisTier = useBFNum - 5000;
                AFAmplifyNum += amountInThisTier * 0.0001;
                useBFNum = 5000;
            }
            if (useBFNum > 1000)
            {
                double amountInThisTier = useBFNum - 1000;
                AFAmplifyNum += amountInThisTier * 0.0005;
                useBFNum = 1000;
            }
            if (useBFNum > 500)
            {
                double amountInThisTier = useBFNum - 500;
                AFAmplifyNum += amountInThisTier * 0.001;
                useBFNum = 500;
            }
            if (useBFNum > 100)
            {
                double amountInThisTier = useBFNum - 100;
                AFAmplifyNum += amountInThisTier * 0.005;
                useBFNum = 100;
            }
            if (useBFNum > 50)
            {
                double amountInThisTier = useBFNum - 50;
                AFAmplifyNum += amountInThisTier * 0.01;
                useBFNum = 50;
            }
            if (useBFNum > 0)
            {
                AFAmplifyNum += useBFNum * 0.02;
                useBFNum = 0;
            }
        }
        Debug.Log("Restored AFAmplifyNum: " + AFAmplifyNum);

        GameManager.Instance.SetAlphaFactorMultiFromBetaAmplification(AFAmplifyNum);
    }

    private void ShowDetails()
    {
        UsedBFAndAFMultiText.text = "今までに" + GameManager.Instance.BetaFactorUsedInAmplification + "BFを使用" + "\n" + $"{GameManager.Instance.AlphaFactorMulti:F2}" + "倍のAFを獲得中";
    }

    public void OnSubmit() // 購入ボタンが押されたとき
    {
        if (!string.IsNullOrEmpty(BFInputArea.text))
        {
            double useBFNum = double.Parse(BFInputArea.text);
            if (useBFNum <= GameManager.Instance.BetaFactorForDisplay)
            {
                CalcAFAmplifyNum(useBFNum);
                GameManager.Instance.SubBetaFactor(useBFNum);
                ShowDetails();
            }
            else
            {
                UsedBFAndAFMultiText.text = "BFが足りません!";
            }
        }
        else
        {
            UsedBFAndAFMultiText.text = "使用するBFの値を入力してください!";
        }
    }

    public void ResetAFAmplification() // AFAmplifyNumをゲーム起動時に再設定する
    {
        CalcAFAmplifyNum(0);
        ShowDetails();
    }
}
