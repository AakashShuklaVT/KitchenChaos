using UnityEngine;
[CreateAssetMenu(fileName = "BurningRecipe", menuName = "BurningRecipeSO")]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float burningTimerMax;
}
