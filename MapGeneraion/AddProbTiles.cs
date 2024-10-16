using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddProbTiles : MonoBehaviour
{
    public GameObject tile;
    public int rate = 5;

    public GameObject[] tiles;
    
    void Start()
    {
        tiles = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            tiles[i] = transform.GetChild(i).gameObject;
        }

        System.Random rnd = new System.Random();

        foreach(GameObject obj in tiles)
        {
            int chance = rnd.Next(0,10);
            if(chance < rate)
            {
                Instantiate(tile, obj.transform.position, Quaternion.identity);
            }
        }
    }
}
