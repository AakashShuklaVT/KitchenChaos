using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeListSO", menuName = "RecipeListSO")]
public class RecipeListSO : ScriptableObject
{
    public List<RecipeSO> recipeSOList;
}
