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

        amplificationText.text = "���b�N��";
        missionText.text = "���b�N��";
        bankText.text = "���b�N��";
        revolutionText.text = "���b�N��";
    }

    private void Update()
    {
        if (BetaUpgradeManager.Instance.IsUpgrade7Completed)
        {
            amplification.interactable = true;
            amplificationText.text = "����";
        }
        if (BetaUpgradeManager.Instance.IsUpgrade8Completed)
        {
            mission.interactable = true;
            amplificationText.text = "����";
        }
        if (BetaUpgradeManager.Instance.IsUpgrade9Completed)
        {
            bank.interactable = true;
            amplificationText.text = "��s";
        }
        if (BetaUpgradeManager.Instance.IsUpgrade10Completed)
        {
            revolution.interactable = true;
            amplificationText.text = "�v��";
        }
        
        
        
    }
}
