using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    
    [SerializeField] private Transform attackController;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource attackAudio;
    [SerializeField] private AudioSource missAudio;

    public float attackRadius;
    public int attackDamage;
    public float cooldown;
    private float timer = 0;
   
    void Update()
    {

        if(Input.GetButtonDown("Fire1") && timer >= cooldown) 
        {
            timer = 0;
            Attack();
        }

        timer += Time.deltaTime;

    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        Collider2D[] objects = Physics2D.OverlapCircleAll(attackController.position, attackRadius);
        bool enemy = false;

        foreach(Collider2D collision in objects)
        {
            if(collision.CompareTag("Enemy"))
            {
                enemy = true;
                collision.transform.GetComponent<HealthController>().takeDamage(attackDamage, transform);
            }
        }

        if(enemy) attackAudio.Play();
        else missAudio.Play();
    }
}
