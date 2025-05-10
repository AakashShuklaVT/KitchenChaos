using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // Scriptable Object (SO) defining kitchen item properties

    public override void Interact(Player player)
    {
        if(!HasKitchenObject()) 
        {
            if(player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
               
            }
        }
        else
        {
            // counter has kitchen object and player also have kitchen object
            if(player.HasKitchenObject()) 
            {
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) 
                {
                    // player is holding plate
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else 
                {
                    // player is not holding and plate but something else
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) 
                    {
                        // counter is holding a plate
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else 
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}