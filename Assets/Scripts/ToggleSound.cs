using UnityEngine;
using UnityEngine.Audio;

public class MuteController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    public void ToggleSound(bool soundOn)
    {
        if (soundOn)
            _audioMixer.SetFloat("Master", 0f);
        else
            _audioMixer.SetFloat("Master", -80f);
    }
}