using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMachine2 : MonoBehaviour, IInteractable
{
    [SerializeField] private int miniGameID = 2;
    [SerializeField] private string MiniGame2;

    public bool CanInteract()
    {
        return GameProgress.Instance.CanPlayMiniGame(miniGameID);
    }

    public void Interact()
    {
        if (GameProgress.Instance.CanPlayMiniGame(miniGameID))
        {
            SceneManager.LoadScene(MiniGame2);
        }
        else
        {
            Debug.Log("This machine is locked until you complete the previous mini-game");
        }
    }
}
