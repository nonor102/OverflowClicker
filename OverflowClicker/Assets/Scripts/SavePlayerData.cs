using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;

[Serializable]
public class SaveData
{
    public double AlphaFactorForCalc;
    public bool IsAlphaOverflowCollapsed;
    public double AlphaOverflowCount;

    public bool IsArrivedBeta;
    public double BetaFactorForCalc;
    public double BetaNum;
    public double BetaFactorUsedInAmplification;
    public bool IsBetaOverflowCollapsed;
    public double BetaOverflowCount;

    public bool IsUpgrade0Completed;
    public bool IsUpgrade5Completed;
    public bool IsUpgrade6Completed;
    public bool IsUpgrade7Completed;
    public bool IsUpgrade8Completed;
    public bool IsUpgrade9Completed;
    public bool IsUpgrade10Completed;
    public bool IsUpgrade11Completed;

    public int LatestCompletedMissionID;

    public SaveData()
    {
        AlphaFactorForCalc = 0.0;
        AlphaOverflowCount = 0.0;

        BetaFactorForCalc = 0.0;
        BetaNum = 0.0;
        BetaOverflowCount = 0.0;

        LatestCompletedMissionID = 0;
    }
}

[Serializable]
public class BetaUpgradeClassWrapper
{
    public List<int> CompleteBetaUpgradeIDs;
}

[Serializable]
public class GameSaveData
{
    public SaveData mainData;
    public BetaUpgradeClassWrapper betaUpgradeData;
}

public class SavePlayerData : MonoBehaviour
{
    public SaveData Data { get; private set; }
    public BetaUpgradeClassWrapper BetaWrapper { get; private set; }

    private string _saveDataFilePath;
    private string _saveDataFileName = "PlayerData.json";

    private void Awake()
    {
        Data = new SaveData();
        BetaWrapper = new BetaUpgradeClassWrapper();
        BetaWrapper.CompleteBetaUpgradeIDs = new List<int>();

        _saveDataFilePath = Path.Combine(Application.persistentDataPath, _saveDataFileName);

        if (File.Exists(_saveDataFilePath))
        {
            Load();
        }
    }

    private void Start()
    {
        if (!File.Exists(_saveDataFilePath))
        {
            Save();
        }
        GameManager.Instance.InitializeDataFromJson(Data);
        BetaMissionManager.Instance.SetMissionsFromSaveData(Data);

        BetaUpgradeManager.Instance.InitializeFromSaveData(BetaWrapper.CompleteBetaUpgradeIDs);
    }

    private void Save() // json�ɂ���
    {
        GameSaveData gameSaveData = new GameSaveData
        {
            mainData = Data,
            betaUpgradeData = BetaWrapper
        };

        string json = JsonUtility.ToJson(gameSaveData, true);

        File.WriteAllText(_saveDataFilePath, json);
    }

    private void Load() // json������
    {
        string json = File.ReadAllText(_saveDataFilePath);

        GameSaveData loadedData = JsonUtility.FromJson<GameSaveData>(json);

        Data = loadedData.mainData;
        BetaWrapper = loadedData.betaUpgradeData;

        if (Data == null) Data = new SaveData();
        if (BetaWrapper == null)
        {
            BetaWrapper = new BetaUpgradeClassWrapper();
            BetaWrapper.CompleteBetaUpgradeIDs = new List<int>();
        }
    }

    public void SaveUserData()
    {
        SaveVariables(Data, BetaWrapper);
        Save();
        Debug.Log("userdata is saved to: " + _saveDataFilePath);
    }

    private void SaveVariables(SaveData saveData, BetaUpgradeClassWrapper betaUpgradeClassWrapper) // �f�[�^��GameManager�Ƃ�����Ƃ��Ă���
    {
        saveData.AlphaFactorForCalc = GameManager.Instance.AlphaFactorForCalc;
        saveData.IsAlphaOverflowCollapsed = GameManager.Instance.IsAlphaOverflowCollapsed;
        saveData.AlphaOverflowCount = GameManager.Instance.AlphaOverflowCount;

        saveData.IsArrivedBeta = GameManager.Instance.IsArrivedBeta;
        saveData.BetaFactorForCalc = GameManager.Instance.BetaFactorForCalc;
        saveData.BetaNum = GameManager.Instance.BetaNum;
        saveData.BetaFactorUsedInAmplification = GameManager.Instance.BetaFactorUsedInAmplification;
        saveData.IsBetaOverflowCollapsed = GameManager.Instance.IsBetaOverflowCollapsed;
        saveData.BetaOverflowCount = GameManager.Instance.BetaOverflowCount;

        saveData.IsUpgrade0Completed = BetaUpgradeManager.Instance.IsUpgrade0Completed;
        saveData.IsUpgrade5Completed = BetaUpgradeManager.Instance.IsUpgrade5Completed;
        saveData.IsUpgrade6Completed = BetaUpgradeManager.Instance.IsUpgrade6Completed;
        saveData.IsUpgrade7Completed = BetaUpgradeManager.Instance.IsUpgrade7Completed;
        saveData.IsUpgrade8Completed = BetaUpgradeManager.Instance.IsUpgrade8Completed;
        saveData.IsUpgrade9Completed = BetaUpgradeManager.Instance.IsUpgrade9Completed;
        saveData.IsUpgrade10Completed = BetaUpgradeManager.Instance.IsUpgrade10Completed;

        saveData.LatestCompletedMissionID = BetaMissionManager.Instance.LatestCompletedMissionID;

        // BetaUpgradeStatuses �f�B�N�V���i���̒�����A
        // �l(Value)�� Completed �ł�����̂����𒊏o��(Where)�A
        // ���̃L�[(Key)������I������(Select)�A
        // �V�������X�g�Ƃ���(ToList)�������B
        betaUpgradeClassWrapper.CompleteBetaUpgradeIDs = BetaUpgradeManager.Instance.BetaUpgradeStatuses
            .Where(pair => pair.Value == BetaUpgradeStatus.Completed)
            .Select(pair => pair.Key)
            .ToList();
    }
}