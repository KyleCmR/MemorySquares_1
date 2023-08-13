using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnMouseTrigger : MonoBehaviour
{
    public AudioSource _audioSource;
    bool _isPlaying = false;
    bool _isMouseInObject = false;
    public Transform _transform;
    private Quaternion _originalRotation;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _audioSource= GetComponent<AudioSource>();
        _originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (_isPlaying)
        {
            _transform.Rotate(Vector3.up * Time.deltaTime * 50);       
        }
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
        transform.rotation = _originalRotation;
    }
}
