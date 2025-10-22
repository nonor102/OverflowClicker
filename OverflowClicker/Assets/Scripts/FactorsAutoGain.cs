using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class FactorsAutoGain : MonoBehaviour
{
    async void Start()
    {
        // このオブジェクトが破棄されたときにキャンセルされるトークンを取得
        var ct = this.GetCancellationTokenOnDestroy();

        try
        {
            // キャンセルされるまで（＝オブジェクトが破棄されるまで）ループ
            while (!ct.IsCancellationRequested)
            {
                // まずアップグレードが完了するまで待機する
                // (すでにtrueなら即座に通過します)
                await UniTask.WaitUntil(() => BetaUpgradeManager.Instance.IsUpgrade5Completed, cancellationToken: ct);

                // --- アップグレードが有効な間の処理 ---

                // 1秒待機する
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: ct);

                // 1秒待った後、まだアップグレードが有効か確認してから処理を実行
                // (Delay中にfalseになった場合を防ぐ)
                if (BetaUpgradeManager.Instance.IsUpgrade5Completed)
                {
                    GameManager.Instance.UpdateAlphaFactor();
                }
            }
        }
        catch (OperationCanceledException)
        {
            // 何もせず終了
        }
    }
}
