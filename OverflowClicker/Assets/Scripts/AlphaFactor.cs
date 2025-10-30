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

    private double displayAlphaFactor; // �{�^���ɕ\������p��AF
    private double displayBetaFactor; // �{�^���ɕ\������p��BF

    private double displayAFPerClickInEquation; // AF�̌v�Z���ɕ\������A�N���b�N�œ�����AF
    private double displayAFMultiInEquation; // AF�̌v�Z���ɕ\������AAF�搔

    private string displayAFPerClickText; // AF�̌v�Z���ɕ\������A�N���b�N�œ�����AF�̃e�L�X�g�p
    private string displayAFMultiText; // AF�̌v�Z���ɕ\������AAF�搔�̃e�L�X�g�p

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
        if (displayAlphaFactor < 100) // �{�^���ɕ\���p��AF��100�ȉ��Ȃ珬���_�ȉ�2���܂ŕ\��
        {
            alphaFactorGainText.text = "" + $"{displayAlphaFactor:F2}" + "AF���l��";
        }
        else
        {
            alphaFactorGainText.text = "" + displayAlphaFactor + "AF���l��";
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
            if(displayBetaFactor < 100) // �{�^���ɕ\���p��BF��100�ȉ��Ȃ珬���_�ȉ�2���܂ŕ\��
            {
                alpha2BetaText.text = "" + $"{displayBetaFactor:F2}" + "BF���l��";
            }
            else
            {
                alpha2BetaText.text = "" + displayBetaFactor + "BF���l��";
            }
        }
        else
        {
            alpha2BetaButton.SetActive(false);
        }

        if (BetaUpgradeManager.Instance.IsUpgrade0Completed == true)
        {
            displayFactorsButton.SetActive(true);
            nowFactorsPanel.GetComponentInChildren<TextMeshProUGUI>().text = "BF: " + GameManager.Instance.BetaFactorForDisplay + " ��: " + GameManager.Instance.BetaNum;
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
            nowAFEquationPanel.GetComponentInChildren<TextMeshProUGUI>().text = "AF�̌v�Z��: \n" + "(" + displayAFPerClickText + " * " + displayAFMultiText + ") ^ " + $"{GameManager.Instance.AlphaFactorExp:F2}";
        }
    }
}
