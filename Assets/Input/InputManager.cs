using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class InputManager : MonoBehaviour
{
    private Grid _grid;
    public static Vector2 movement;

    private PlayerInput _playerInput;
    private InputAction _moveAction;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _grid = GameObject.Find("Grid").GetComponent<Grid>();
        
    }

    void Update()
    {
    }
}
