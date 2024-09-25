using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    public static EffectSoundManager Instance;
    private AudioSource _audioSource;
    public AudioClip ButtonSound;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(this);
        _audioSource = GetComponent<AudioSource>();
    }

    public void ButtonEffect()
    {
        _audioSource.clip = ButtonSound;
        _audioSource.Play();
    }
}
