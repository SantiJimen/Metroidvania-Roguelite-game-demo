using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;
    public float deathAnimTime;

    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KBFromRight;

    [SerializeField] private Rigidbody2D rb;
    public Animator anim;
    
    void Start()
    {
        if(!PlayerPrefs.HasKey("health"))
        {
            health = maxHealth;
            PlayerPrefs.SetInt("health", health);
        }
    }

    void FixedUpdate(){
        if(KBCounter > 0) KnockBack();
    }

    public void Dead()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        GetComponent<BoxCollider2D>().enabled = false;
        if(gameObject.name == "Necromancer")
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().dashActive = true;
        }
        
        if(transform.CompareTag("Player")) 
        {
            GameObject.Find("DataSave").GetComponent<PlayerSave>().SavePlayer();
            PlayerPrefs.DeleteKey("health");
            StartCoroutine(Wait(deathAnimTime));
            GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadScene(0);
        }
        else Destroy(this.gameObject, deathAnimTime);
    }

    public int Health()
    {
        return health;
    }

    public void takeDamage(int damage, Transform fromWho)
    {
        if(!GameObject.Find("ExitDoor").GetComponent<Finish>().finished) 
        {
            health -= damage;
            if(health > 0)
            {
                anim.SetTrigger("Hurt");
                KnockBackCounter(fromWho);
            }
            else {
                anim.SetTrigger("Death");
                Dead();
            }
        }  
            
    }

    private void KnockBackCounter(Transform t)
    {
        KBCounter = KBTotalTime;
        if(t.position.x <= transform.position.x) KBFromRight = true;
        else KBFromRight = false;
    }

    private void KnockBack()
    {
        if(KBFromRight)
        {
            rb.velocity = new Vector2(KBForce, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-KBForce, rb.velocity.y);
        }

        KBCounter -= Time.deltaTime;
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
    
    public float getKBCounter()
    {
        return KBCounter;
    }

}
