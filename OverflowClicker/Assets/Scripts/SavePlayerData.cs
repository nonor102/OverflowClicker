using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class SaveData
{
    public double AlphaFactor;
    public double AlphaFactorPerClick;
    public double AlphaFactorMulti;
    public double AlphaFactorExp;
    public bool IsAlphaOverflowCollapsed;
    public double AlphaOverflowCount;

    public bool IsArrivedBeta;
    public double BetaFactor;
    public double BetaNum;
    public double BetaNumPerGain;
    public double BetaFactorPerGain;
    public double BetaFactorMulti;
    public double BetaFactorExp;
    public bool IsBetaOverflowCollapsed;
    public double BetaOverflowCount;

    public bool IsUpgrade0Completed;
    public bool IsUpgrade5Completed;
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
    private SaveData _data;
    private BetaUpgradeClassWrapper _betaUpgradeClassWrapper;
    private string _saveDataFilePath;
    private string _saveDataFileName = "PlayerData.json";

    private void Awake()
    {
        _data = new SaveData();
        _betaUpgradeClassWrapper = new BetaUpgradeClassWrapper();
        _betaUpgradeClassWrapper.CompleteBetaUpgradeIDs = new List<int>();

        _saveDataFilePath = Path.Combine(Application.persistentDataPath, _saveDataFileName);

        if (File.Exists(_saveDataFilePath))
        {
            Load();
        }
        else
        {
            Save();
        }
    }

    private void Save() // jsonÇ…Ç∑ÇÈ
    {
        GameSaveData gameSaveData = new GameSaveData
        {
            mainData = _data,
            betaUpgradeData = _betaUpgradeClassWrapper
        };

        string json = JsonUtility.ToJson(gameSaveData, true);

        File.WriteAllText(_saveDataFilePath, json);
    }

    private void Load() // jsonÇ©ÇÁÇÊÇﬁ
    {
        string json = File.ReadAllText(_saveDataFilePath);

        GameSaveData loadedData = JsonUtility.FromJson<GameSaveData>(json);

        _data = loadedData.mainData;
        _betaUpgradeClassWrapper = loadedData.betaUpgradeData;

        if (_data == null) _data = new SaveData();
        if (_betaUpgradeClassWrapper == null)
        {
            _betaUpgradeClassWrapper = new BetaUpgradeClassWrapper();
            _betaUpgradeClassWrapper.CompleteBetaUpgradeIDs = new List<int>();
        }
    }

    public void SaveUserData()
    {
        SaveVariables(_data, _betaUpgradeClassWrapper);
        Save();
        Debug.Log("userdata is saved to: " + _saveDataFilePath);
    }

    private void SaveVariables(SaveData saveData, BetaUpgradeClassWrapper betaUpgradeClassWrapper) // ÉfÅ[É^ÇGameManagerÇ∆Ç©Ç©ÇÁÇ∆Ç¡ÇƒÇ≠ÇÈ
    {
        saveData.AlphaFactor = GameManager.Instance.AlphaFactorForCalc;
        saveData.AlphaFactorPerClick = GameManager.Instance.AlphaFactorPerClick;
        saveData.AlphaFactorMulti = GameManager.Instance.AlphaFactorMulti;
        saveData.AlphaFactorExp = GameManager.Instance.AlphaFactorExp;
        saveData.IsAlphaOverflowCollapsed = GameManager.Instance.IsAlphaOverflowCollapsed;
        saveData.AlphaOverflowCount = GameManager.Instance.AlphaOverflowCount;

        saveData.IsArrivedBeta = GameManager.Instance.IsArrivedBeta;
        saveData.BetaFactor = GameManager.Instance.BetaFactorForCalc;
        saveData.BetaNum = GameManager.Instance.BetaNum;
        saveData.BetaNumPerGain = GameManager.Instance.BetaNumPerGain;
        saveData.BetaFactorPerGain = GameManager.Instance.BetaFactorPerGain;
        saveData.BetaFactorMulti = GameManager.Instance.BetaFactorMulti;
        saveData.BetaFactorExp = GameManager.Instance.BetaFactorExp;
        saveData.IsBetaOverflowCollapsed = GameManager.Instance.IsBetaOverflowCollapsed;
        saveData.BetaOverflowCount = GameManager.Instance.BetaOverflowCount;

        saveData.IsUpgrade0Completed = BetaUpgradeManager.Instance.IsUpgrade0Completed;
        saveData.IsUpgrade5Completed = BetaUpgradeManager.Instance.IsUpgrade5Completed;

        betaUpgradeClassWrapper.CompleteBetaUpgradeIDs.Clear();
        betaUpgradeClassWrapper.CompleteBetaUpgradeIDs.AddRange(
            BetaUpgradeManager.Instance.BetaUpgradeStatuses.Keys
        );
    }
}