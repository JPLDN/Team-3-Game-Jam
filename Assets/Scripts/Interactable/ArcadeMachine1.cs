using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMachine1 : MonoBehaviour
{
    [SerializeField] private int miniGameID = 1;
    [SerializeField] private string MiniGame1;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (GameProgress.Instance.CanPlayMiniGame(miniGameID))
        {
            SceneManager.LoadScene(MiniGame1);
        }
        else
        {
            Debug.Log("This machine is locked until you complete the previous mini-game");
        }
    }
}
