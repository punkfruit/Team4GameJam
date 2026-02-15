using UnityEngine;

public class BoilingWaterSpawnScript : MonoBehaviour
{
    
    public GameObject boilingWater;
    public float spawnRate = 5;
    private float timer = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Instantiate(boilingWater, transform.position, transform.rotation);
            timer = 0;
        }
        
    }
}
