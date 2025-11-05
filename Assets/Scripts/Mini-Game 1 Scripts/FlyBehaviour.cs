using System.Security.Cryptography;
using UnityEngine;

public class FlyBehaviour : MonoBehaviour
{
    [SerializeField] public float velocity = 1.5f;
    [SerializeField] public float rotationSpeed = 5f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = Vector2.up * velocity;

            rb.AddForce(Vector2.up * velocity, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, rb.linearVelocity.y * rotationSpeed);
    }
}
