using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class FactorsAutoGain : MonoBehaviour
{
    async void Start()
    {
        // ���̃I�u�W�F�N�g���j�����ꂽ�Ƃ��ɃL�����Z�������g�[�N�����擾
        var ct = this.GetCancellationTokenOnDestroy();
        try
        {
            // �L�����Z�������܂Łi���I�u�W�F�N�g���j�������܂Łj���[�v
            while (!ct.IsCancellationRequested)
            {
                if(BetaMissionManager.Instance.IsBetaMission2NowExecuting) // BetaMission2�ɒ��풆�Ȃ�
                {
                    BetaMission2Executing();
                }
                // �܂��A�b�v�O���[�h����������܂őҋ@����
                // (���ł�true�Ȃ瑦���ɒʉ߂��܂�)
                await UniTask.WaitUntil(() => BetaUpgradeManager.Instance.IsUpgrade5Completed, cancellationToken: ct);

                // --- �A�b�v�O���[�h���L���ȊԂ̏��� ---

                // 1�b�ҋ@����
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: ct);

                // 1�b�҂�����A�܂��A�b�v�O���[�h���L�����m�F���Ă��珈�������s
                // (Delay����false�ɂȂ����ꍇ��h��)
                if (BetaUpgradeManager.Instance.IsUpgrade5Completed)
                {
                    GameManager.Instance.UpdateAlphaFactor();
                }
            }
        }
        catch (OperationCanceledException)
        {
            // ���������I��
        }
    }

    async void BetaMission2Executing() // AF�𖈕b�����ɂ��鏈��
    {
        if(BetaMissionManager.Instance.IsBetaMission2NowExecuting)
        {
            // ���̃I�u�W�F�N�g���j�����ꂽ�Ƃ��ɃL�����Z�������g�[�N�����擾
            var ct = this.GetCancellationTokenOnDestroy();
            try
            {
                // �L�����Z�������܂Łi���I�u�W�F�N�g���j�������܂Łj���[�v
                while (!ct.IsCancellationRequested)
                {
                    // --- ����2���L���ȊԂ̏��� ---
                    // 0.1�b�ҋ@����
                    await UniTask.Delay(TimeSpan.FromSeconds(0.1), cancellationToken: ct);
                    // 0.1�b�҂�����A�܂�����2���L�����m�F���Ă��珈�������s
                    // (Delay����false�ɂȂ����ꍇ��h��)
                    if (BetaMissionManager.Instance.IsBetaMission2NowExecuting)
                    {
                        GameManager.Instance.AlphaFactorDecreaser(2); // AF��0.1�b���Ƃɔ����ɂ���
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // ���������I��
            }
        }
        else
        {
            return; // �I��
        }
    }
}
