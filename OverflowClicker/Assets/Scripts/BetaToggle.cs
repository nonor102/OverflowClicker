using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetaToggle : MonoBehaviour
{
    [SerializeField] private Toggle upgrade;
    [SerializeField] private Toggle amplification;
    [SerializeField] private Toggle mission;
    [SerializeField] private Toggle bank;
    [SerializeField] private Toggle revolution;

    [SerializeField] private TextMeshProUGUI amplificationText;
    [SerializeField] private TextMeshProUGUI missionText;
    [SerializeField] private TextMeshProUGUI bankText;
    [SerializeField] private TextMeshProUGUI revolutionText;

    void Start()
    {
        upgrade.interactable = true;
        amplification.interactable = false;
        mission.interactable = false;
        bank.interactable = false;
        revolution.interactable = false;

        amplificationText.text = "ロック中";
        missionText.text = "ロック中";
        bankText.text = "ロック中";
        revolutionText.text = "ロック中";
    }

    private void Update()
    {
        if (BetaUpgradeManager.Instance.IsUpgrade7Completed)
        {
            amplification.interactable = true;
            amplificationText.text = "増幅";
        }
        if (BetaUpgradeManager.Instance.IsUpgrade8Completed)
        {
            mission.interactable = true;
            amplificationText.text = "試練";
        }
        if (BetaUpgradeManager.Instance.IsUpgrade9Completed)
        {
            bank.interactable = true;
            amplificationText.text = "銀行";
        }
        if (BetaUpgradeManager.Instance.IsUpgrade10Completed)
        {
            revolution.interactable = true;
            amplificationText.text = "革命";
        }
        
        
        
    }
}
