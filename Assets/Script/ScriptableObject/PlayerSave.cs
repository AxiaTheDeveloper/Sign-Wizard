using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum levelMode{
    outside, MakingPotion, finishQuest
}
[CreateAssetMenu]
public class PlayerSave : ScriptableObject
{
    public int level;
    public levelMode modeLevel;
    public WitchGameManager.Place placePlayerNow;
    public WitchGameManager.OutDoorType outDoorTypeNow;
    public bool isResetDay;
    public bool isFromOutside;
    public bool isSubmitPotion;
    public bool isResetSave;

    public bool isFirstTime_Tutorial;

    public bool isFirstTimeInGame = true;

}
