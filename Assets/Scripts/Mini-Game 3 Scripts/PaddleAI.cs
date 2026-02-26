using System.Threading;
using UnityEngine;

public class PaddleAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform ball;
    [SerializeField] private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float deadZone = 0.05f;
    [SerializeField] private float smooth = 15f;

    [Header("Difficulty")]
    [SerializeField] private float targetReactionTime = 0.12f;
    [SerializeField] private float maxTargetOffset = 0.3f;

    private float targetY;
    private float timer;

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (ball == null) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = targetReactionTime;

            float offset = Random.Range(-maxTargetOffset, maxTargetOffset);
            targetY = ball.position.y + offset;
        }
    }

    private void FixedUpdate()
    {
        if(ball == null || rb == null) return;

        float diff = targetY - transform.position.y;

        if (Mathf.Abs(diff) < deadZone)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float desiredVy = Mathf.Sign(diff) * moveSpeed;

        float smoothedVy = Mathf.Lerp(rb.linearVelocity.y, desiredVy, smooth * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector2(0f, smoothedVy);
    }
}
