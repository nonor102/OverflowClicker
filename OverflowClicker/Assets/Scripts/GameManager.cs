using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;

    // sbyte�p�̒萔�A�ϐ��A�搔����
    // sbyte��AF (Alpha Factor)�Ƃ������O�ŊǗ�����
    public double AlphaFactorForCalc = 0; // �v�Z�p��AF
    public sbyte AlphaFactorForDisplay { get; private set; } = 0; // �\���p��AF
    public double AlphaFactorPerClick { get; private set; } = 1; // �N���b�N���Ƃ̑���AF
    public double AlphaFactorMulti { get; private set; } = 1; // AF�l���搔
    public double AlphaFactorExp { get; private set; } = 1; // AF�l���w��
    public bool IsAlphaOverflowCollapsed { get; private set; } = false; // Alpha�̃I�[�o�[�t���[��collapse�������ǂ����t���O
    public double AlphaOverflowCount { get; private set; } = 0; // Alpha���I�[�o�[�t���[������

    // short�p�̒萔�A�ϐ��A�搔����
    // short��BF (Beta Factor)�Ƃ������O�ŊǗ�����
    public bool IsArrivedBeta { get; private set; } = false; // Beta�ɓ��B�������t���O
    public double BetaFactorForCalc = 0; // �v�Z�p��BF
    public short BetaFactorForDisplay { get; private set; } = 0; // �\���p��BF
    public double BetaNum { get; private set; } = 0; // alpha2beta�̉�
    public double BetaNumPerGain {  get; private set; } = 1; // BetaNum�𑝂₷��
    public double BetaFactorPerGain { get; private set; } = 1; // �擾����BF�̊�{�̐�
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

    private void Start()
    {
        AlphaFactorSyncer();
        BetaFactorSyncer();
    }

    public void InitializeDataFromJson(SaveData saveData) // json�̃f�[�^�������
    {
        AlphaFactorForCalc = saveData.AlphaFactorForCalc;
        IsAlphaOverflowCollapsed = saveData.IsAlphaOverflowCollapsed;
        AlphaOverflowCount = saveData.AlphaOverflowCount;

        IsArrivedBeta = saveData.IsArrivedBeta;
        BetaFactorForCalc = saveData.BetaFactorForCalc;
        BetaNum = saveData.BetaNum;
        IsBetaOverflowCollapsed = saveData.IsBetaOverflowCollapsed;
        BetaOverflowCount = saveData.BetaOverflowCount;

        AlphaFactorSyncer();
        BetaFactorSyncer();

        Debug.Log("data loaded");
    }

    public void UpdateAlphaFactor()
    {
        sbyte beforeValue = AlphaFactorForDisplay; // �{�^�����������O�̒l�������Ă���(�I�[�o�[�t���[���m�p)
        if (!IsAlphaOverflowCollapsed) // alpha��collapse�t���O�������Ă��Ȃ��Ƃ�
        {
            if (AlphaFactorForDisplay >= 0)
            {
                AddAlphaFactor();
            }
        }
        else
        {
            AddAlphaFactor();
            AlphaFactorSyncer(); // AlphaFactor�̒l���X�V���Ă���
            if (AlphaFactorForDisplay < 0 && beforeValue > 0) // AlphaFactorForDisplay�̒l�����̐����畉�̐��֕ς������
            {
                AlphaOverflowCount++;
                Debug.Log("Alpha Overflow: " + AlphaOverflowCount);
            }
        }
        AlphaFactorSyncer();

        if (!IsAlphaOverflowCollapsed)
        {
            if (AlphaFactorForDisplay < 0 && beforeValue >= 0)
            {
                AlphaOverflowCount = 1;
                AlphaFactorForCalc = 128; // �I�[�o�[�t���[����ƕ\����̒l��-128�ɌŒ肷��
                Debug.Log("Alpha Overflowed");
            }
            else if (AlphaFactorForDisplay < 0)
            {
                AlphaOverflowCount = 1; // �Œ�
                AlphaFactorForCalc = 128; // �Œ�
            }
        }
        AlphaFactorSyncer();
    }

    public void AddAlphaFactor() // AlphaFactorForCalc�̒l�𑝉�������֐�
    {
        AlphaFactorForCalc += Math.Pow((AlphaFactorPerClick * AlphaFactorMulti), AlphaFactorExp); // (AlphaFactorPerClick * AlphaFactorMulti) ^ AlphaFactorExp �Ƃ����v�Z
    }

    public void AddAlphaFactorPerClick(double num) // num�̒l��AlphaFactorPerClick�Ɋ|���Ēl���㏸������֐�
    {
        AlphaFactorPerClick *= num;
        Debug.Log("AlphaFactorPerCiick: " + AlphaFactorPerClick);
    }

    public void AddAlphaFactorMulti(double num) // num�̒l��AlphaFactorMulti�Ɋ|���Ēl���㏸������֐�
    {
        AlphaFactorMulti *= num;
        Debug.Log("AlphaFactorMulti: " + AlphaFactorMulti);
    }

    public void AddAlphaFactorExp(double num) // num�̒l��AlphaFactorExp�Ɋ|���Ēl���㏸������֐�
    {
        AlphaFactorExp *= num;
        Debug.Log("AlphaFactorExp: " + AlphaFactorExp);
    }

    public void CollapseAlpha() // IsAlphaOverflowCollapsed��T�ɕύX����֐�
    {
        IsAlphaOverflowCollapsed = true;
    }

    private void AlphaFactorSyncer() // AlphaFactorForDisplay�̒l��AlphaFactor�̒l�ƑΉ�������֐�
    {
        AlphaFactorForDisplay = (sbyte)AlphaFactorForCalc;
        Debug.Log("AlphaFactorForDisplay: " + AlphaFactorForDisplay);
    }

    public void Alpha2Beta() // alpha�����Z�b�g����beta���擾����֐�
    {
        IsArrivedBeta = true;
        AddBetaFactor();
        AddBetaNum();
        BetaFactorSyncer();
        AlphaFactorForCalc = 0;
        AlphaOverflowCount = 0;
        AlphaFactorSyncer();
        Debug.Log("alpha2beta");
    }

    public void AddBetaFactor() // BetaFactor�̒l�𑝉�������֐�
    {
        BetaFactorForCalc += Math.Pow((BetaFactorPerGain * BetaFactorMulti), BetaFactorExp); // (BetaFactorPerGain * BetaFactorMulti) ^ BetaFactorExp �Ƃ����v�Z
    }

    public void SubBetaFactor(double num)
    {
        BetaFactorForCalc -= num;
        BetaFactorSyncer();
    }

    private void BetaFactorSyncer() // BetaFactorForDisplay�̒l��BetaFactor�̒l�ƑΉ�������֐�
    {
        BetaFactorForDisplay = (short)BetaFactorForCalc;
        Debug.Log("BetaFactorForDisplay: " + BetaFactorForDisplay);
    }

    public void AddBetaNum() // BetaNum�̒l�𑝉�������֐�
    {
        BetaNum += BetaNumPerGain;
        Debug.Log("BetaNum: " + BetaNum);
    }

    public void AddBetaFactorPerGain(double num) // num�̒l��BetaFactorPerGain�Ɋ|���Ēl���㏸������֐�
    {
        BetaFactorPerGain *= num;
        Debug.Log("BetaFactorPerGain: " + BetaFactorPerGain);
    }

    public void AddBetaFactorMulti(double num) // num�̒l��BetaFactorMulti�Ɋ|���Ēl���㏸������֐�
    {
        BetaFactorMulti *= num;
        Debug.Log("BetaFactorMulti: " + BetaFactorMulti);
    }

    public void AddBetaFactorExp(double num) // num�̒l��BetaFactorExp�Ɋ|���Ēl���㏸������֐�
    {
        BetaFactorExp *= num;
        Debug.Log("BetaFactorExp: " + BetaFactorExp);
    }
}
