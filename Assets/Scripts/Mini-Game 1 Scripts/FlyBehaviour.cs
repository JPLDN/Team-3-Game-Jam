using System.Security.Cryptography;
using UnityEngine;

public class FlyBehaviour : MonoBehaviour
{
    [SerializeField] public float velocity = 1.5f;
    [SerializeField] public float rotationSpeed = 5f;

    [SerializeField] private GameObject restartButton;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    public AudioSource bgSound;
    public AudioClip failSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(restartButton != null)
            restartButton.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = Vector2.up * velocity;

            rb.AddForce(Vector2.up * velocity, ForceMode2D.Impulse);

            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, rb.linearVelocity.y * rotationSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Time.timeScale = 0;
        if (restartButton != null)
            restartButton.SetActive(true);
        audioSource.clip = failSound; 
        audioSource.Play();
        bgSound.Stop();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
