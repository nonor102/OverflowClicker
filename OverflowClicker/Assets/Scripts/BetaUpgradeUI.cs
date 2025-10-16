using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BetaUpgradeUI : MonoBehaviour
{
    [SerializeField] private BetaUpgrade betaUpgrade;
    [SerializeField] private TextMeshProUGUI upgradeTitleText;
    [SerializeField] private TextMeshProUGUI upgradeDescriptionText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickedUpgradeButtonFromTree() // ������ʂŃN���b�N���ꂽ�Ƃ� (��������{�^���ł͂Ȃ�)
    {
        upgradeTitleText.text = betaUpgrade.title;
        upgradeDescriptionText.text = betaUpgrade.description;
    }

    public void OnClickedUpgradeButton() // ������ʂőI������ăN���b�N���ꂽ�Ƃ� (��������{�^��)
    {
        BetaUpgradeManager.Instance.CompleteMission(betaUpgrade.missionID);
    }
}
