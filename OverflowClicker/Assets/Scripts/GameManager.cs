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

    public void AddSbyteScore()
    {
        _sbyteScore += Math.Pow((AddSbyteScorePerClick * SbyteScoreMulti), SbyteScoreExp);
        Debug.Log("_sbyteScore: " + _sbyteScore);
    }

    public void AddSbyteScoreMulti(double num)
    {
        SbyteScoreMulti += num;
        Debug.Log("SbyteScoreMulti: " + SbyteScoreMulti);
    }
    public void AddSbyteScoreExp(double num)
    {
        SbyteScoreExp += num;
        Debug.Log("SbyteScoreExp: " + SbyteScoreExp);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SbyteScoreForDisplay = (sbyte)_sbyteScore;
    }
}
