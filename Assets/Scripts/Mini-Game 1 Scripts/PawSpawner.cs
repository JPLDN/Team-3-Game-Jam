using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawSpawner : MonoBehaviour
{

    [SerializeField] public float maxTime = 1.5f;
    [SerializeField] public float heightRange = 1f;
    [SerializeField] public GameObject pawPrefab;

    public float timer; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPaw();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime)
        {
            SpawnPaw();
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    public void SpawnPaw()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-heightRange, heightRange));
        GameObject paw = Instantiate(pawPrefab, spawnPos, Quaternion.identity);

        Destroy(paw, 10f);
    }
}
