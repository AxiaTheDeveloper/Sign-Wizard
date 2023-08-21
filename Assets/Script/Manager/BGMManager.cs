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
    [SerializeField]private float fadeInDurationMax = 0.5f;
    private float fadeInDuratiom;
    private void Awake() {
        if(!Instance){
            if(!PlayerPrefs.HasKey(PLAYER_PREF_BGM_VOLUME))PlayerPrefs.SetFloat(PLAYER_PREF_BGM_VOLUME, 0.3f);
            volume = PlayerPrefs.GetFloat(PLAYER_PREF_BGM_VOLUME);
            // Debug.Log(volume);
            fadeInDuratiom = 0;
            BGM.volume = 0f;
            
            BGM.Play();
            StartCoroutine(fadeIn());
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
        
        bgmSlider.value = volume;
        // BGM.volume = volume;
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
    private IEnumerator fadeIn()
    {
        while(fadeInDuratiom < fadeInDurationMax )
        {
            fadeInDuratiom += 0.01f;
            // Debug.Log(fadeInDuratiom + " " + Mathf.Lerp(0f, volume, fadeInDuratiom/fadeInDurationMax));
            BGM.volume = Mathf.Lerp(0f, volume, fadeInDuratiom/fadeInDurationMax);       
            yield return new WaitForSeconds(0.1f);
        }
        BGM.volume = volume;
    }
}
