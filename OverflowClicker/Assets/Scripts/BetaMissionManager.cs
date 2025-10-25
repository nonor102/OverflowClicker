using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BetaMissionManager : MonoBehaviour
{
    public static BetaMissionManager Instance { get; private set; }
    public int LatestCompletedMissionID { get; private set; } = 0; // �Ō�Ɋ��������~�b�V������ID (���� + 1��BF�l���ʂɏ�Z������)

    public List<BetaMission> allMissions; // �Q�[������Mission�A�Z�b�g��o�^
    public Dictionary<int, BetaMissionStatus> BetaMissionsStatuses { get; private set; } = new Dictionary<int, BetaMissionStatus>(); // �eMisiion�̏�Ԃ�ۑ����鎫��

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);

            InitializeMissions();
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitializeMissions()
    {
        foreach (var mission in allMissions)
        {
            if(mission.preRequiredMissions != null)
            {
                BetaMissionsStatuses[mission.missionID] = BetaMissionStatus.Available; // �O��������Ȃ��~�b�V�����͍ŏ����璧��\
                Debug.Log("Available: " + mission.missionID);
            }
            else
            {
                BetaMissionsStatuses[mission.missionID] = BetaMissionStatus.Locked; // �������b�N
                Debug.Log("Locked: " + mission.missionID);
            }
        }
    }

    public void SetMissionsFromSaveData(SaveData saveData) // �Z�[�u�f�[�^��������Ă���֐�
    {
        LatestCompletedMissionID = saveData.LatestCompletedMissionID;
        CompleteMissions(LatestCompletedMissionID);
    }

    private void CompleteMissions(int missionID) // �B������
    {
        if(missionID > 0)
        {
            LatestCompletedMissionID = missionID;
            Debug.Log("LatestCompletedMissionID: " + LatestCompletedMissionID);
            for (int i = 0; i < missionID; i++)
            {
                BetaMissionsStatuses[missionID] = BetaMissionStatus.Completed; // �n���ꂽID�܂�Completed�ɂ��Ƃ� (�f�[�^�̃Z�[�u�ƌ݊�������������������)
            }
        }
        BetaMissionsStatuses[missionID + 1] = BetaMissionStatus.Available; // ���̂��Available��
    }

    public BetaMissionStatus GetBetaMissionStatus(int missionID) // missionID�̏�Ԃ�Ԃ��֐�
    {
        if(missionID > 0)
        {
            return BetaMissionsStatuses[missionID];
        }
        else
        {
            return BetaMissionStatus.Locked;
        }
    }

    //TODO �e�~�b�V�����̓��e�ƕ�V�Ƃ���������֐�������
}
