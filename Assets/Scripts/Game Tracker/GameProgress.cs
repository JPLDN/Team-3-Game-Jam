using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public static GameProgress Instance;

    public bool miniGame1Complete;
    public bool miniGame2Complete;
    public bool miniGame3Complete;

    private void Awake()
    {
       if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
       else
        {
            Destroy(gameObject);
        }
    }

    public bool CanPlayMiniGame(int id)
    {
        if (id == 1) return true;
        if (id == 2) return miniGame1Complete;
        if(id == 3) return miniGame2Complete;
        return false;
    }

    public void CompleteMiniGame(int id)
    {
        if (id == 1) miniGame1Complete = true;
        if (id == 2) miniGame2Complete = true;
        if (id == 3) miniGame3Complete = true;
    }
    
}
