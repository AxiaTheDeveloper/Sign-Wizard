using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestScriptableObject : ScriptableObject
{
    public int levelQuest;
    public ItemScriptableObject[] potionWantList;
    public int totalPotion => potionWantList.Length;
    [field : TextArea]
    public string QuestDescription;
    [field : TextArea]
    public string QuestLog;

    

}
