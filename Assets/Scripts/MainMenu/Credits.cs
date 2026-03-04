using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    
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

    public void ShowsPanel()
    {
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void HidesPanel()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
