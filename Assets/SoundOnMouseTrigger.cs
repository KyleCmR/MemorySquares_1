using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnMouseTrigger : MonoBehaviour
{
    public AudioSource _audioSource;
    bool _isPlaying = false;

    private void Start()
    {
        _audioSource= GetComponent<AudioSource>();
    }
    private void OnMouseEnter()
    {
        if (!_audioSource.isPlaying && !_isPlaying)
        {
            _audioSource.Play();
            _isPlaying = true;
        }
    }
    private void OnMouseExit()
    {
        _isPlaying = false;
    }
}
