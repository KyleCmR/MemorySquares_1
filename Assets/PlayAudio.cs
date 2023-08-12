using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource _audioSource;
    [SerializeField] public AudioClip[] _audioClip;

    public void PlayClip(int index)
    {
        _audioSource.PlayOneShot(_audioClip[index]);
    }
}
