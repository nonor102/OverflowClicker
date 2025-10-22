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

    private void VaridateAmplificationToggle(bool newValue) // �����^�u���J���֐�
    {
        if(newValue == true) // �^�u���J���Ȃ�true (����ꍇ�͉����֗^����������悤��)
        {
            if (!BetaUpgradeManager.Instance.IsUpgrade7Completed) // �������Ă邩
            {
                Debug.LogWarning("cant open amplfication tab");
                amplification.SetIsOnWithoutNotify(false); // unity�C�x���g�Ȃ���IsOn��false��
            }
        }
    }

    private void VaridateMissionToggle(bool newValue) // �����^�u���J���֐�
    {
        if (newValue == true) // �^�u���J���Ȃ�true (����ꍇ�͉����֗^����������悤��)
        {
            if (!BetaUpgradeManager.Instance.IsUpgrade8Completed) // �������Ă邩
            {
                Debug.LogWarning("cant open mission tab");
                mission.SetIsOnWithoutNotify(false); // unity�C�x���g�Ȃ���IsOn��false��
            }
        }
    }

    private void VaridateBankToggle(bool newValue) // ��s�^�u���J���֐�
    {
        if (newValue == true) // �^�u���J���Ȃ�true (����ꍇ�͉����֗^����������悤��)
        {
            if (!BetaUpgradeManager.Instance.IsUpgrade9Completed) // �������Ă邩
            {
                Debug.LogWarning("cant open bank tab");
                bank.SetIsOnWithoutNotify(false); // unity�C�x���g�Ȃ���IsOn��false��
            }
        }
    }

    private void VaridateRevolutionToggle(bool newValue) // �v���^�u���J���֐�
    {
        if (newValue == true) // �^�u���J���Ȃ�true (����ꍇ�͉����֗^����������悤��)
        {
            if (!BetaUpgradeManager.Instance.IsUpgrade10Completed) // �������Ă邩
            {
                Debug.LogWarning("cant open revolution tab");
                revolution.SetIsOnWithoutNotify(false); // unity�C�x���g�Ȃ���IsOn��false��
            }
        }
    }
}
