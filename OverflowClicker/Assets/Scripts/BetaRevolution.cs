using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetaRevolution : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI warningText;

    private double RevolutionExpnum = 0;

    void Start()
    {
        warningText.color = Color.black;
    }

    void Update()
    {
        RevolutionExpnum = 1 + Mathf.Sqrt((float)GameManager.Instance.AllBetaFactorGetInThisTerm) * 0.001; // ���l��BF�̃��[�g���Ƃ���0.001�{�������̂�1�Ƒ����ċ����ʂƂ���
        descriptionText.text = "AF��BF�̊l���ʂ� " + $"{RevolutionExpnum:F2}" + " ��ɂ���";
        warningText.text = "!�x��!" + "\n" + "�v�������s����ƁAAF�ABF�̎w���ȊO�̑S�Ă̗v�f������������܂��B" + "\n" + "�v���͎w���̒l���㏑�����܂��B���s����ƍ������キ�Ȃ邱�Ƃ�����܂��B";
    }

    public void OnSubmitRevolution() // �v���̎��s�{�^���������ꂽ��
    {
        GameManager.Instance.ResetBetaByRevolution();
        BetaUpgradeManager.Instance.ResetAllUpgrades();
        BetaMissionManager.Instance.ResetAllMissions();
        BetaBankManager.Instance.ResetBank();

        GameManager.Instance.SetAlphaFactorExp(RevolutionExpnum);
    }
}
