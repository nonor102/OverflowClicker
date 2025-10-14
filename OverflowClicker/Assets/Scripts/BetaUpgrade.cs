using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BetaUpgradeStatus
{
    Locked, // ���J��
    Available, // ����\
    Completed // ����ς�
}

[CreateAssetMenu(fileName = "BetaUpgrade", menuName = "ScriptableObjects/CreateBetaUpgrade")]
public class BetaUpgrade : ScriptableObject
{
    [Header("�����̏��")]
    public string missionID; // ���j�[�N��ID
    public string title; // �^�C�g��
    public double needBetaFactor; // ����ɕK�v��BetaFactor�̐�
    [TextArea]
    public string description; // ����

    [Header("�O��ƂȂ鋭��")]
    public List<BetaUpgrade> preRequiredUpgrade;

    //�ȉ��A�K�v�ȕϐ�������
}
