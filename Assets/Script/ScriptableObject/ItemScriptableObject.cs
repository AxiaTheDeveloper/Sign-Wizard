using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//item
public enum ItemType{
    bahanPotion, bahanHarusDigerus, potion
}
[CreateAssetMenu]

public class ItemScriptableObject : ScriptableObject
{
    public int itemID => GetInstanceID();
    public string itemName;
    public ItemType type;
    public Sprite itemSprite;
    public bool isFromChest;
    public int maxStack = 1;
    [field : TextArea]
    public string Desc;
    
}
