using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetaFactor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nowBFAndBetaNumText;

    void Update()
    {
        nowBFAndBetaNumText.text = "BF: " + GameManager.Instance.BetaFactorForDisplay + "\n" + "É¿: " + GameManager.Instance.BetaNum;
    }
}
