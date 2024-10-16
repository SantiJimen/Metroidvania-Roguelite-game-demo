using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRooms : MonoBehaviour
{

    public GameObject tile;

    public GameObject[] tiles;
    
    void Start()
    {
        tiles = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            tiles[i] = transform.GetChild(i).gameObject;
        }

        foreach(GameObject obj in tiles)
        {
            Instantiate(tile, obj.transform.position, Quaternion.identity);
        }
    }


}
