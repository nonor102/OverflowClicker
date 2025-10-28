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

    private void CalcAFAmplifyNum(double num/*, bool restoration*/)
    {
        //if (restoration)
        //{
        /*AFAmplifyNum = 1; // �Q�[���J�n���̎��s�ł͏�����

        double remainingNum = num; // �v�Z�Ɏg��

        if (remainingNum > 10000)
        {
            double amountInThisTier = remainingNum - 10000;
            AFAmplifyNum += amountInThisTier * 0.00005;
            remainingNum = 10000;
        }
        if (remainingNum > 5000)
        {
            double amountInThisTier = remainingNum - 5000;
            AFAmplifyNum += amountInThisTier * 0.0001;
            remainingNum = 5000;
        }
        if (remainingNum > 1000)
        {
            double amountInThisTier = remainingNum - 1000;
            AFAmplifyNum += amountInThisTier * 0.0005;
            remainingNum = 1000;
        }
        if (remainingNum > 500)
        {
            double amountInThisTier = remainingNum - 500;
            AFAmplifyNum += amountInThisTier * 0.001;
            remainingNum = 500;
        }
        if (remainingNum > 100)
        {
            double amountInThisTier = remainingNum - 100;
            AFAmplifyNum += amountInThisTier * 0.005;
            remainingNum = 100;
        }
        if (remainingNum > 50)
        {
            double amountInThisTier = remainingNum - 50;
            AFAmplifyNum += amountInThisTier * 0.01;
            remainingNum = 50;
        }
        if (remainingNum > 0)
        {
            AFAmplifyNum += remainingNum * 0.02;
        }
        Debug.Log("Restored AFAmplifyNum: " + AFAmplifyNum);*/
        //}
        /*else
        {
            double addBFUsedInAmpNum = num; // ���ƂŎg�p����BF�����Z���邽�߂ɕۑ�
            while (num > 0)
            {
                if (GameManager.Instance.BetaFactorUsedInAmplification + num > 10000)
                {
                    double amountInThisTier = num - 10000;
                    AFAmplifyNum += amountInThisTier * 0.00005;
                    num = 10000;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification + num > 5000 && GameManager.Instance.BetaFactorUsedInAmplification + num <= 10000)
                {
                    double amountInThisTier = num - 5000;
                    AFAmplifyNum += amountInThisTier * 0.0001;
                    num = 5000;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification + num > 1000 && GameManager.Instance.BetaFactorUsedInAmplification + num <= 5000)
                {
                    double amountInThisTier = num - 1000;
                    AFAmplifyNum += amountInThisTier * 0.0005;
                    num = 1000;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification + num > 500 && GameManager.Instance.BetaFactorUsedInAmplification + num <= 1000)
                {
                    double amountInThisTier = num - 500;
                    AFAmplifyNum += amountInThisTier * 0.001;
                    num = 500;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification + num > 100 && GameManager.Instance.BetaFactorUsedInAmplification + num <= 500)
                {
                    double amountInThisTier = num - 100;
                    AFAmplifyNum += amountInThisTier * 0.005;
                    num = 100;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification + num > 50 && GameManager.Instance.BetaFactorUsedInAmplification + num <= 100)
                {
                    double amountInThisTier = num - 50;
                    AFAmplifyNum += amountInThisTier * 0.01;
                    num = 50;
                }
                else if (GameManager.Instance.BetaFactorUsedInAmplification + num > 0 && GameManager.Instance.BetaFactorUsedInAmplification + num <= 50)
                {
                    AFAmplifyNum += num * 0.02;
                    break;
                }
                Debug.Log("Added AFAmplifyNum: " + AFAmplifyNum);

                GameManager.Instance.AddBetaFactorUsedInAmplification(addBFUsedInAmpNum); // �ʏ�̎��s�ł͎g�p����BF�����Z
            }
        }
        */
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

    public void OnSubmit()
    {
        if (!string.IsNullOrEmpty(BFInputArea.text))
        {
            double useBFNum = double.Parse(BFInputArea.text);
            if (useBFNum <= GameManager.Instance.BetaFactorForDisplay)
            {
                CalcAFAmplifyNum(useBFNum/*, false*/);
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
        CalcAFAmplifyNum(0/*, true*/);
        ShowDetails();
    }
}
