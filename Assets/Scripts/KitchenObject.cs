using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO; // Data container for this object
    private IKitchenObjectParent kitchenObjectParent; // Current counter holding this object

    // Getter for the KitchenObjectSO data
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    // Returns the counter this object is placed on
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    // Move object to a new counter and update its parent/position
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject(); 
        }
        this.kitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.SetKitchenObject(this); 
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform(); // Reparent
        transform.localPosition = Vector3.zero; // Reset position
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    // doubt in here why its on kitchen object
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) 
    {
        if(this is PlateKitchenObject) 
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else plateKitchenObject = null;
        return false;
    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}