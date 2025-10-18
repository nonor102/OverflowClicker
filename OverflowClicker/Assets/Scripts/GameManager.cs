using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    // sbyte用の定数、変数、乗数たち
    // sbyteはAF (Alpha Factor)という名前で管理する
    private double _alphaFactor = 0; // 計算用のAF
    public sbyte AlphaFactorForDisplay { get; private set; } = 0; // 表示用のAF
    public double AlphaFactorPerClick { get; private set; } = 1; // クリックごとの増加AF
    public double AlphaFactorMulti { get; private set; } = 1; // AF獲得乗数
    public double AlphaFactorExp { get; private set; } = 1; // AF獲得指数
    public bool IsAlphaOverflowCollapsed { get; private set; } = false; // Alphaのオーバーフローがcollapseしたかどうかフラグ
    public double AlphaOverflowCount { get; private set; } = 0; // Alphaがオーバーフローした回数

    // short用の定数、変数、乗数たち
    // shortはBF (Beta Factor)という名前で管理する
    public bool IsArrivedBeta { get; private set; } = false; // Betaに到達したかフラグ
    private double _betaFactor = 0; // 計算用のBF
    public short BetaFactorForDisplay { get; private set; } = 0; // 表示用のBF
    public double BetaNum { get; private set; } = 0; // alpha2betaの回数
    public double BetaNumPerGain {  get; private set; } = 1; // BetaNumを増やす数
    public double BetaFactorPerGain { get; private set; } = 1; // 取得するBFの基本の数
    public double BetaFactorMulti { get; private set; } = 1; // BF獲得乗数
    public double BetaFactorExp { get; private set; } = 1; // BF獲得指数
    public bool IsBetaOverflowCollapsed { get; private set; } = false; // Betaのオーバーフローがcollapseしたかどうかフラグ
    public double BetaOverflowCount { get; private set; } = 0; // Betaがオーバーフローした回数

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void UpdateAlphaFactor()
    {
        if (!IsAlphaOverflowCollapsed) // alphaのcollapseフラグが立っていないとき
        {
            if (AlphaFactorForDisplay >= 0)
            {
                AddAlphaFactor();
            }
            else
            {
                AlphaOverflowCount = 1;
                _alphaFactor = 128; // オーバーフローすると表示上の値で-128に固定する
            }
        }
        else
        {
            sbyte beforeValue = AlphaFactorForDisplay; // ボタンが押される前の値を代入しておく(オーバーフロー検知用)
            AddAlphaFactor();
            AlphaFactorSyncer(); // AlphaFactorの値を更新しておく
            if (AlphaFactorForDisplay < 0 && beforeValue > 0) // AlphaFactorForDisplayの値が正の数から負の数へ変わったら
            {
                AlphaOverflowCount++;
                Debug.Log("Alpha Overflow: " + AlphaOverflowCount);
            }
        }
        AlphaFactorSyncer();
    }

    public void AddAlphaFactor() // _alphaFactorの値を増加させる関数
    {
        _alphaFactor += Math.Pow((AlphaFactorPerClick * AlphaFactorMulti), AlphaFactorExp); // (AlphaFactorPerClick * AlphaFactorMulti) ^ AlphaFactorExp という計算
    }

    public void AddAlphaFactorPerClick(double num) // numの値をAlphaFactorPerClickに掛けて値を上昇させる関数
    {
        AlphaFactorPerClick *= num;
        Debug.Log("AlphaFactorPerCiick: " + AlphaFactorPerClick);
    }

    public void AddAlphaFactorMulti(double num) // numの値をAlphaFactorMultiに掛けて値を上昇させる関数
    {
        AlphaFactorMulti *= num;
        Debug.Log("AlphaFactorMulti: " + AlphaFactorMulti);
    }

    public void AddAlphaFactorExp(double num) // numの値をAlphaFactorExpに掛けて値を上昇させる関数
    {
        AlphaFactorExp *= num;
        Debug.Log("AlphaFactorExp: " + AlphaFactorExp);
    }

    public void CollapseOrRestoreAlpha(bool value) // IsAlphaOverflowCollapsedのTFを変更する関数
    {
        if (value)
        {
            IsAlphaOverflowCollapsed = true;
        }
        else
        {
            IsAlphaOverflowCollapsed = false;
        }
    }

    private void AlphaFactorSyncer() // AlphaFactorForDisplayの値を_alphaFactorの値と対応させる関数
    {
        AlphaFactorForDisplay = (sbyte)_alphaFactor;
        Debug.Log("AlphaFactorForDisplay: " + AlphaFactorForDisplay);
    }

    public void Alpha2Beta() // alphaをリセットしてbetaを取得する関数
    {
        IsArrivedBeta = true;
        AddBetaFactor();
        AddBetaNum();
        BetaFactorSyncer();
        _alphaFactor = 0;
        AlphaOverflowCount = 0;
        AlphaFactorSyncer();
        Debug.Log("alpha2beta");
    }

    public void AddBetaFactor() // _betaFactorの値を増加させる関数
    {
        _betaFactor += Math.Pow((BetaFactorPerGain * BetaFactorMulti), BetaFactorExp); // (BetaFactorPerGain * BetaFactorMulti) ^ BetaFactorExp という計算
    }

    public void SubBetaFactor(double num)
    {
        _betaFactor -= num;
        BetaFactorSyncer();
    }

    private void BetaFactorSyncer() // BetaFactorForDisplayの値を_betaFactorの値と対応させる関数
    {
        BetaFactorForDisplay = (short)_betaFactor;
        Debug.Log("BetaFactorForDisplay: " + BetaFactorForDisplay);
    }

    public void AddBetaNum() // BetaNumの値を増加させる関数
    {
        BetaNum += BetaNumPerGain;
        Debug.Log("BetaNum: " + BetaNum);
    }

    public void AddBetaFactorPerGain(double num) // numの値をBetaFactorPerGainに掛けて値を上昇させる関数
    {
        BetaFactorPerGain *= num;
        Debug.Log("BetaFactorPerGain: " + BetaFactorPerGain);
    }

    public void AddBetaFactorMulti(double num) // numの値をBetaFactorMultiに掛けて値を上昇させる関数
    {
        BetaFactorMulti *= num;
        Debug.Log("BetaFactorMulti: " + BetaFactorMulti);
    }

    public void AddBetaFactorExp(double num) // numの値をBetaFactorExpに掛けて値を上昇させる関数
    {
        BetaFactorExp *= num;
        Debug.Log("BetaFactorExp: " + BetaFactorExp);
    }

    // Start is called before the first frame update
    void Start()
    {
        CollapseOrRestoreAlpha(true); // debug
    }
}
