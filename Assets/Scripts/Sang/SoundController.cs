using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    public GameObject SettingSound;

    public static bool isGamePaused = false;

    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }

    public void ToggleSfx()
    {
        AudioManager.instance.ToggleSfx();
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(_musicSlider.value);
    }

    public void SfxVolume()
    {
        AudioManager.instance.SfxVolume(_sfxSlider.value);
    }

}
