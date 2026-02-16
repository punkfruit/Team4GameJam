using UnityEngine;

public class BoilingWaterSpawnScript : MonoBehaviour
{
    
    public GameObject boilingWater;
    public float spawnRate = 5f;//Rate between spawns in seconds
    public float delay = 0f;//Delay from start of game until spawning starts
    private float timer = 0f;
    private float tempTimer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tempTimer = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (tempTimer > 0f)
        {
            tempTimer -= Time.deltaTime;
            return;
        }
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Instantiate(boilingWater, transform.position, transform.rotation, this.transform);
            timer = 0;
        }
    }
}
