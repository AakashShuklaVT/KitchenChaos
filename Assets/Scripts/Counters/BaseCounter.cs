using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaced;
    [SerializeField] private Transform counterTopPoint; // Position where objects are placed
    private KitchenObject kitchenObject; // Currently held kitchen object
    public virtual void Interact(Player player){}
    public virtual void InteractAlternate(Player player){}
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
