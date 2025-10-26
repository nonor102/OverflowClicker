using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BetaMissionStatus
{
    Locked, // ロック中
    Available, // 挑戦可能
    Now, // 挑戦中
    Completed // 完了
}

[CreateAssetMenu(fileName = "BetaMission", menuName = "ScriptableObjects/Create Beta Mission")]
public class BetaMission : ScriptableObject
{
    [Header("試練の情報")]
    public int missionID; // 試練のID

    [TextArea]
    public string description; // 説明文

    [Header("前提条件")]
    public BetaMission preRequiredMissions; // 前提条件(これを満たさないと状態をLockedに、満たすとAvailableに)
}
