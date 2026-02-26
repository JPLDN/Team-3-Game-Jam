using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public float startingSpeed;

    [SerializeField] private PongGameManager gameManager;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LaunchBall();
    }

    public void LaunchBall()
    {
        float x = Random.value >= 0.5f ? 1f : -1f;

        float y = Random.Range(-1f, 1f);
        if (Mathf.Abs(y) < 0.3f)
            y = Mathf.Sign(y == 0 ? 1 : y) * 0.3f;

        Vector2 direction = new Vector2(x, y).normalized;
        rb.linearVelocity = direction * startingSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftGoal"))
        {
            gameManager.AIScored();
        }
        else if (collision.CompareTag("RightGoal"))
        {
            gameManager.PlayerScored();
        }
    }
}
