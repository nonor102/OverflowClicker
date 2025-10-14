using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    // sbyte�p�̒萔�A�ϐ��A�搔����
    // sbyte��AF (Alpha Factor)�Ƃ������O�ŊǗ�����
    private double _alphaFactor = 0; // �v�Z�p��AF
    public sbyte AlphaFactorForDisplay { get; private set; } = 0; // �\���p��AF
    public double AddAlphaFactorPerClick { get; private set; } = 1; // �N���b�N���Ƃ̑���AF
    public double AlphaFactorMulti { get; private set; } = 1; // AF�l���搔
    public double AlphaFactorExp { get; private set; } = 1; // AF�l���w��
    public bool IsAlphaOverflowCollapsed { get; private set; } = false; // Alpha�̃I�[�o�[�t���[��collapse�������ǂ����t���O
    public double AlphaOverflowCount { get; private set; } = 0; // Alpha���I�[�o�[�t���[������

    // short�p�̒萔�A�ϐ��A�搔����
    // short��BF (Beta Factor)�Ƃ������O�ŊǗ�����
    private double _betaFactor = 0; // �v�Z�p��BF
    public short BetaFactorForDisplay { get; private set; } = 0; // �\���p��BF
    public double BetaNum { get; private set; } = 0; // alpha2beta�̉�
    public double BetaNumPerGain {  get; private set; } = 1; // BetaNum�𑝂₷��
    public double AddBetaFactorPerGain { get; private set; } = 1; // �擾����BF�̊�{�̐�
    public double BetaFactorMulti { get; private set; } = 1; // BF�l���搔
    public double BetaFactorExp { get; private set; } = 1; // BF�l���w��
    public bool IsBetaOverflowCollapsed { get; private set; } = false; // Beta�̃I�[�o�[�t���[��collapse�������ǂ����t���O
    public double BetaOverflowCount { get; private set; } = 0; // Beta���I�[�o�[�t���[������

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
        if (!IsAlphaOverflowCollapsed) // alpha��collapse�t���O�������Ă��Ȃ��Ƃ�
        {
            if (AlphaFactorForDisplay >= 0)
            {
                AddAlphaFactor();
            }
            else
            {
                AlphaOverflowCount = 1;
                _alphaFactor = 128; // �I�[�o�[�t���[����ƕ\����̒l��-128�ɌŒ肷��
            }
        }
        else
        {
            sbyte beforeValue = AlphaFactorForDisplay; // �{�^�����������O�̒l�������Ă���(�I�[�o�[�t���[���m�p)
            AddAlphaFactor();
            AlphaFactorSyncer(); // AlphaFactor�̒l���X�V���Ă���
            if (AlphaFactorForDisplay < 0 && beforeValue > 0) // AlphaFactorForDisplay�̒l�����̐����畉�̐��֕ς������
            {
                AlphaOverflowCount++;
                Debug.Log("Alpha Overflow: " + AlphaOverflowCount);
            }
        }
        AlphaFactorSyncer();
    }

    public void AddAlphaFactor() // _alphaFactor�̒l�𑝉�������֐�
    {
        _alphaFactor += Math.Pow((AddAlphaFactorPerClick * AlphaFactorMulti), AlphaFactorExp); // (AddAlphaFactorPerClick * AlphaFactorMulti) ^ AlphaFactorExp �Ƃ����v�Z
    }

    public void AddAlphaFactorMulti(double num) // AlphaFactorMulti�̒l���㏸������֐�
    {
        AlphaFactorMulti += num;
        Debug.Log("AlphaFactorMulti: " + AlphaFactorMulti);
    }

    public void AddAlphaFactorExp(double num) // AlphaFactorExp�̒l���㏸������֐�
    {
        AlphaFactorExp += num;
        Debug.Log("AlphaFactorExp: " + AlphaFactorExp);
    }

    public void CollapseOrRestoreAlpha(bool value) // IsAlphaOverflowCollapsed��TF��ύX����֐�
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

    private void AlphaFactorSyncer() // AlphaFactorForDisplay�̒l��_alphaFactor�̒l�ƑΉ�������֐�
    {
        AlphaFactorForDisplay = (sbyte)_alphaFactor;
        Debug.Log("AlphaFactorForDisplay: " + AlphaFactorForDisplay);
    }

    public void Alpha2Beta() // alpha�����Z�b�g����beta���擾����֐�
    {
        AddBetaFactor();
        AddBetaNum();
        BetaFactorSyncer();
        _alphaFactor = 0;
        AlphaOverflowCount = 0;
        AlphaFactorSyncer();
        Debug.Log("alpha2beta");
    }

    public void AddBetaFactor() // _betaFactor�̒l�𑝉�������֐�
    {
        _betaFactor += Math.Pow((AddBetaFactorPerGain * BetaFactorMulti), BetaFactorExp); // (AddBetaFactorPerGain * BetaFactorMulti) ^ BetaFactorExp �Ƃ����v�Z
    }

    private void BetaFactorSyncer() // BetaFactorForDisplay�̒l��_betaFactor�̒l�ƑΉ�������֐�
    {
        BetaFactorForDisplay = (short)_betaFactor;
        Debug.Log("BetaFactorForDisplay: " + BetaFactorForDisplay);
    }

    public void AddBetaNum()
    {
        BetaNum += BetaNumPerGain;
    }

    // Start is called before the first frame update
    void Start()
    {
        CollapseOrRestoreAlpha(true); // debug
    }

    // Update is called once per frame
    void Update()
    {
        //AlphaFactorSyncer();
    }
}
