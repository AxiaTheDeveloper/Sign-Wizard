using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PotionRecipeScriptableObject : ScriptableObject
{
    public ItemScriptableObject[] Ingredients;
    public int fireSizeLevel;
    public ItemScriptableObject output_Potion;
}
