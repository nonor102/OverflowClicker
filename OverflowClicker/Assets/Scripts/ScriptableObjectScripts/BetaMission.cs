using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionStatus
{
    Locked, // ƒƒbƒN’†
    Available, // ’§í‰Â”\
    Now, // ’§í’†
    Completed // Š®—¹
}

[CreateAssetMenu(fileName = "BetaMission", menuName = "ScriptableObjects/Create Beta Mission")]
public class BetaMission : ScriptableObject
{
    [Header("—û‚Ìî•ñ")]
    public int missionID; // —û‚ÌID
    [TextArea]
    public string discription; // à–¾•¶
}
