using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    private void CalcAFAmplifyNum(double num)
    {
        AFAmplifyNum = 1; // ������
        GameManager.Instance.AddBetaFactorUsedInAmplification(num);
        double useBFNum = GameManager.Instance.BetaFactorUsedInAmplification; // ����̎��s�Ŏg�p����BF�̗ʂ��v�Z

        while (useBFNum > 0)
        {
            if (useBFNum > 10000)
            {
                double amountInThisTier = useBFNum - 10000;
                AFAmplifyNum += amountInThisTier * 0.00005;
                useBFNum = 10000;
            }
            if (useBFNum > 5000)
            {
                double amountInThisTier = useBFNum - 5000;
                AFAmplifyNum += amountInThisTier * 0.0001;
                useBFNum = 5000;
            }
            if (useBFNum > 1000)
            {
                double amountInThisTier = useBFNum - 1000;
                AFAmplifyNum += amountInThisTier * 0.0005;
                useBFNum = 1000;
            }
            if (useBFNum > 500)
            {
                double amountInThisTier = useBFNum - 500;
                AFAmplifyNum += amountInThisTier * 0.001;
                useBFNum = 500;
            }
            if (useBFNum > 100)
            {
                double amountInThisTier = useBFNum - 100;
                AFAmplifyNum += amountInThisTier * 0.005;
                useBFNum = 100;
            }
            if (useBFNum > 50)
            {
                double amountInThisTier = useBFNum - 50;
                AFAmplifyNum += amountInThisTier * 0.01;
                useBFNum = 50;
            }
            if (useBFNum > 0)
            {
                AFAmplifyNum += useBFNum * 0.02;
                useBFNum = 0;
            }
        }
        Debug.Log("Restored AFAmplifyNum: " + AFAmplifyNum);

        GameManager.Instance.SetAlphaFactorMultiFromBetaAmplification(AFAmplifyNum);
    }

    private void ShowDetails()
    {
        UsedBFAndAFMultiText.text = "���܂ł�" + GameManager.Instance.BetaFactorUsedInAmplification + "BF���g�p" + "\n" + $"{GameManager.Instance.AlphaFactorMulti:F2}" + "�{��AF���l����";
    }

    public void OnSubmit() // �w���{�^���������ꂽ�Ƃ�
    {
        if (!string.IsNullOrEmpty(BFInputArea.text))
        {
            double useBFNum = double.Parse(BFInputArea.text);
            if (useBFNum <= GameManager.Instance.BetaFactorForDisplay)
            {
                CalcAFAmplifyNum(useBFNum);
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
        CalcAFAmplifyNum(0);
        ShowDetails();
    }
}
