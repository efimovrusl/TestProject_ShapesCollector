using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A bridge between given InputController and CharacterController
/// </summary>
[DisallowMultipleComponent,
 RequireComponent( typeof( CharacterController ) ),
 RequireComponent( typeof( InputController ) )]
public class LegsComponent : MonoBehaviour
{
    private CharacterController _characterController;
    private InputController _inputController;

    private float currentFreeFallSpeed = 0;

    [SerializeField, Range(2, 32)] private const float SpeedModifier = 8;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputController = GetComponent<InputController>();
    }

    private void Start()
    {
        _inputController.OnMoveInput += vec2 =>
        {
            if ( IsGrounded )
            {
                vec2 *= SpeedModifier;
                _characterController.SimpleMove( new Vector3( vec2.x, 0, vec2.y ) );
            }
        };
    }

    private void Update()
    {
        // faking earth gravity
        if ( !IsGrounded )
        {
            // free-fall acceleration
            currentFreeFallSpeed += -9.81f * Time.deltaTime; 
            _characterController.Move( currentFreeFallSpeed * Time.deltaTime * Vector3.up );
        }
        else
        {
            {currentFreeFallSpeed = 0;}
        }
    }

    // If a player will be allowed to jump, this one has to be implemented
    private bool IsGrounded => _characterController.isGrounded;
    
    
    
}