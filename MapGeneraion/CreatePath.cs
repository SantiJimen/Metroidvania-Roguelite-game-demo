using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CreatePath: MonoBehaviour
{
    
    public int length, height;
    private (int x, int y) roomDimensions = (20,10);
    private int[,] level;
    private System.Random rnd;

    [SerializeField] private GameObject block;
    [SerializeField] private GameObject backGroundTile;
    [SerializeField] private GameObject roomsType0;
    [SerializeField] private GameObject roomsType1;
    [SerializeField] private GameObject roomsType2;
    [SerializeField] private GameObject roomsType3;
    [SerializeField] private GameObject roomsType4;
    [SerializeField] private GameObject startRoom;
    [SerializeField] private GameObject exitRoom;
    
    
    /*
    codis d'habitacions:
    4: habitacio final del nivell (sempre esta a la ultima fila)
    3: habitacio amb sortides esquerra, dreta i adalt garantides
    2: habitacio amb sortides esquerra, dreta i abaix garantides (si te una habitacio 2 a sobre, tambe garanteix sortida a dalt)
    1: habitacio amb sortides esquerra i dreta garantides
    0: habitacio que no forma part del cami (no te sortides garantides / deadends)
    */

    void Start()
    {
        level = new int[height, length];
        rnd = new System.Random();
        int start = rnd.Next(0,length);
        GeneratePathToEnd(0, 0, 1);
        
        GenerateMap();
    }

    /*
    codis de moviment:
    1,2: esquerra
    3,4: dreta
    5: abaix
    */

    private void GeneratePathToEnd(int x, int y, int room)
    {
        int movement;
        if(x==0 && y==0) movement = rnd.Next(1,6);
        else movement = rnd.Next(1,7);

        while(!validDestination(x, y, movement)) 
        {
            movement++;
        }
        
        if(finalRoom(x, movement)) 
        {
            level[x,y] = 4; 
        }
        else
        {
            level[x,y] = room;

            switch(movement)
            {
                case 1:
                case 2:
                case 3:
                    GeneratePathToEnd(x, y-1, 1);
                    break;
                case 4:
                case 5:
                    GeneratePathToEnd(x, y+1, 1);
                    break;
                default:
                    level[x,y] = 2;
                    GeneratePathToEnd(x+1, y, 3);
                    break;
            }
            
        }
    }

    private bool finalRoom(int x, int direction)
    {
        return direction > 5 && x == height-1;
    }


    private bool validDestination(int x, int y, int direction)
    {
        switch(direction)
        {
            case 1:
            case 2:
            case 3:
                return y > 0 && level[x, y-1] == 0;
            case 4:
            case 5:
                return y < length-1 && level[x, y+1] == 0;
            default:
                return true;
        }
    }
    
    private void GenerateMap()
    {
        DrawPerimeter();
        GameObject rooms; 
        GameObject[,] prev_rooms = new GameObject[height, length];
        int r;

        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < length; j++)
            {
                
                switch(level[i,j])
                {
                    case 0:
                        if(dropFromAvobe(prev_rooms, i, j)) r = 3;
                        else if(j == length-1) r = 1;
                        else if(j == 0 || (j > 0 && prev_rooms[i,j-1].name == "NoRoom"))  r = 2;
                        else if(prev_rooms[i,j-1].name == "RoomType0_2")
                        {
                            int[] room = {0,7};
                            r = room[rnd.Next(0,2)];
                        }
                        else if(i < height-1 && level[i+1,j] == 0)
                        {
                            int[] room = {0,0,0,0,0,0,0,5,6,4,4,4,4,4};
                            r = room[rnd.Next(0,14)];
                        }
                        else
                        {
                            int[] room = {0,0,0,0,0,5,6};
                            r = room[rnd.Next(0,7)];
                        }
                        rooms = roomsType0;
                        break;
                    
                    case 2:
                        if(dropFromAvobe(prev_rooms, i, j))
                        {
                            r = rnd.Next(0,3);
                            rooms = roomsType4;
                        }
                        else 
                        {
                            r = rnd.Next(0,4);
                            rooms = roomsType2;
                        }
                        
                        break;
                    case 3:
                        r = rnd.Next(0,3);
                        rooms = roomsType3;
                        
                        break;
                    case 4:
                        if(dropFromAvobe(prev_rooms, i, j)) r = 1;
                        else r = 0;
                        rooms = exitRoom;
                        break;
                    default:
                        if(dropFromAvobe(prev_rooms, i, j)) r = 5;
                        else if(i == height-1) r = rnd.Next(0,4);
                        else
                        {
                            int[] choices = {0,1,2,3,4,4,4};
                            r = choices[rnd.Next(0,7)];
                        }
                        rooms = roomsType1;
                        
                        break;
                }
                if(i == 0 && j == 0)
                {
                    r = 0;
                    rooms = startRoom;
                }


                Transform[] directChildren =  (from directChild in rooms.
                                    GetComponentsInChildren<Transform>() 
                                    where directChild.transform.parent == rooms.transform 
                                    select directChild).ToArray();

                prev_rooms[i,j] = directChildren[r].gameObject;
                
                Instantiate(directChildren[r].gameObject, new Vector3(j*roomDimensions.x, -i*roomDimensions.y, 0), Quaternion.identity);
            }
        }
    }

    private bool dropFromAvobe(GameObject[,] prev_rooms, int x, int y)
    {
        return (x > 0 && (prev_rooms[x-1,y].name == "RoomType0_5" ||  
                prev_rooms[x-1,y].name == "RoomType1_5" ||
                prev_rooms[x-1,y].transform.parent.gameObject.name == "RoomType4" || 
                prev_rooms[x-1,y].transform.parent.gameObject.name == "RoomType2"));
    }
    
    private void DrawPerimeter()
    {
        float x = - 5.5f;
        float y = - 10.5f;

        for(float i = x - 8; i < height*roomDimensions.y + x + 10; i++)
        {
            for(float j = y - 15; j < length*roomDimensions.x + y + 17; j++)
            {
                if(i <= x || i >= height*roomDimensions.y +x+ 1 || j <= y || j >= length*roomDimensions.x +y+ 1) 
                {
                    Instantiate(block,new Vector3(j,-i,0), Quaternion.identity);
                }
                else
                {
                    Instantiate(backGroundTile,new Vector3(j,-i,0), Quaternion.identity);
                }
            }
        }
    }

    

    public List<Transform> directChildren(GameObject parentGO) 
    {

        List<Transform> directChildren = new List<Transform>();
        foreach(Transform go in parentGO.transform){  // This will only find direct children
                Transform c = go.gameObject.GetComponent<Transform>();
                directChildren.Add(c);
        }
        return directChildren;
    }
}
