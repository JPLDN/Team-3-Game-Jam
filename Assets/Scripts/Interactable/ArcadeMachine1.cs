using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ArcadeMachine : MonoBehaviour, IInteractable
{
    [Header("Mini-Game Settings")]
    [SerializeField] private Minigames miniGameID = Minigames.Minigame1; 
    [SerializeField] private string miniGameScene;     

    [Header("Locked Message UI")]
    [SerializeField] private GameObject lockedPopupPanel; 
    [SerializeField] private TMP_Text lockedPopupText;    

    public bool CanInteract() => true;

    public void Interact()
    {
        if (GameStateManager.Instance.CanPlayMinigame(miniGameID))
        {
            SceneManager.LoadScene(miniGameScene);
        }
        else
        {
            ShowLockedMessage();
        }
    }

    private void ShowLockedMessage()
    {
        if (lockedPopupPanel != null && lockedPopupText != null)
        {
            lockedPopupPanel.SetActive(true);

            if (miniGameID == Minigames.Minigame2)
                lockedPopupText.text = "You need to complete Mini-Game 1 first!";
            else if (miniGameID == Minigames.Minigame3)
                lockedPopupText.text = "You need to complete Mini-Game 2 first!";
            else
                lockedPopupText.text = "This machine is locked!";

            Invoke(nameof(HideLockedMessage), 2.5f);
        }
        else
        {
            Debug.LogWarning("Locked popup UI not set on " + gameObject.name);
        }
    }

    private void HideLockedMessage()
    {
        if (lockedPopupPanel != null)
            lockedPopupPanel.SetActive(false);
    }
}

