using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LocalPlayerController : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private Transform _sprites;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private float _horiInput, _vertInput;
    private Vector2 _direction;
    private float _speed = 5f;

    private int _faceDirection;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!IsOwner) return;
        MovementHandle();
    }

    private void MovementHandle()
    {
        _horiInput = Input.GetAxis("Horizontal");
        _vertInput = Input.GetAxis("Vertical");
        _direction = new Vector2(_horiInput, _vertInput).normalized;

        _rigidbody2D.velocity = _direction * _speed;
        if (_direction.x != 0)
        {
            _faceDirection = _direction.x > 0 ? 1 : -1;
        }

        _sprites.localScale = new Vector2(_faceDirection, 1f);
        //Change animation
    }
}
