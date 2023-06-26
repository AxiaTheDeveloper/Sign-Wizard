using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum levelMode{
    startQuest, MakingPotion, EndQuestFail, EndQuestSuccess
}
[CreateAssetMenu]
public class PlayerSave : ScriptableObject
{
    public int level;
    public levelMode modeLevel;

}
