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

    public void OnClickedUpgradeButtonFromTree() // 強化画面でクリックされたとき (強化するボタンではない)
    {
        upgradeTitleText.text = betaUpgrade.title;
        upgradeDescriptionText.text = betaUpgrade.description;
    }

    public void OnClickedUpgradeButton() // 強化画面で選択されてクリックされたとき (強化するボタン)
    {
        BetaUpgradeManager.Instance.CompleteMission(betaUpgrade.missionID);
    }
}
