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

    private double displayAlpfaFactor; // ボタンに表示する用のAF
    private double displayBetaFactor; // ボタンに表示する用のBF

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
        displayAlpfaFactor = Math.Pow((GameManager.Instance.AlphaFactorPerClick * GameManager.Instance.AlphaFactorMulti), GameManager.Instance.AlphaFactorExp);
        if (displayAlpfaFactor < 100) // ボタンに表示用のAFが100以下なら小数点以下2桁まで表示
        {
            alphaFactorGainText.text = "" + $"{displayAlpfaFactor:F2}" + "AFを獲得";
        }
        else
        {
            alphaFactorGainText.text = "" + displayAlpfaFactor + "AFを獲得";
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
            nowAFEquationPanel.GetComponentInChildren<TextMeshProUGUI>().text = "AFの計算式: \n" + "(" + $"{GameManager.Instance.AlphaFactorPerClick:F2}" + " * " + $"{GameManager.Instance.AlphaFactorMulti:F2}" + ") ^ " + $"{GameManager.Instance.AlphaFactorExp:F2}";
        }
    }
}
