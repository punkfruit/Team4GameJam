using System;
using UnityEngine;

public class BoilingWater : MonoBehaviour
{

    public float fallSpeed = 10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platforms") || collision.collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Platforms") || other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
