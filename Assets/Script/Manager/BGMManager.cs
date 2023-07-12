using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    [SerializeField]private AudioSource BGM;
    private float volume;
    [SerializeField]private Slider bgmSlider;
    [SerializeField]private bool isMainMenu;
    private const string PLAYER_PREF_BGM_VOLUME = "BGM_Volume";
    public static BGMManager Instance{get; private set;}
    private void Awake() {
        if(!Instance){
            Instance = this;
            if(!isMainMenu){
                DontDestroyOnLoad(gameObject);
                // Debug.Log("halo??");
            }
            
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start() {
        volume = PlayerPrefs.GetFloat(PLAYER_PREF_BGM_VOLUME, 0.3f);
        bgmSlider.value = volume;
        BGM.volume = volume;
    }
    private void Update() {
        if(bgmSlider == null){
            bgmSlider = GameObject.FindWithTag("BGMSlider").GetComponent<PauseUI>().GetBGMSlider();
            bgmSlider.value = volume;
        }
    }

    public void UpdateBGM_Volume(float upVolume){
        volume += upVolume;
        bgmSlider.value = volume;
        BGM.volume = volume;
        PlayerPrefs.SetFloat(PLAYER_PREF_BGM_VOLUME, volume);
    }
    public void DestroyInstance(){
        Destroy(gameObject);
    }
}
