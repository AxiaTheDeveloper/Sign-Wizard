using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TumbukRecipeScriptableObject : ScriptableObject
{
    public ItemScriptableObject Ingredient;
    public ItemScriptableObject output_Ingredient;
    [Tooltip("The progress value per tumbuk. Must be between 1 and 100.")]
    public float progressPerTumbuk;
}
