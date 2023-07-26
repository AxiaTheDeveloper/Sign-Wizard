using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestScriptableObject : ScriptableObject
{
    public int levelQuest;
    public string nameSender;
    public ItemScriptableObject[] potionWantList;
    public int totalPotion => potionWantList.Length;

    public string Quest_Title;

    [field : TextArea]
    public string QuestDescription;
    [field : TextArea]
    public string QuestTask;
    [field : TextArea]
    public string QuestinMail;

    public float progressPerTumbuk_Quest;

    

}
