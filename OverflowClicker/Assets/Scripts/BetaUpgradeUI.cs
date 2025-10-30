using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaUpgradeUI : MonoBehaviour
{
    [SerializeField] private List<Button> menuButtons;
    [SerializeField] private Sprite availableImage;
    [SerializeField] private Sprite lockedImage;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var button in menuButtons)
        {
            button.image.sprite = lockedImage;
        }
    }

    private void Update()
    {
        ButtonPicChanger();
    }

    private void ButtonPicChanger()
    {
        foreach (var upgrade in BetaUpgradeManager.Instance.allUpgrades)
        {
            var status = BetaUpgradeManager.Instance.GetBetaUpgradeStatus(upgrade.upgradeID);
            if (status == BetaUpgradeStatus.Available || status == BetaUpgradeStatus.Completed)
            {
                menuButtons[upgrade.upgradeID].image.sprite = availableImage;
            }
            else if (status == BetaUpgradeStatus.Locked)
            {
                menuButtons[upgrade.upgradeID].image.sprite = lockedImage;
            }
        }
    }
}
