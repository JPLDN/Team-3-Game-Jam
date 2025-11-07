using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject mainMenuPanel;
    
    public void ShowPanel()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void HidePanel()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
