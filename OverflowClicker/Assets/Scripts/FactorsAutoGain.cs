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
}
