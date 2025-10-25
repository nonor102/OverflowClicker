using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Search;

public class BetaAmplification : MonoBehaviour
{
    [SerializeField] private TMP_InputField BFInputArea;
    [SerializeField] private TextMeshProUGUI UsedBFAndAFMultiText;

    private double AFAmplifyNum = 1;

    private void Start()
    {
        ShowDetails();
    }

    private void CalcAFAmplifyNum(double num) // AFAmplifyNumの値を計算
    {
        GameManager.Instance.AddBetaFactorUsedInAmplification(num);
        while(num > 0)
        {
            if (GameManager.Instance.BetaFactorUsedInAmplification > 10000)
            {
                AFAmplifyNum += num * 0.00001;
                num -= 10000;
            }
            else if (GameManager.Instance.BetaFactorUsedInAmplification > 5000 && GameManager.Instance.BetaFactorUsedInAmplification <= 10000)
            {
                AFAmplifyNum += num * 0.00005;
                num -= 5000;
            }
            else if (GameManager.Instance.BetaFactorUsedInAmplification > 1000 && GameManager.Instance.BetaFactorUsedInAmplification <= 5000)
            {
                AFAmplifyNum += num * 0.0001;
                num -= 1000;
            }
            else if (GameManager.Instance.BetaFactorUsedInAmplification > 500 && GameManager.Instance.BetaFactorUsedInAmplification <= 1000)
            {
                AFAmplifyNum += num * 0.0005;
                num -= 500;
            }
            else if (GameManager.Instance.BetaFactorUsedInAmplification > 100 && GameManager.Instance.BetaFactorUsedInAmplification <= 500)
            {
                AFAmplifyNum += num * 0.001;
                num -= 100;
            }
            else if(GameManager.Instance.BetaFactorUsedInAmplification > 50 &&  GameManager.Instance.BetaFactorUsedInAmplification <= 100)
            {
                AFAmplifyNum += num * 0.005;
                num -= 50;
            }
            else if(GameManager.Instance.BetaFactorUsedInAmplification >= 0 && GameManager.Instance.BetaFactorUsedInAmplification <= 50)
            {
                AFAmplifyNum += num * 0.01;
                num -= 10;
            }
        }
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
                CalcAFAmplifyNum(useBFNum);
                GameManager.Instance.AddAlphaFactorMulti(AFAmplifyNum);
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
}
