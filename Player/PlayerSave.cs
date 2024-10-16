using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    
    public void SavePlayer()
    {
        GameObject player = GameObject.Find("Player");
        PlayerMovement move = player.GetComponent<PlayerMovement>();
        PlayerPrefs.SetInt("dash", move.dashActive?1:0);
        PlayerPrefs.SetInt("claws", move.climbClaws?1:0);
        PlayerPrefs.SetInt("boots", move.bootsOn?1:0);
        PlayerPrefs.SetInt("health", player.GetComponent<HealthController>().Health());
    }

    public void LoadPlayer()
    {
        GameObject player = GameObject.Find("Player");
        if(PlayerPrefs.HasKey("dash"))
        {
            player.GetComponent<PlayerMovement>().dashActive = PlayerPrefs.GetInt("dash")==1?true:false;
            player.GetComponent<PlayerMovement>().climbClaws = PlayerPrefs.GetInt("claws")==1?true:false;
            player.GetComponent<PlayerMovement>().bootsOn = PlayerPrefs.GetInt("boots")==1?true:false;
            player.GetComponent<HealthController>().health = PlayerPrefs.GetInt("health");
        }
        
    }
}
