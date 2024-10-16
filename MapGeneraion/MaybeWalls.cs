using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MaybeWalls : MonoBehaviour
{

    public GameObject[] prefabs;

    private GameObject[] tiles;
    
    void Start()
    {
        Transform[] directChildren = (from directChild in transform.
                                    GetComponentsInChildren<Transform>() 
                                    where directChild.transform.parent == transform
                                    select directChild).ToArray();

        System.Random rnd = new System.Random();

        int x = rnd.Next(0,2);

        tiles = new GameObject[directChildren[x].childCount];

        for (int i = 0; i < directChildren[x].childCount; i++)
        {
            tiles[i] = directChildren[x].GetChild(i).gameObject;
        }
        foreach(GameObject obj in tiles)
        {
            Instantiate(prefabs[x], obj.transform.position, Quaternion.identity);
        }
    }
}
