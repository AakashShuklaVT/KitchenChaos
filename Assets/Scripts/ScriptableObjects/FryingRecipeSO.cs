using UnityEngine;
[CreateAssetMenu(fileName = "FryingRecipe", menuName = "FryingRecipeSO")]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float timerProgressMax;
}
