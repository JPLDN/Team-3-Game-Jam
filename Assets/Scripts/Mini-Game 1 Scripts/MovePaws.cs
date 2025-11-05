using System.Security.Cryptography;
using UnityEngine;

public class MovePaws : MonoBehaviour
{
    [SerializeField] public float speed = 1f;

    // Update is called once per frame
    void Update()
    {   
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
