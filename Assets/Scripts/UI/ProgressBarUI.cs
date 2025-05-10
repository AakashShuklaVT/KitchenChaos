using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] GameObject hasProgressGameobject;
    [SerializeField] private Image barImage;
    private IHasProgress hasProgress;
    private void Start()
    {
        hasProgress = hasProgressGameobject.GetComponent<IHasProgress>();

        if(hasProgress == null) 
        {
            Debug.LogError("Gameobject " + hasProgressGameobject + "does not have component that implements IHasProgress!");
        }

        hasProgress.OnProgressChanged += IHasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void IHasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        
        if(e.progressNormalized == 0 || e.progressNormalized == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show() 
    {
        gameObject.SetActive(true);
    }

    private void Hide() 
    {
        gameObject.SetActive(false);
    }
}
