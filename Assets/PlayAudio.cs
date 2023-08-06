using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource _audioSource;
    public AudioClip _audioClip1;
    public AudioClip _audioClip2;
    public AudioClip _audioClip3;
    public AudioClip _audioClip4;
    public void PlayClip(int index)
    {
        switch (index)
        {
            case 1:
                _audioSource.PlayOneShot(_audioClip1); break;
            case 2:
                _audioSource.PlayOneShot(_audioClip2); break;
            case 3:
                _audioSource.PlayOneShot(_audioClip3); break;
            case 4:
                _audioSource.PlayOneShot(_audioClip4); break;
        }
    }
}
