using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionStatus
{
    Locked, // ���b�N��
    Available, // ����\
    Now, // ���풆
    Completed // ����
}

[CreateAssetMenu(fileName = "BetaMission", menuName = "ScriptableObjects/Create Beta Mission")]
public class BetaMission : ScriptableObject
{
    [Header("�����̏��")]
    public int missionID; // ������ID
    [TextArea]
    public string discription; // ������
}
