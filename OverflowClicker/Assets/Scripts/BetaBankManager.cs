using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaBankManager : MonoBehaviour
{
    public static BetaBankManager Instance { get; private set; }

    public double CurrentBetaBankAmount { get; private set; } = 0.0; // ���ݗa���Ă���BF�̗�
    public double InterestRate { get; private set; } = 0.0001; // ��s�̗����i0.01%�j

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async void Start()
    {
        // ���̃I�u�W�F�N�g���j�����ꂽ�Ƃ��ɃL�����Z�������g�[�N�����擾
        var ct = this.GetCancellationTokenOnDestroy();
        try
        {
            // �L�����Z�������܂Łi���I�u�W�F�N�g���j�������܂Łj���[�v
            while (!ct.IsCancellationRequested)
            {
                // �܂��A�b�v�O���[�h����������܂őҋ@����
                // (���ł�true�Ȃ瑦���ɒʉ߂��܂�)
                await UniTask.WaitUntil(() => BetaUpgradeManager.Instance.IsUpgrade9Completed, cancellationToken: ct);

                // --- �A�b�v�O���[�h���L���ȊԂ̏��� ---

                // 1�b�ҋ@����
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: ct);

                // 1�b�҂�����A�܂��A�b�v�O���[�h���L�����m�F���Ă��珈�������s
                // (Delay����false�ɂȂ����ꍇ��h��)
                if (BetaUpgradeManager.Instance.IsUpgrade9Completed)
                {
                    ApplyInterest(); // ������K�p
                }
            }
        }
        catch (OperationCanceledException)
        {
            // ���������I��
        }
    }

    public void InitializeFromSaveData(double savedAmount, double savedInterestRate) // �Z�[�u�f�[�^���珉����
    {
        CurrentBetaBankAmount = savedAmount;
        InterestRate = savedInterestRate;
    }

    public void DepositToBank(double amount) // ��s�ɗa������
    {
        if (amount <= GameManager.Instance.BetaFactorForDisplay)
        {
            GameManager.Instance.SubBetaFactor(amount);
            CurrentBetaBankAmount += amount;
        }
        else
        {
            Debug.Log("BF������܂���!");
        }
    }

    public void WithdrawFromBank(double amount) // ��s��������o��
    {
        if (amount <= CurrentBetaBankAmount)
        {
            CurrentBetaBankAmount -= amount;
            GameManager.Instance.AddBetaFactorFromBank(amount);
        }
        else
        {
            Debug.Log("��s��BF������܂���!");
        }
    }

    public void ApplyInterest() // ������K�p
    {
        CurrentBetaBankAmount += CurrentBetaBankAmount * InterestRate;
        Debug.Log("CurrentBetaBankAmount: " + CurrentBetaBankAmount);
    }

    public void SetInterestRate(double newRate) // ������ݒ�
    {
        InterestRate = newRate;
        Debug.Log("New InterestRate: " + InterestRate);
    }

    public void ResetBank()
    {
        CurrentBetaBankAmount = 0;
        InterestRate = 0.0001;
    }
}
