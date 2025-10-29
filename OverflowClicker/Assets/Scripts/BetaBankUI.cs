using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetaBankUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI betaBankDescriptionText;
    [SerializeField] private Button depositButton;
    [SerializeField] private Button withdrawButton;
    [SerializeField] private TMP_InputField amountInputField;

    // Start is called before the first frame update
    void Start()
    {
        ShowDetails();
    }


    public void OnDepositButtonClicked()
    {
        if (double.TryParse(amountInputField.text, out double amount))
        {
            if (amount > 0 && amount <= GameManager.Instance.BetaFactorForDisplay)
            {
                BetaBankManager.Instance.DepositToBank(amount);
            }
            else
            {
                betaBankDescriptionText.text = "BF‚ª‘«‚è‚Ü‚¹‚ñ!";
                Debug.LogWarning("Invalid deposit amount");
            }
        }
        else
        {
            Debug.LogWarning("Invalid input for deposit amount");
        }
        ShowDetails();
    }

    public void OnWithdrawButtonClicked()
    {
        if (double.TryParse(amountInputField.text, out double amount))
        {
            if (amount > 0 && amount <= BetaBankManager.Instance.CurrentBetaBankAmount)
            {
                BetaBankManager.Instance.WithdrawFromBank(amount);
            }
            else
            {
                Debug.LogWarning("Invalid withdraw amount");
            }
        }
        else
        {
            Debug.LogWarning("Invalid input for withdraw amount");
        }
        ShowDetails();
    }

    private void ShowDetails()
    {
        if(BetaBankManager.Instance.CurrentBetaBankAmount < 100)
        {
            betaBankDescriptionText.text = "Œ»Ý‚Ì—a‚¯“ü‚êBF: " + (BetaBankManager.Instance.CurrentBetaBankAmount).ToString("F2") + " BF\n" +
            "‹âs‚Ì—˜—¦: " + (BetaBankManager.Instance.InterestRate * 100).ToString("F3") + " % / •b";
        }
        else
        {
            betaBankDescriptionText.text = "Œ»Ý‚Ì—a‚¯“ü‚êBF: " + (BetaBankManager.Instance.CurrentBetaBankAmount).ToString("F0") + " BF\n" +
            "‹âs‚Ì—˜—¦: " + (BetaBankManager.Instance.InterestRate * 100).ToString("F3") + " % / •b";
        }
    }
}
