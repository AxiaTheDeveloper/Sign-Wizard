using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PotionRecipeScriptableObject : ScriptableObject
{
    public ItemScriptableObject[] Ingredients;
    public float fireSize;
    public ItemScriptableObject output_Potion;
}
