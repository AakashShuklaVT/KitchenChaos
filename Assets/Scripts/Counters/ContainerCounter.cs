using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // Scriptable Object (SO) defining kitchen item properties

    public event Action OnPlayerGrabbedObject;
    public override void Interact(Player player)
    {
        // Spawn object if counter is empty
        if (!player.HasKitchenObject())
        {
            // player is not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke();
        }
    }
}
