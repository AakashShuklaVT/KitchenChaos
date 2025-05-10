using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] BaseCounter baseCounter; // Counter to monitor
    [SerializeField] GameObject[] visualGameobjectArray; // Highlight visual 

    private void Start()
    {
        // Subscribe to player's counter selection event
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    // Toggle visual when selection changes
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            ShowVisual();
        }
        else
        {
            HideVisual();
        }
    }
    private void ShowVisual() {
        foreach(GameObject visualGameobject in visualGameobjectArray) 
        {
            visualGameobject.SetActive(true);
        }
    }

    private void HideVisual() 
    {
        foreach(GameObject visualGameobject in visualGameobjectArray) 
        {
            visualGameobject.SetActive(false);
        }
    }    
}