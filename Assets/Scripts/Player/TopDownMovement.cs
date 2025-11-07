using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement;

    private Animator anim;
    private bool playingFootsteps = false;
    public float footstepSpeed = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = movement * moveSpeed;
        //StopFootSteps();

        if (!playingFootsteps)
        {
            //StartFootSteps();
        }
        else if (playingFootsteps && movement == Vector2.zero)
        {
            //StopFootSteps();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        anim.SetBool("isWalking", true);
        StartFootSteps();

        if (context.canceled)
        {
            anim.SetBool("isWalking", false);
            anim.SetFloat("LastInputX", movement.x);
            anim.SetFloat("LastInputY", movement.y);
            StopFootSteps();
        }

        movement = context.ReadValue<Vector2>();
        anim.SetFloat("InputX", movement.x);
        anim.SetFloat("InputY", movement.y);
    }

    void StartFootSteps()
    {
        playingFootsteps = true;
        InvokeRepeating(nameof(PlayFootstep), 0f, footstepSpeed);
    }

    void StopFootSteps()
    {
        playingFootsteps = false;
        CancelInvoke(nameof(PlayFootstep));
    }

    void PlayFootstep()
    {
        SoundEffectManager.Play("Footstep");
    }
}
