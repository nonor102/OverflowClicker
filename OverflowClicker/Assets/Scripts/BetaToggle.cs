using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetaToggle : MonoBehaviour
{
    [SerializeField] private Toggle amplification;
    [SerializeField] private Toggle mission;
    [SerializeField] private Toggle bank;
    [SerializeField] private Toggle revolution;

    void Start()
    {
        amplification.onValueChanged.AddListener(VaridateAmplificationToggle);
        mission.onValueChanged.AddListener(VaridateMissionToggle);
        bank.onValueChanged.AddListener(VaridateBankToggle);
        revolution.onValueChanged.AddListener(VaridateRevolutionToggle);
    }

    private void VaridateAmplificationToggle(bool newValue) // 増幅タブを開く関数
    {
        if(newValue == true) // タブを開くならtrue (閉じる場合は何も関与せず閉じられるように)
        {
            if (!BetaUpgradeManager.Instance.IsUpgrade7Completed) // 解放されてるか
            {
                Debug.LogWarning("cant open amplfication tab");
                amplification.SetIsOnWithoutNotify(false); // unityイベントなしでIsOnをfalseに
            }
        }
    }

    private void VaridateMissionToggle(bool newValue) // 試練タブを開く関数
    {
        if (newValue == true) // タブを開くならtrue (閉じる場合は何も関与せず閉じられるように)
        {
            if (!BetaUpgradeManager.Instance.IsUpgrade8Completed) // 解放されてるか
            {
                Debug.LogWarning("cant open mission tab");
                mission.SetIsOnWithoutNotify(false); // unityイベントなしでIsOnをfalseに
            }
        }
    }

    private void VaridateBankToggle(bool newValue) // 銀行タブを開く関数
    {
        if (newValue == true) // タブを開くならtrue (閉じる場合は何も関与せず閉じられるように)
        {
            if (!BetaUpgradeManager.Instance.IsUpgrade9Completed) // 解放されてるか
            {
                Debug.LogWarning("cant open bank tab");
                bank.SetIsOnWithoutNotify(false); // unityイベントなしでIsOnをfalseに
            }
        }
    }

    private void VaridateRevolutionToggle(bool newValue) // 革命タブを開く関数
    {
        if (newValue == true) // タブを開くならtrue (閉じる場合は何も関与せず閉じられるように)
        {
            if (!BetaUpgradeManager.Instance.IsUpgrade10Completed) // 解放されてるか
            {
                Debug.LogWarning("cant open revolution tab");
                revolution.SetIsOnWithoutNotify(false); // unityイベントなしでIsOnをfalseに
            }
        }
    }
}
