using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public int damage;
    private float timer = 5;
    public float coolDown;

    private Transform player;
    [SerializeField] private AudioSource attackSound;
    
    private void OnCollisionStay2D(Collision2D other)
    {
        timer += Time.deltaTime;
        if(other.gameObject.tag == "Player" && timer >= coolDown)
        {
            player = GameObject.Find("Player").transform;
            attackSound.Play();
            timer = 0;
            player.GetComponent<HealthController>().takeDamage(damage, transform);
        }
        
    }
}
