using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PongGameManager : MonoBehaviour
{
    [Header("Score")]
    public TMP_Text playerScoreText;
    public TMP_Text aiScore;

    private int playerScore = 0;
    private int aiScoreScore = 0;

    [Header("Ball")]
    [SerializeField] private Ball ball;
    [SerializeField] private int winScore = 3;

    public void PlayerScored()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();

        if (playerScore >= winScore)
        {
            Debug.Log("Player Wins!");
            Time.timeScale = 0f;

            GameStateManager.Instance.SetFlag(GameFlags.IsMinigame3Complete, true);
            UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene");
            return;
        }

        ResetBall();
    }

    public void AIScored()
    {
        aiScoreScore++;
        aiScore.text = aiScoreScore.ToString();

        if (aiScoreScore >= winScore)
        {
            Debug.Log("You Lose!");
            Time.timeScale = 0f;
            return;
        }

        ResetBall();
    }

    private void ResetBall()
    {
        ball.transform.position = Vector3.zero;
        ball.rb.linearVelocity = Vector2.zero;
        ball.Invoke("LaunchBall", 0.5f);
    }
}
