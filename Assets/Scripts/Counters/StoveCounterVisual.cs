using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStoveChanged;
    }
    private void StoveCounter_OnStoveChanged(object sender, StoveCounter.OnStateChangedEventArgs e) 
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}
