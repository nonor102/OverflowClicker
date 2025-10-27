using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Search;
using UnityEditor;

public class BetaAmplification : MonoBehaviour
{
    [SerializeField] private TMP_InputField BFInputArea;
    [SerializeField] private TextMeshProUGUI UsedBFAndAFMultiText;

    private double AFAmplifyNum = 1;

    private void Start()
    {
        ResetAFAmplification();
        ShowDetails();
    }

    private void CalcAFAmplifyNum(double num, bool restoration)
    {
        if (restoration)
        {
            AFAmplifyNum = 1; // �Q�[���J�n���̎��s�ł͏�����
        }
        else
        {
            GameManager.Instance.AddBetaFactorUsedInAmplification(num); // �ʏ�̎��s�ł͎g�p����BF�����Z
        }

        if (restoration)
        {
            // === �������̃��W�b�N (�i�K�I�v�Z) ===
            double remainingNum = num;

            if (remainingNum > 10000)
            {
                double amountInThisTier = remainingNum - 10000;
                AFAmplifyNum += amountInThisTier * 0.00001;
                remainingNum = 10000;
            }
            if (remainingNum > 5000)
            {
                double amountInThisTier = remainingNum - 5000;
                AFAmplifyNum += amountInThisTier * 0.00005;
                remainingNum = 5000;
            }
            if (remainingNum > 1000)
            {
                double amountInThisTier = remainingNum - 1000;
                AFAmplifyNum += amountInThisTier * 0.0001;
                remainingNum = 1000;
            }
            if (remainingNum > 500)
            {
                double amountInThisTier = remainingNum - 500;
                AFAmplifyNum += amountInThisTier * 0.0005;
                remainingNum = 500;
            }
            if (remainingNum > 100)
            {
                double amountInThisTier = remainingNum - 100;
                AFAmplifyNum += amountInThisTier * 0.001;
                remainingNum = 100;
            }
            if (remainingNum > 50)
            {
                double amountInThisTier = remainingNum - 50;
                AFAmplifyNum += amountInThisTier * 0.005;
                remainingNum = 50;
            }
            if (remainingNum > 0)
            {
                AFAmplifyNum += remainingNum * 0.01;
            }
            Debug.Log("Restored AFAmplifyNum: " + AFAmplifyNum);
        }
        else
        {
            // === �ʏ펞�̃��W�b�N ===

            while (num > 0)
            {
                if (GameManager.Instance.BetaFactorUsedInAmplification > 10000)
                {
                    AFAmplifyNum += num * 0.00001;
                    num -= 10000;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification > 5000 && GameManager.Instance.BetaFactorUsedInAmplification <= 10000)
                {
                    AFAmplifyNum += num * 0.00005;
                    num -= 5000;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification > 1000 && GameManager.Instance.BetaFactorUsedInAmplification <= 5000)
                {
                    AFAmplifyNum += num * 0.0001;
                    num -= 1000;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification > 500 && GameManager.Instance.BetaFactorUsedInAmplification <= 1000)
                {
                    AFAmplifyNum += num * 0.0005;
                    num -= 500;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification > 100 && GameManager.Instance.BetaFactorUsedInAmplification <= 500)
                {
                    AFAmplifyNum += num * 0.001;
                    num -= 100;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification > 50 && GameManager.Instance.BetaFactorUsedInAmplification <= 100)
                {
                    AFAmplifyNum += num * 0.005;
                    num -= 50;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification >= 0 && GameManager.Instance.BetaFactorUsedInAmplification <= 50)
                {
                    AFAmplifyNum += num * 0.01;
                    num -= 10;
                }
                Debug.Log("Added AFAmplifyNum: " + AFAmplifyNum);
            }
        }
        GameManager.Instance.SetAlphaFactorMultiFromBetaAmplification(AFAmplifyNum);
    }

    private void ShowDetails()
    {
        UsedBFAndAFMultiText.text = "���܂ł�" + GameManager.Instance.BetaFactorUsedInAmplification + "BF���g�p" + "\n" + $"{GameManager.Instance.AlphaFactorMulti:F2}" + "�{��AF���l����";
    }

    public void OnSubmit()
    {
        if (!string.IsNullOrEmpty(BFInputArea.text))
        {
            double useBFNum = double.Parse(BFInputArea.text);
            if (useBFNum <= GameManager.Instance.BetaFactorForDisplay)
            {
                CalcAFAmplifyNum(useBFNum, false);
                GameManager.Instance.AddAlphaFactorMulti(AFAmplifyNum);
                GameManager.Instance.SubBetaFactor(useBFNum);
                ShowDetails();
            }
            else
            {
                UsedBFAndAFMultiText.text = "BF������܂���!";
            }
        }
        else
        {
            UsedBFAndAFMultiText.text = "�g�p����BF�̒l����͂��Ă�������!";
        }
    }

    public void ResetAFAmplification() // AFAmplifyNum���Q�[���N�����ɍĐݒ肷��
    {
        CalcAFAmplifyNum(GameManager.Instance.BetaFactorUsedInAmplification, true);
        ShowDetails();
    }
}
