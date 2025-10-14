using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class BetaUpgradeTreeUI : MonoBehaviour
{
    [SerializeField] private GameObject betaUpgradeNodePrefab; // �����\���pUI�̃v���n�u
    [SerializeField] private Transform container;          // UI�𐶐�����e�I�u�W�F�N�g

    private Dictionary<string, Button> missionNodeObjects = new Dictionary<string, Button>();

    void Start()
    {
        GenerateMissionTree();
    }

    void Update()
    {
        UpdateMissionNodes();
    }

    void GenerateMissionTree() // �����c���[��UI��ɐ�������
    {
        foreach (var mission in BetaUpgradeManager.Instance.allMissions) // �S�Ẵ~�b�V�����ɑ΂���UI�m�[�h�𐶐�
        {
            GameObject nodeObj = Instantiate(betaUpgradeNodePrefab, container);
            nodeObj.GetComponentInChildren<Text>().text = mission.title;
            // TODO: �����Ńm�[�h�̍��W��ݒ肷��B�蓮���A�������C�A�E�g�A���S���Y�����g���B
            // nodeObj.transform.localPosition = new Vector3( ... );

            Button nodeButton = nodeObj.GetComponent<Button>();
            string missionID = mission.missionID; // �N���[�W���̂��߂Ƀ��[�J���ϐ��ɃR�s�[
            nodeButton.onClick.AddListener(() => OnMissionNodeClicked(missionID));

            missionNodeObjects[mission.missionID] = nodeButton;
        }

        // TODO: �����Ń~�b�V�����m�[�h�Ԃ���Ō��ԏ���������
    }

    void UpdateMissionNodes() // ����I��UI�̏�Ԃ��X�V����
    {
        foreach (var mission in BetaUpgradeManager.Instance.allMissions)
        {
            Button button = missionNodeObjects[mission.missionID];
            BetaUpgradeStatus status = BetaUpgradeManager.Instance.GetBetaUpgradeStatus(mission.missionID);

            switch (status) // ��Ԃɉ����ă{�^���̐F��C���^���N�V������ύX
            {
                case BetaUpgradeStatus.Locked:
                    button.interactable = false;
                    button.GetComponent<Image>().color = Color.gray;
                    break;
                case BetaUpgradeStatus.Available:
                    button.interactable = true;
                    button.GetComponent<Image>().color = Color.white;
                    break;
                case BetaUpgradeStatus.Completed:
                    button.interactable = false;
                    button.GetComponent<Image>().color = Color.green;
                    break;
            }
        }
    }

    void OnMissionNodeClicked(string missionID) // �����m�[�h���N���b�N���ꂽ�Ƃ��̏���
    {
        Debug.Log($"�N���b�N���ꂽ�~�b�V����: {missionID}");
        // �����Ń~�b�V�����ڍ׉�ʂ��J������A�~�b�V�������J�n���鏈�����Ăяo��
        // ��FBetaUpgradeManager.Instance.StartMission(missionID);
    }
}