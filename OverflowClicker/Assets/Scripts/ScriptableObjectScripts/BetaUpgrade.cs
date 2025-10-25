using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BetaUpgradeStatus
{
    Locked, // 未開放
    Available, // 解放可能
    Completed // 解放済み
}

[CreateAssetMenu(fileName = "BetaUpgrade", menuName = "ScriptableObjects/Create Beta Upgrade")]
public class BetaUpgrade : ScriptableObject
{
    [Header("強化の情報")]
    public int upgradeID; // ユニークなID
    public string title; // タイトル
    public double needBetaFactor; // 解放に必要なBetaFactorの数
    [TextArea]
    public string description; // 説明

    [Header("前提となる強化")]
    public List<BetaUpgrade> preRequiredUpgrade;

    //以下、必要な変数を準備
}
