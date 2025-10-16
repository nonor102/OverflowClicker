using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetaFactor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nowBFAndBetaNumText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nowBFAndBetaNumText.text = "BF: " + GameManager.Instance.BetaFactorForDisplay + "\n" + "É¿: " + GameManager.Instance.BetaNum;
    }
}
