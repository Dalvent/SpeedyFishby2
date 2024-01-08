using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShowEnergyShield : MonoBehaviour
{
    private const string HighlightFeatherMaterialField = "_Highlight_Feather";

    public AudioSource ShowShieldSound;
    public AudioSource HideShieldSound;
    public SpriteRenderer SpriteRenderer;
    public float ActiveFeather = 0.08f;
    public float DisabledFeather = 0.60f;
    public float TransitionSpeed = 1f;
    public bool IsShowing;

    public event Action FullyDispersed; 
    
    private float _currentFeatherValue;
    private float _pastFrameFeatherValue;
    private Material _material;
    private bool _lastShowingStatus;
    
    void Start()
    {
        if (SpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not assigned.");
            return;
        }

        _material = SpriteRenderer.material;
        IsShowing = false;
        _currentFeatherValue = DisabledFeather;
    }

    void Update()
    {
        if (IsShowing != _lastShowingStatus)
        {
            if(IsShowing)
                ShowShieldSound.Play();
            else
                HideShieldSound.Play();
        }

        _lastShowingStatus = IsShowing;
            
        if (!IsShowing)
        {
            _currentFeatherValue += Time.deltaTime * TransitionSpeed;
            if (_currentFeatherValue >= DisabledFeather)
            {
                _currentFeatherValue = DisabledFeather;
                SpriteRenderer.enabled = false; // Disable the SpriteRenderer
                FullyDispersed?.Invoke();
            }
        }
        else
        {
            if (!SpriteRenderer.enabled) 
                SpriteRenderer.enabled = true;

            _currentFeatherValue -= Time.deltaTime * TransitionSpeed;
            if (_currentFeatherValue <= ActiveFeather)
            {
                _currentFeatherValue = ActiveFeather;
            }
        }

        if (_pastFrameFeatherValue == _currentFeatherValue)
            return;
        
        _pastFrameFeatherValue = _currentFeatherValue;
        _material.SetFloat(HighlightFeatherMaterialField, _currentFeatherValue);
    }
}