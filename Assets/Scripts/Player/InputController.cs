using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class InputController : MonoBehaviour
{
    private CharacterController _characterController;
    private InputActionScheme _inputActionScheme;
    private InputAction _move;
    
    public delegate void OnMoveEvent( Vector2 move );
    public event OnMoveEvent OnMoveInput;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputActionScheme = new InputActionScheme();
        _move = _inputActionScheme.Player.Move;
    }

    private void Start()
    {
        _move.started += StartMove;
        _move.canceled += EndMove;
    }

    private void OnEnable()
    {
        _inputActionScheme.Enable();
    }
    
    private void OnDisable()
    {
        _inputActionScheme.Disable();
    }
    
    private void StartMove( InputAction.CallbackContext ctx )
    {
        StartCoroutine( _MoveHandler() );
    }

    private void EndMove( InputAction.CallbackContext ctx )
    {
    }

    private IEnumerator _MoveHandler()
    {
        while ( _move.IsInProgress() )
        {
            OnMoveInput?.Invoke( GetMoveDelta() );
            yield return null;
        }
    }

    private Vector2 GetMoveDelta() => _move.ReadValue<Vector2>() * Time.deltaTime;
    
}