using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu]
public class PotionRecipeScriptableObject : ScriptableObject
{
    // public ItemScriptableObject[] Ingredients;
    [Serializable]
    public class Ingredients{
        public ItemScriptableObject ingredientName;
        public int quantity;
    }
    public Ingredients[] ingredientArray;
    public int fireSizeLevel;
    public ItemScriptableObject output_Potion;
}
