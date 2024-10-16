using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    private GameObject player;
    private GameObject spawnPos;

    [SerializeField] private AudioSource hitSound;
    [SerializeField] private GameObject explosionEffect;
    
    public float time = 7f;
    public int damage;
    public float explosionRadius = 0.5f;
    public float maxDistance;

    private Vector3 origin;
    private Vector3 target;
    private Vector3 distance;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPos = GameObject.FindGameObjectWithTag("BombSpawnPos");

        rb = GetComponent<Rigidbody2D>();

        origin = spawnPos.transform.position;
        target = player.transform.position;

        distance = target - origin;

        if(Mathf.Abs(distance.x) > maxDistance)
        {
            if(distance.x < 0) distance.x = -maxDistance;
            else distance.x = maxDistance;
        }
        
        Vector3 vo = CalculateVeliocity();
        rb.velocity = vo;
    }


    Vector3 CalculateVeliocity()
    {
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;
 
        float sY = distance.y;
        float sXz = distanceXz.magnitude;
 
        float Vxz = sXz / time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);
 
        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;
 
        return result;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Enemy"))
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            foreach(Collider2D collision in objects)
            {
                hitSound.Play();
                if(collision.CompareTag("Player"))
                {
                    player.GetComponent<HealthController>().takeDamage(damage, transform);
                }
            }
            
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject,0.5f);
        }
    }
}
