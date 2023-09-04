using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance{get; private set;}
    [SerializeField]private AudioSource SoundEffect_Run_Grass, SoundEffect_Run_House, Chest_Open, Chest_Close, DoorOpen, FlipPage, Mailbox, Mortar, OpenEnvelope, PotionJadi, StartFire, WalkSoundForTimeLineOnly, CauldronBoil, MenuSound, WindSoundForTimeLineOnly, TileWaterMoveSound;
    
    private float volume;
    [SerializeField]private Slider soundSlider;
    private const string PLAYER_PREF_SOUND_VOLUME = "Sound_Volume";

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        volume = PlayerPrefs.GetFloat(PLAYER_PREF_SOUND_VOLUME, 0.3f);
        soundSlider.value = volume;
        UpdateAllVolume();
        
    }


    public void UpdateSound_Volume(float upVolume){
        volume += upVolume;
        soundSlider.value = volume;
        PlayerPrefs.SetFloat(PLAYER_PREF_SOUND_VOLUME, volume);
        UpdateAllVolume();
        
    }

    private void UpdateAllVolume(){
        if(SoundEffect_Run_Grass)SoundEffect_Run_Grass.volume = volume;
        if(SoundEffect_Run_House)SoundEffect_Run_House.volume = volume;
        if(Chest_Open)Chest_Open.volume = volume;
        if(Chest_Close)Chest_Close.volume = volume;
        if(DoorOpen)DoorOpen.volume = volume;
        if(FlipPage)FlipPage.volume = volume;
        if(Mailbox)Mailbox.volume = volume;
        if(Mortar)Mortar.volume = volume;
        if(OpenEnvelope)OpenEnvelope.volume = volume;
        if(PotionJadi)PotionJadi.volume = volume;
        if(StartFire)StartFire.volume = volume;
        if(WalkSoundForTimeLineOnly)WalkSoundForTimeLineOnly.volume = volume;
        if(WindSoundForTimeLineOnly)WindSoundForTimeLineOnly.volume = volume;
        if(CauldronBoil)CauldronBoil.volume = volume;
        if(MenuSound)MenuSound.volume = volume;
        if(TileWaterMoveSound)TileWaterMoveSound.volume = volume;
        
    }
    public void PlaySFX_PlayerWalk(){
        if(WitchGameManager.Instance.GetPlace() == WitchGameManager.Place.indoor){
            SoundEffect_Run_House.Play();
        }
        else if(WitchGameManager.Instance.GetPlace() == WitchGameManager.Place.outdoor){
            SoundEffect_Run_Grass.Play();
        }
    }
    public void StopSFX_PlayerWalk(){
        if(WitchGameManager.Instance.GetPlace() == WitchGameManager.Place.indoor){
            SoundEffect_Run_House.Stop();
        }
        else if(WitchGameManager.Instance.GetPlace() == WitchGameManager.Place.outdoor){
            SoundEffect_Run_Grass.Stop();
        }
    }
    public bool isPlayedSFX_PlayerWalk(){
        if(WitchGameManager.Instance.GetPlace() == WitchGameManager.Place.indoor){
            return SoundEffect_Run_House.isPlaying;
        }
        else if(WitchGameManager.Instance.GetPlace() == WitchGameManager.Place.outdoor){
            return SoundEffect_Run_Grass.isPlaying;
        }
        return SoundEffect_Run_House.isPlaying;
    }
    
    public void PlayChestOpen(){
        Chest_Open.Play();
    }
    public void PlayChestClose(){
        Chest_Close.Play();
    }

    public void PlayDoorOpen(){
        DoorOpen.Play();
    }
    public void PlayFlipPage(){
        FlipPage.Play();
    }
    public void PlayMailbox(){
        Mailbox.Play();
    }
    public void PlayMortar(){
        Mortar.Play();
    }
    public void PlayOpenEnvelope(){
        OpenEnvelope.Play();
    }
    public void PlayPotionJadi(){
        PotionJadi.Play();
    }
    public void PlayStartFire(){
        StartFire.Play();
    }
    public void PlayMenuSound()
    {
        if(MenuSound)MenuSound.Play();
    }
    public void PlayWaterMoveSound()
    {
        TileWaterMoveSound.Play();
    }
    public void StopWaterMoveSound()
    {
        TileWaterMoveSound.Stop();
    }
    public bool isWaterMovePlay()
    {
        return TileWaterMoveSound.isPlaying;
    }
}
