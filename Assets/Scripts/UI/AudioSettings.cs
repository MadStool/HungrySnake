using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        InitializeSlider(masterSlider, "Master");
        InitializeSlider(musicSlider, "Music");
        InitializeSlider(sfxSlider, "SFX");
    }

    private void InitializeSlider(Slider slider, string volumeParameter)
    {
        if (slider != null)
        {
            float savedVolume = PlayerPrefs.GetFloat(volumeParameter, 0.75f);
            slider.value = savedVolume;
            slider.onValueChanged.AddListener((value) => SetVolume(volumeParameter, value));
            SetVolume(volumeParameter, savedVolume);
        }
    }

    public void SetMasterVolume(float volume) => SetVolume("Master", volume);
    public void SetMusicVolume(float volume) => SetVolume("Music", volume);
    public void SetSFXVolume(float volume) => SetVolume("SFX", volume);

    private void SetVolume(string parameterName, float volume)
    {
        float volumeDB = Mathf.Log10(volume) * 20;
        if (volume <= 0.001f) volumeDB = -80f;

        audioMixer.SetFloat(parameterName, volumeDB);
        PlayerPrefs.SetFloat(parameterName, volume);
    }

    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }
}