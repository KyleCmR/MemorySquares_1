using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnMouseTrigger : MonoBehaviour
{
    public AudioSource _audioSource;
    bool _isPlaying = false;
    public Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _audioSource= GetComponent<AudioSource>();
    }
    private void OnMouseEnter()
    {
        if (!_audioSource.isPlaying && !_isPlaying)
        {
            _audioSource.Play();
            _isPlaying = true;
        }
        //_transform.Rotate(2, 4, 0);
    }
    private void OnMouseExit()
    {
        _isPlaying = false;
        //_transform.Rotate(-2, -4, 0);
    }
}
