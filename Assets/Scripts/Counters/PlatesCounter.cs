using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event Action OnPlateSpawned;
    public event Action OnPlateRemoved;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;
    private void Start()
    {
        
    }

    private void Update()
    {   
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0;
            if(platesSpawnAmount < platesSpawnAmountMax) {
                platesSpawnAmount++;
                OnPlateSpawned?.Invoke();
            }
        }
    }

    public override void Interact(Player player)
    {
        if(!player.GetKitchenObject()) {
            // player is empty handed

            if(platesSpawnAmount > 0) {
                // there is atleast on plate
                platesSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke();
            }
        }
    }
}
