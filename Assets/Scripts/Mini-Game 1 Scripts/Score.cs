using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    public static Score instance;

    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] private int WinScore = 15;

    private int score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = score.ToString();
    }
    
    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
        if (score >= WinScore)
        {
            var instance = GameStateManager.Instance;
            instance.SetFlag(GameFlags.IsMinigame1Complete, true);

            UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        }
    }
}
