using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    [SerializeField]private AudioSource BGM;
    private float volume;
    [SerializeField]private Slider bgmSlider;
    private const string PLAYER_PREF_BGM_VOLUME = "BGM_Volume";

    private void Start() {
        volume = PlayerPrefs.GetFloat(PLAYER_PREF_BGM_VOLUME, 0.3f);
        BGM.volume = volume;
    }

    public void UpdateBGM_Volume(){
        volume = bgmSlider.value;
        BGM.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREF_BGM_VOLUME, volume);
    }
}
