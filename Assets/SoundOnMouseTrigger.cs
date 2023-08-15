using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnMouseTrigger : MonoBehaviour
{
    public AudioSource _audioSource;
    bool _isPlaying = false;
    public Transform _transform;
    [SerializeField] public Animator _animator;

    private void Awake()
    {

    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseEnter()
    {
        if (!_audioSource.isPlaying && !_isPlaying)
        {
            _audioSource.Play();
            _isPlaying = true;
            _animator.SetBool("_isMouse", true);
        }
    }
    private void OnMouseExit()
    {
        _isPlaying = false;
        _animator.SetBool("_isMouse", false);
        //transform.rotation = _originalRotation;
    }
}
