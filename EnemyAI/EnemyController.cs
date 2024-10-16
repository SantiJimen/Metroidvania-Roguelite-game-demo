using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public float speedChase = 5f;
    public float turnDelayTime = 1;
    public float turnDelayChase = 0.4f;
    public float h = 1;
    public float agroRange = 6;
    public float disengageRange = 11;
    public float KBCounter;
    public float scale;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Animator anim;
    private Rigidbody2D rb;
    private GameObject walls;

    void FixedUpdate()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        walls = GameObject.Find("WallsBoss");

        if(walls != null) 
        {
            if(walls.transform.GetComponent<LowerWalls>().areLowered() && Health() > 0)
                rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        }

        if(h > 0) FlipRight();
        else FlipLeft();

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if(Health() < 0) rb.velocity = new Vector2(0,rb.velocity.y);

        KBCounter = transform.GetComponent<HealthController>().KBCounter;
    }

    public void changeH()
    {
        h *= -1;
    }

    public void FlipLeft()
    {
        Vector3 localScale = transform.localScale;
        localScale.x = -scale;
        transform.localScale = localScale;
    }

    public void FlipRight()
    {
        Vector3 localScale = transform.localScale;
        localScale.x = scale;
        transform.localScale = localScale;
    }

    public float getKBCounter()
    {
        return KBCounter;
    }

    public bool checkForGound(Transform _transform)
    {
        return Physics2D.OverlapCircle(_transform.position, 0.2f, groundLayer) || Physics2D.OverlapCircle(_transform.position, 0.2f, obstacleLayer);
    }

    public float Health()
    {
        return transform.GetComponent<HealthController>().Health();
    }

    public void StartAnimation(string action)
    {
        anim.SetTrigger(action);
    }

    public void SetBoolAnim(string action, bool value)
    {
        anim.SetBool(action, value);
    }

}
