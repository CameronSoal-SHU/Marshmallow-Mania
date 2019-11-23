using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer = null;

    [SerializeField]
    Slider masterSlider = null;
    [SerializeField]
    Slider musicSlider = null;
    [SerializeField]
    Slider sfxSlider = null;

    [SerializeField]
    float defaultValue = 0.75f;

    void Start()
    {
        // Fetch current values from PlayerPrefs
        audioMixer.SetFloat("Master", ToDb(PlayerPrefs.GetFloat("Master", defaultValue)));
        audioMixer.SetFloat("Music", ToDb(PlayerPrefs.GetFloat("Music", defaultValue)));
        audioMixer.SetFloat("SFX", ToDb(PlayerPrefs.GetFloat("SFX", defaultValue)));
        if (masterSlider)
        {
            masterSlider.value = PlayerPrefs.GetFloat("Master", defaultValue);
        }
        if (musicSlider)
        {
            musicSlider.value = PlayerPrefs.GetFloat("Music", defaultValue);
        }
        if (sfxSlider)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFX", defaultValue);
        }
    }

    // Update levels (via slider)
    public void SetMasterLevel(float lvl)
    {
        audioMixer.SetFloat("Master", ToDb(lvl));
        PlayerPrefs.SetFloat("Master", lvl);
    }
    public void SetMusicLevel(float lvl)
    {
        audioMixer.SetFloat("Music", ToDb(lvl));
        PlayerPrefs.SetFloat("Music", lvl);
    }
    public void SetSFXLevel(float lvl)
    {
        audioMixer.SetFloat("SFX", ToDb(lvl));
        PlayerPrefs.SetFloat("SFX", lvl);
    }

    // Convert from percentage (0,1) to Db
    float ToDb(float x)
    {
        float dbValue;
        if (Mathf.Approximately(x, 0)) dbValue = -144;
        else
        {
            dbValue = 20 * Mathf.Log(x);
        }
        return dbValue;
    }
}
