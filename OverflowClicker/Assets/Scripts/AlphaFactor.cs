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
        alphaFactorGainText.text = "" + (sbyte)Math.Pow((GameManager.Instance.AlphaFactorPerClick * GameManager.Instance.AlphaFactorMulti), GameManager.Instance.AlphaFactorExp) + "AFÇälìæ";

        alphaFactorText.text = "AF: " + GameManager.Instance.AlphaFactorForDisplay;
        if (GameManager.Instance.IsAlphaOverflowCollapsed && GameManager.Instance.AlphaOverflowCount > 0)
        {
            alphaFactorText.text += "\n" + "(Overflow: " + GameManager.Instance.AlphaOverflowCount + ")";
        }

        if(GameManager.Instance.AlphaOverflowCount == 0)
        {
            alpha2BetaButton.SetActive(false);
        }

        if(GameManager.Instance.AlphaOverflowCount > 0)
        {
            alpha2BetaButton.SetActive(true);
            alpha2BetaText.text = "" + (short)Math.Pow((GameManager.Instance.BetaFactorPerGain * GameManager.Instance.BetaFactorMulti), GameManager.Instance.BetaFactorExp) + "BFÇälìæ";
        }

        if(BetaUpgradeManager.Instance.IsUpgrade0Completed == true)
        {
            displayFactorsButton.SetActive(true);
            nowFactorsPanel.GetComponentInChildren<TextMeshProUGUI>().text = "BF: " + GameManager.Instance.BetaFactorForDisplay + " É¿: " + GameManager.Instance.BetaNum;
            displayAFEquationButton.SetActive(true);
            nowAFEquationPanel.GetComponentInChildren<TextMeshProUGUI>().text = "åªç›ÇÃåvéZéÆ: \n" + "(" + GameManager.Instance.AlphaFactorPerClick + " * " + GameManager.Instance.AlphaFactorMulti + ") ^ " + GameManager.Instance.AlphaFactorExp;
        }
    }
}
