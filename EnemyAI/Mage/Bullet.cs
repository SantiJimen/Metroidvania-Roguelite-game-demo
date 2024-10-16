using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private GameObject player;
    
    public float speed = 20f;
    public int damage;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource hitSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            hitSound.Play();
            player.GetComponent<HealthController>().takeDamage(damage, transform);
            Destroy(gameObject);
        }
    }
}
