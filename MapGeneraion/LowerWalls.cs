using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerWalls : MonoBehaviour
{

    private Vector3 target;
    private Vector3 origin;

    void Start()
    {
        origin = transform.position;
        target = new Vector3 (transform.position.x, transform.position.y - 3, transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Started()) lowerWalls();
        if(Finished()) riseWalls();
    }

    private void lowerWalls()
    {
        transform.position = target;
    }

    private void riseWalls()
    {
        transform.position = origin;
    }

    public bool Started()
    {
        return GameObject.Find("Player").transform.position.x > -10;
    }

    public bool Finished()
    {
        return GameObject.Find("Necromancer") == null;
    }

    public bool areLowered()
    {
        return transform.position == target;
    }
}
