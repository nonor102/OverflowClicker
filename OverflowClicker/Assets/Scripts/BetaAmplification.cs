using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Search;
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

    private void CalcAFAmplifyNum(double num/*, bool restoration*/)
    {
        //if (restoration)
        //{
        /*AFAmplifyNum = 1; // ゲーム開始時の実行では初期化

        double remainingNum = num; // 計算に使う

        if (remainingNum > 10000)
        {
            double amountInThisTier = remainingNum - 10000;
            AFAmplifyNum += amountInThisTier * 0.00005;
            remainingNum = 10000;
        }
        if (remainingNum > 5000)
        {
            double amountInThisTier = remainingNum - 5000;
            AFAmplifyNum += amountInThisTier * 0.0001;
            remainingNum = 5000;
        }
        if (remainingNum > 1000)
        {
            double amountInThisTier = remainingNum - 1000;
            AFAmplifyNum += amountInThisTier * 0.0005;
            remainingNum = 1000;
        }
        if (remainingNum > 500)
        {
            double amountInThisTier = remainingNum - 500;
            AFAmplifyNum += amountInThisTier * 0.001;
            remainingNum = 500;
        }
        if (remainingNum > 100)
        {
            double amountInThisTier = remainingNum - 100;
            AFAmplifyNum += amountInThisTier * 0.005;
            remainingNum = 100;
        }
        if (remainingNum > 50)
        {
            double amountInThisTier = remainingNum - 50;
            AFAmplifyNum += amountInThisTier * 0.01;
            remainingNum = 50;
        }
        if (remainingNum > 0)
        {
            AFAmplifyNum += remainingNum * 0.02;
        }
        Debug.Log("Restored AFAmplifyNum: " + AFAmplifyNum);*/
        //}
        /*else
        {
            double addBFUsedInAmpNum = num; // あとで使用したBFを加算するために保存
            while (num > 0)
            {
                if (GameManager.Instance.BetaFactorUsedInAmplification + num > 10000)
                {
                    double amountInThisTier = num - 10000;
                    AFAmplifyNum += amountInThisTier * 0.00005;
                    num = 10000;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification + num > 5000 && GameManager.Instance.BetaFactorUsedInAmplification + num <= 10000)
                {
                    double amountInThisTier = num - 5000;
                    AFAmplifyNum += amountInThisTier * 0.0001;
                    num = 5000;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification + num > 1000 && GameManager.Instance.BetaFactorUsedInAmplification + num <= 5000)
                {
                    double amountInThisTier = num - 1000;
                    AFAmplifyNum += amountInThisTier * 0.0005;
                    num = 1000;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification + num > 500 && GameManager.Instance.BetaFactorUsedInAmplification + num <= 1000)
                {
                    double amountInThisTier = num - 500;
                    AFAmplifyNum += amountInThisTier * 0.001;
                    num = 500;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification + num > 100 && GameManager.Instance.BetaFactorUsedInAmplification + num <= 500)
                {
                    double amountInThisTier = num - 100;
                    AFAmplifyNum += amountInThisTier * 0.005;
                    num = 100;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification + num > 50 && GameManager.Instance.BetaFactorUsedInAmplification + num <= 100)
                {
                    double amountInThisTier = num - 50;
                    AFAmplifyNum += amountInThisTier * 0.01;
                    num = 50;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification + num > 0 && GameManager.Instance.BetaFactorUsedInAmplification + num <= 50)
                {
                    AFAmplifyNum += num * 0.02;
                    break;
                }
                Debug.Log("Added AFAmplifyNum: " + AFAmplifyNum);

                GameManager.Instance.AddBetaFactorUsedInAmplification(addBFUsedInAmpNum); // 通常の実行では使用したBFを加算
            }
        }
        */
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

    public void OnSubmit()
    {
        if (!string.IsNullOrEmpty(BFInputArea.text))
        {
            double useBFNum = double.Parse(BFInputArea.text);
            if (useBFNum <= GameManager.Instance.BetaFactorForDisplay)
            {
                CalcAFAmplifyNum(useBFNum/*, false*/);
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
        CalcAFAmplifyNum(0/*, true*/);
        ShowDetails();
    }
}
