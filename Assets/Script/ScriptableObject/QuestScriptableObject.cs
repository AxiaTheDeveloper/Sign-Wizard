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

    public string Quest_Title_ID, Quest_Title_EN;
    public string Quest_Title
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? Quest_Title_ID : Quest_Title_EN;
        }
    }

    [field : TextArea]
    public string QuestDescription_ID, QuestDescription_EN;
    public string QuestDescription
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? QuestDescription_ID : QuestDescription_EN;
        }
    }
    [field : TextArea]
    public string QuestTask_ID, QuestTask_EN;
    public string QuestTask
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? QuestTask_ID : QuestTask_EN;
        }
    }
    [field : TextArea]
    public string QuestTaskPlace_ID, QuestTaskPlace_EN;
    public string QuestTaskPlace
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? QuestTaskPlace_ID : QuestTaskPlace_EN;
        }
    }
    [field : TextArea]
    public string QuestinMail_ID, QuestinMail_EN;
    public string QuestinMail
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? QuestinMail_ID : QuestinMail_EN;
        }
    }

    [Header("Dari Quest Sebelumnya")]
    [field : TextArea]
    public string GiftDescinMail_ID, GiftDescinMail_EN;
    public string GiftDescinMail
    {
        get
        {
            return PlayerPrefs.GetString("pilihanIDEN", "ID") == "ID" ? GiftDescinMail_ID : GiftDescinMail_EN;
        }
    }
    public string giftSender;
    public float progressPerTumbuk_Quest;

    

}
