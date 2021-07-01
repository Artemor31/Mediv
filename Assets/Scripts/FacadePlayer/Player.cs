using System;
using UnityEditor;
using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    private InputSystem _input;
    private PlayerFacade _facade;

    public void Init(InputSystem input, PlayerFacade facade)
    {
        _input = input;
        _facade = facade;
    }
}