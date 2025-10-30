using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaBankManager : MonoBehaviour
{
    public static BetaBankManager Instance { get; private set; }

    public double CurrentBetaBankAmount { get; private set; } = 0.0; // 現在預けているBFの量
    public double InterestRate { get; private set; } = 0.0001; // 銀行の利率（0.01%）

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
        // このオブジェクトが破棄されたときにキャンセルされるトークンを取得
        var ct = this.GetCancellationTokenOnDestroy();
        try
        {
            // キャンセルされるまで（＝オブジェクトが破棄されるまで）ループ
            while (!ct.IsCancellationRequested)
            {
                // まずアップグレードが完了するまで待機する
                // (すでにtrueなら即座に通過します)
                await UniTask.WaitUntil(() => BetaUpgradeManager.Instance.IsUpgrade9Completed, cancellationToken: ct);

                // --- アップグレードが有効な間の処理 ---

                // 1秒待機する
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: ct);

                // 1秒待った後、まだアップグレードが有効か確認してから処理を実行
                // (Delay中にfalseになった場合を防ぐ)
                if (BetaUpgradeManager.Instance.IsUpgrade9Completed)
                {
                    ApplyInterest(); // 利息を適用
                }
            }
        }
        catch (OperationCanceledException)
        {
            // 何もせず終了
        }
    }

    public void InitializeFromSaveData(double savedAmount, double savedInterestRate) // セーブデータから初期化
    {
        CurrentBetaBankAmount = savedAmount;
        InterestRate = savedInterestRate;
    }

    public void DepositToBank(double amount) // 銀行に預け入れ
    {
        if (amount <= GameManager.Instance.BetaFactorForDisplay)
        {
            GameManager.Instance.SubBetaFactor(amount);
            CurrentBetaBankAmount += amount;
        }
        else
        {
            Debug.Log("BFが足りません!");
        }
    }

    public void WithdrawFromBank(double amount) // 銀行から引き出し
    {
        if (amount <= CurrentBetaBankAmount)
        {
            CurrentBetaBankAmount -= amount;
            GameManager.Instance.AddBetaFactorFromBank(amount);
        }
        else
        {
            Debug.Log("銀行のBFが足りません!");
        }
    }

    public void ApplyInterest() // 利息を適用
    {
        CurrentBetaBankAmount += CurrentBetaBankAmount * InterestRate;
        Debug.Log("CurrentBetaBankAmount: " + CurrentBetaBankAmount);
    }

    public void SetInterestRate(double newRate) // 利率を設定
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
