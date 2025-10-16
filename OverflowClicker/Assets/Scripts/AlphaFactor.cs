using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlphaFactor : MonoBehaviour
{
    [SerializeField] private GameObject alpha2BetaButton;
    [SerializeField] private TextMeshProUGUI alphaFactorText;
    [SerializeField] private TextMeshProUGUI alpha2BetaText;
    [SerializeField] private TextMeshProUGUI nowAFMultiAndExpText;

    public void OnAlphaFactorButtonClicked()
    {
        GameManager.Instance.UpdateAlphaFactor();
    }

    public void OnAlpha2BetaButtonClicked()
    {
        GameManager.Instance.Alpha2Beta();
    }
    
    void Start()
    {
        
    }

    void Update()
    {
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
            alpha2BetaText.text = "" + Math.Pow((GameManager.Instance.AddBetaFactorPerGain * GameManager.Instance.BetaFactorMulti), GameManager.Instance.BetaFactorExp) + "BF‚ğŠl“¾";
        }

        nowAFMultiAndExpText.text = "AFŠl“¾æ”: " + GameManager.Instance.AlphaFactorMulti + "\n" + "AFŠl“¾w”: " + GameManager.Instance.AlphaFactorExp;
    }
}
