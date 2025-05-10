using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSucess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance {get; private set;}
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private int waitingRecipesMax = 4;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        if(spawnRecipeTimer >= spawnRecipeTimerMax && waitingRecipeSOList.Count < waitingRecipesMax) 
        {
            spawnRecipeTimer = 0;
            int index = (int) UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count);
            RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[index];
            waitingRecipeSOList.Add(waitingRecipeSO);
            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        }
        else 
        {
            spawnRecipeTimer += Time.deltaTime;
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) 
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            bool plateContentMatchesRecipe = true;

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) 
            {
                // Has the same number of ingredients
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    // Cycling through all ingredients in the Recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) 
                    {
                        // Cycling through all ingredients in the Plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) 
                        {
                            // Ingredient matches!
                            ingredientFound = true;
                            break;
                        }
                    }
                    if(!ingredientFound) {
                        plateContentMatchesRecipe = false;
                    }
                }
                if(plateContentMatchesRecipe) {
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSucess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        Debug.Log("Player did not deliver correct recipe");
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
}
