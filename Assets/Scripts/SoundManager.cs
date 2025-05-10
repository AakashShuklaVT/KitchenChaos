using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{ 
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipsRefsSO audioClipRefsSO;

    private void Awake()
    {
        Instance = this;
    }
    private void Start() 
    {
        DeliveryManager.Instance.OnRecipeSucess += DeliveryManager_OnRecipeSucess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyRecipeCut += CuttingCounter_OnAnyRecipeCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }
    private void BaseCounter_OnAnyObjectPlaced(object sender, EventArgs e) 
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectPickup, baseCounter.transform.position);
    }
    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(audioClipRefsSO.objectPickup, player.transform.position);
    }
    private void CuttingCounter_OnAnyRecipeCut(object sender, EventArgs e) 
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }
    private void DeliveryManager_OnRecipeSucess(object sender, EventArgs e) 
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);

    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e) 
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFailed, deliveryCounter.transform.position);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) 
    {
        AudioSource.PlayClipAtPoint(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) 
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }
}
