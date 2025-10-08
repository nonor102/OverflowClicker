using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    // sbyte用の定数、変数、乗数たち
    private double _sbyteScore = 0;
    public sbyte SbyteScoreForDisplay { get; private set; } = 0;
    public double AddSbyteScorePerClick { get; private set; } = 1; // クリックごとの増加スコア
    public double SbyteScoreMulti { get; private set; } = 1; // 乗数
    public double SbyteScoreExp { get; private set; } = 1; // 指数
    public bool IsSbyteOverflowCollapsed { get; private set; } = false; // sbyteのオーバーフローがcollapseしたかどうかフラグ
    public double SbyteOverflowCount { get; private set; } = 0; // sbyteがオーバーフローした回数

    // short用の定数、変数、乗数たち


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

    public void UpdateSbyteScore()
    {
        if (!IsSbyteOverflowCollapsed) // sbyteのcollapseフラグが立っていないとき
        {
            if (SbyteScoreForDisplay >= 0)
            {
                AddSbyteScore();
            }
            else
            {
                SbyteOverflowCount = 1;
                _sbyteScore = 128; // オーバーフローすると表示上の値で-128に固定する
            }
        }
        else
        {
            sbyte beforeValue = SbyteScoreForDisplay; // ボタンが押される前の値を代入しておく(オーバーフロー検知用)
            SbyteValueSyncer(); // SbyteScoreの値を更新しておく
            AddSbyteScore();
            if (SbyteScoreForDisplay < 0 && beforeValue > 0) // SbyteScoreForDisplayの値が正の数から負の数へ変わったら
            {
                SbyteOverflowCount++;
            }
        }
        SbyteValueSyncer();
        Debug.Log("_sbyteScore: " + _sbyteScore);
        Debug.Log("SbyteOverFlowCount: " + SbyteOverflowCount);
    }

    public void AddSbyteScore() // _sbyteScoreの値を増加させる関数
    {
        _sbyteScore += Math.Pow((AddSbyteScorePerClick * SbyteScoreMulti), SbyteScoreExp); // (AddSbyteScorePerClick * SbyteScoreMulti) ^ SbyteScoreExp という計算
    }

    public void AddSbyteScoreMulti(double num) // SbyteScoreMultiの値を上昇させる関数
    {
        SbyteScoreMulti += num;
        Debug.Log("SbyteScoreMulti: " + SbyteScoreMulti);
    }

    public void AddSbyteScoreExp(double num) // SbyteScoreExpの値を上昇させる関数
    {
        SbyteScoreExp += num;
        Debug.Log("SbyteScoreExp: " + SbyteScoreExp);
    }

    public void CollapseOrRestoreSbyte(bool value) // IsSbyteOverflowCollapsedのTFを変更する関数
    {
        if (value)
        {
            IsSbyteOverflowCollapsed = true;
        }
        else
        {
            IsSbyteOverflowCollapsed = false;
        }
    }

    private void SbyteValueSyncer() // SbyteScoreの値を_sbyteScoreの値と対応させる関数
    {
        SbyteScoreForDisplay = (sbyte)_sbyteScore;
        Debug.Log("SbyteScoreForDisplay: " + SbyteScoreForDisplay);
    }

    // Start is called before the first frame update
    void Start()
    {
        CollapseOrRestoreSbyte(true); // debug
    }

    // Update is called once per frame
    void Update()
    {
        //SbyteValueSyncer();
    }
}
