using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    langsung, craft
}
[CreateAssetMenu]

public class ItemScriptableObject : ScriptableObject
{
    public int itemID => GetInstanceID();
    public string itemName;
    public ItemType type;
    public Sprite itemSprite;
    public bool isStackable;
    public int maxStack = 1;
    [field : TextArea]
    public string Desc;
    
}
