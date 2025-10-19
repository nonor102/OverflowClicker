using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FactorAutoGain : MonoBehaviour
{
    void Update()
    {
        if (BetaUpgradeManager.Instance.IsUpgrade5Completed)
        {
            _ = AFAutoGain(1, () => GameManager.Instance.AddAlphaFactor()); // 1•b‚²‚Æ‚ÉAFŠl“¾
        }
    }

    private async UniTask AFAutoGain(float time, Action action) // AFŽ©“®‰»
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        action();
    }
}
