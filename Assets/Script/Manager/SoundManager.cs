using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]private AudioSource[] SoundEffect_Array;
    private float volume;
    [SerializeField]private Slider soundSlider;
    private const string PLAYER_PREF_SOUND_VOLUME = "Sound_Volume";


    private void Start() {
        volume = PlayerPrefs.GetFloat(PLAYER_PREF_SOUND_VOLUME, 0.3f);
        foreach(AudioSource soundEffect in SoundEffect_Array){
            soundEffect.volume = volume;
        }
        
    }

    public void UpdateSound_Volume(){
        volume = soundSlider.value;
        PlayerPrefs.SetFloat(PLAYER_PREF_SOUND_VOLUME, volume);
        foreach(AudioSource soundEffect in SoundEffect_Array){
            soundEffect.volume = volume;
        }
    }
    

}
