using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BetaMissionUI : MonoBehaviour
{
    [SerializeField] private List<Button> MissionList = new(); // �����̃{�^������Ƃ�
    [SerializeField] private List<TextMeshProUGUI> MissionTitles = new(); // �����̃^�C�g���ɒǋL����e�L�X�g�p��tmp�����
    [SerializeField] private List<TextMeshProUGUI> MissionDescriptions = new(); // �����̐����p��tmp�����
    [SerializeField] private TextMeshProUGUI BetaMissionDescriptionText; // ������ʂ̐����e�L�X�g
    [SerializeField] private Sprite LockPic; // ���O�̉摜
    [SerializeField] private Sprite AvailablePic; // ���ʂ̎��̉摜

    private void Start()
    {
        
    }

    private void Update()
    {
        foreach (var item in BetaMissionManager.Instance.allMissions)
        {
            ColorChange(item);
        }

        BetaMissionDescriptionText.text = "���܂ł̎����̃N���A��: " + BetaMissionManager.Instance.LatestCompletedMissionID + "\n" + "���݁A�����ɂ��" + (BetaMissionManager.Instance.LatestCompletedMissionID + 1) + "�{��BF���l����";
    }

    public void OnButtonClicked(BetaMission betaMission) // �{�^������
    {
        if (BetaMissionManager.Instance.GetBetaMissionStatus(betaMission.missionID) == BetaMissionStatus.Available) // ����\�Ȃ�
        {
            BetaMissionManager.Instance.EnterOrExitBetaMission(betaMission.missionID, true); // ����
        }
        else if (BetaMissionManager.Instance.GetBetaMissionStatus(betaMission.missionID) == BetaMissionStatus.Now) // �����풆�Ȃ�
        {
            BetaMissionManager.Instance.EnterOrExitBetaMission(betaMission.missionID, false); // �������߂�
        }
        else
        {
            return;
        }
        ColorChange(betaMission); // �F�K�p
    }

    public void ColorChange(BetaMission betaMission) // �e�����̐F��ς���֐�
    {
        BetaMissionStatus status = BetaMissionManager.Instance.GetBetaMissionStatus(betaMission.missionID);
        int missionIndex = betaMission.missionID - 1; // �������X�g�̃C���f�b�N�X

        if (missionIndex < 0 || missionIndex >= MissionList.Count)
        {
            Debug.LogError("not found mission");
            return;
        }

        Button missionButton = MissionList[missionIndex];
        Image buttonImage = missionButton.GetComponentInChildren<Image>();
        TextMeshProUGUI titleText = MissionTitles[missionIndex];
        TextMeshProUGUI descriptionText = MissionDescriptions[missionIndex];

        if (status == BetaMissionStatus.Locked)
        {
            buttonImage.sprite = LockPic; // ���b�N���͏��O�̉摜
            buttonImage.color = Color.white; // �摜�̐F
            missionButton.interactable = false; // �����Ȃ��悤�ɂ��Ƃ�
        }
        else if(status == BetaMissionStatus.Available)
        {
            buttonImage.sprite = AvailablePic; // ���b�N�ȊO�Ȃ炽���̔����摜
            buttonImage.color = Color.white; // ����\�Ȃ甒�F
            titleText.text = "���� No." + betaMission.missionID + " (����\)"; // "(����\)"�ƒǋL
            descriptionText.text = betaMission.description; // ��������ݒ�
            missionButton.interactable = true; // ������悤��
        }
        else if(status == BetaMissionStatus.Now)
        {
            buttonImage.sprite = AvailablePic; // ���b�N�ȊO�Ȃ炽���̔����摜
            buttonImage.color = Color.yellow; // ���풆�Ȃ物�F
            titleText.text = "���� No." + betaMission.missionID + " (���풆)"; // "(���풆)"�ƒǋL
            descriptionText.text = betaMission.description; // ��������ݒ�
            missionButton.interactable = true; // ������悤��
        }
        else if(status == BetaMissionStatus.Completed)
        {
            buttonImage.sprite = AvailablePic; // ���b�N�ȊO�Ȃ炽���̔����摜
            buttonImage.color = Color.green; // �����Ȃ�ΐF
            titleText.text = "���� No." + betaMission.missionID + " (����)"; // "(����)"�ƒǋL
            descriptionText.text = betaMission.description; // ��������ݒ�
            missionButton.interactable = false; // �����Ȃ��悤�ɂ��Ƃ�
        }
        else
        {
            return;
        }
    }
}
