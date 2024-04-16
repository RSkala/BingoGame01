using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayback : MonoBehaviour
{
    [SerializeField] AudioClip _buttonClickClip;
    [SerializeField] AudioClip _daubCorrectClip;
    [SerializeField] AudioClip _daubWrongClip;

    AudioSource _buttonClickSource;
    AudioSource _daubCorrectSource;
    AudioSource _daubWrongSource;

    public static AudioPlayback Instance { get; private set;}

    public enum SFX
    {
        ButtonClick,
        CorrectDaub,
        WrongDaub
    }

    void Start()
    {
        if(Instance !=  null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        CreateAudioSources();
    }

    void CreateAudioSources()
    {
        CreateAudioSourceChild(out _buttonClickSource, _buttonClickClip, "ButtonClickSource");
        CreateAudioSourceChild(out _daubCorrectSource, _daubCorrectClip, "DaubCorrectSource");
        CreateAudioSourceChild(out _daubWrongSource, _daubWrongClip, "DaubWrongSource");
    }

    void CreateAudioSourceChild(out AudioSource audioSource, AudioClip audioClip, string audioSourceName)
    {
        GameObject audioSourceGO = new GameObject(audioSourceName);
        audioSource = audioSourceGO.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.transform.parent = transform;
        audioSource.clip = audioClip;
    }

    public void PlaySound(SFX sfx)
    {
        switch(sfx)
        {
            case SFX.ButtonClick:
                _buttonClickSource.Stop();
                _buttonClickSource.Play();
                break;

            case SFX.CorrectDaub:
                _daubCorrectSource.Stop();
                _daubCorrectSource.Play();
                break;

            case SFX.WrongDaub:
                _daubWrongSource.Stop();
                _daubWrongSource.Play();
                break;
        }
    }
}
