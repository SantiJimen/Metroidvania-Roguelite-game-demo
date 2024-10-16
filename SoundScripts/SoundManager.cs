using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] Slider musicVolume;
    
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = musicVolume.value;
        SaveVolume();
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolume.value);
    }

    private void LoadVolume()
    {
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume");
    }
}
