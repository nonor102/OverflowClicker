using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AlphaFactor : MonoBehaviour
{
    [SerializeField] private GameObject alpha2BetaButton;
    [SerializeField] private TextMeshProUGUI alphaFactorText;
    [SerializeField] private TextMeshProUGUI alpha2BetaText;
    [SerializeField] private TextMeshProUGUI alphaFactorGainText;
    [SerializeField] private GameObject displayAFEquationButton;
    [SerializeField] private GameObject nowAFEquationPanel;
    [SerializeField] private GameObject displayFactorsButton;
    [SerializeField] private GameObject nowFactorsPanel;

    private double displayAlphaFactor; // ボタンに表示する用のAF
    private double displayBetaFactor; // ボタンに表示する用のBF

    private double displayAFPerClickInEquation; // AFの計算式に表示する、クリックで得られるAF
    private double displayAFMultiInEquation; // AFの計算式に表示する、AF乗数

    private string displayAFPerClickText; // AFの計算式に表示する、クリックで得られるAFのテキスト用
    private string displayAFMultiText; // AFの計算式に表示する、AF乗数のテキスト用

    public void OnAlphaFactorButtonClicked()
    {
        GameManager.Instance.UpdateAlphaFactor();
    }

    public void OnAlpha2BetaButtonClicked()
    {
        GameManager.Instance.Alpha2Beta();
    }

    private void Start()
    {
        displayFactorsButton.SetActive(false);
        displayAFEquationButton.SetActive(false);
    }

    void Update()
    {
        displayAlphaFactor = Math.Pow((GameManager.Instance.AlphaFactorPerClick * GameManager.Instance.AlphaFactorMulti), GameManager.Instance.AlphaFactorExp);
        if (displayAlphaFactor < 100) // ボタンに表示用のAFが100以下なら小数点以下2桁まで表示
        {
            alphaFactorGainText.text = "" + $"{displayAlphaFactor:F2}" + "AFを獲得";
        }
        else
        {
            alphaFactorGainText.text = "" + displayAlphaFactor + "AFを獲得";
        }
        alphaFactorText.text = "AF: " + GameManager.Instance.AlphaFactorForDisplay;
        if (GameManager.Instance.IsAlphaOverflowCollapsed && GameManager.Instance.AlphaOverflowCount > 0)
        {
            alphaFactorText.text += "\n" + "(Overflow: " + GameManager.Instance.AlphaOverflowCount + ")";
        }

        if(GameManager.Instance.AlphaOverflowCount > 0)
        {
            if (BetaUpgradeManager.Instance.IsUpgrade11Completed)
            {
                GameManager.Instance.AddBetaFactorMultiFromUpgrade11ByBetaNum();
            }
            displayBetaFactor = Math.Pow((GameManager.Instance.BetaFactorPerGain * GameManager.Instance.BetaFactorMulti), GameManager.Instance.BetaFactorExp) * GameManager.Instance.BetaFactorMultiFromUpgrade6 * GameManager.Instance.BetaFactorMultiFromUpgrade11;
            alpha2BetaButton.SetActive(true);
            if(displayBetaFactor < 100) // ボタンに表示用のBFが100以下なら小数点以下2桁まで表示
            {
                alpha2BetaText.text = "" + $"{displayBetaFactor:F2}" + "BFを獲得";
            }
            else
            {
                alpha2BetaText.text = "" + displayBetaFactor + "BFを獲得";
            }
        }
        else
        {
            alpha2BetaButton.SetActive(false);
        }

        if (BetaUpgradeManager.Instance.IsUpgrade0Completed == true)
        {
            displayFactorsButton.SetActive(true);
            nowFactorsPanel.GetComponentInChildren<TextMeshProUGUI>().text = "BF: " + GameManager.Instance.BetaFactorForDisplay + " β: " + GameManager.Instance.BetaNum;
            displayAFEquationButton.SetActive(true);
            displayAFPerClickInEquation = GameManager.Instance.AlphaFactorPerClick;
            displayAFMultiInEquation = GameManager.Instance.AlphaFactorMulti;

            if(displayAFPerClickInEquation < 100)
            {
                displayAFPerClickText = $"{GameManager.Instance.AlphaFactorPerClick:F2}";
            }
            else
            {
                displayAFPerClickText = "" + GameManager.Instance.AlphaFactorPerClick;
            }

            if(displayAFMultiInEquation < 100)
            {
                displayAFMultiText = $"{GameManager.Instance.AlphaFactorMulti:F2}";
            }
            else
            {
                displayAFMultiText = "" + GameManager.Instance.AlphaFactorMulti;
            }
            nowAFEquationPanel.GetComponentInChildren<TextMeshProUGUI>().text = "AFの計算式: \n" + "(" + displayAFPerClickText + " * " + displayAFMultiText + ") ^ " + $"{GameManager.Instance.AlphaFactorExp:F2}";
        }
    }
}
