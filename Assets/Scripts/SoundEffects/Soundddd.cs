using UnityEngine;

public class Sounddd : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        audioSource.Play();
    }
}

