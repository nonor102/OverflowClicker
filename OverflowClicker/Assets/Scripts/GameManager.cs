using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    // sbyte�p�̒萔�A�ϐ��A�搔����
    private double _sbyteScore = 0;
    public sbyte SbyteScoreForDisplay { get; private set; } = 0;
    public double AddSbyteScorePerClick { get; private set; } = 1; // �N���b�N���Ƃ̑����X�R�A
    public double SbyteScoreMulti { get; private set; } = 1; // �搔
    public double SbyteScoreExp { get; private set; } = 1; // �w��
    public bool IsSbyteOverflowCollapsed { get; private set; } = false; // sbyte�̃I�[�o�[�t���[��collapse�������ǂ����t���O
    public double SbyteOverflowCount { get; private set; } = 0; // sbyte���I�[�o�[�t���[������

    // short�p�̒萔�A�ϐ��A�搔����


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
        if (!IsSbyteOverflowCollapsed) // sbyte��collapse�t���O�������Ă��Ȃ��Ƃ�
        {
            if (SbyteScoreForDisplay >= 0)
            {
                AddSbyteScore();
            }
            else
            {
                SbyteOverflowCount = 1;
                _sbyteScore = 128; // �I�[�o�[�t���[����ƕ\����̒l��-128�ɌŒ肷��
            }
        }
        else
        {
            sbyte beforeValue = SbyteScoreForDisplay; // �{�^�����������O�̒l�������Ă���(�I�[�o�[�t���[���m�p)
            SbyteValueSyncer(); // SbyteScore�̒l���X�V���Ă���
            AddSbyteScore();
            if (SbyteScoreForDisplay < 0 && beforeValue > 0) // SbyteScoreForDisplay�̒l�����̐����畉�̐��֕ς������
            {
                SbyteOverflowCount++;
            }
        }
        SbyteValueSyncer();
        Debug.Log("_sbyteScore: " + _sbyteScore);
        Debug.Log("SbyteOverFlowCount: " + SbyteOverflowCount);
    }

    public void AddSbyteScore() // _sbyteScore�̒l�𑝉�������֐�
    {
        _sbyteScore += Math.Pow((AddSbyteScorePerClick * SbyteScoreMulti), SbyteScoreExp); // (AddSbyteScorePerClick * SbyteScoreMulti) ^ SbyteScoreExp �Ƃ����v�Z
    }

    public void AddSbyteScoreMulti(double num) // SbyteScoreMulti�̒l���㏸������֐�
    {
        SbyteScoreMulti += num;
        Debug.Log("SbyteScoreMulti: " + SbyteScoreMulti);
    }

    public void AddSbyteScoreExp(double num) // SbyteScoreExp�̒l���㏸������֐�
    {
        SbyteScoreExp += num;
        Debug.Log("SbyteScoreExp: " + SbyteScoreExp);
    }

    public void CollapseOrRestoreSbyte(bool value) // IsSbyteOverflowCollapsed��TF��ύX����֐�
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

    private void SbyteValueSyncer() // SbyteScore�̒l��_sbyteScore�̒l�ƑΉ�������֐�
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
