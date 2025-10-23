using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaToggle : MonoBehaviour
{
    [SerializeField] private Toggle upgrade;
    [SerializeField] private Toggle amplification;
    [SerializeField] private Toggle mission;
    [SerializeField] private Toggle bank;
    [SerializeField] private Toggle revolution;

    void Start()
    {
        upgrade.interactable = true;
        amplification.interactable = false;
        mission.interactable = false;
        bank.interactable = false;
        revolution.interactable = false;
    }

    private void Update()
    {
        amplification.interactable = BetaUpgradeManager.Instance.IsUpgrade7Completed;
        mission.interactable = BetaUpgradeManager.Instance.IsUpgrade8Completed;
        bank.interactable = BetaUpgradeManager.Instance.IsUpgrade9Completed;
        revolution.interactable = BetaUpgradeManager.Instance.IsUpgrade10Completed;
    }
}
