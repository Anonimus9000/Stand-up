using System;
using UnityEngine;

namespace Script.CharacterAnimationTest
{
public class TestCharacterAnimation: MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;

    private static int IsCrying = Animator.StringToHash("IsCrying");
    private static int IsDancing = Animator.StringToHash("IsDancing");
    private bool _isCrying;
    private bool _isDancing;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            _isCrying = true;
            _animator.SetBool(IsCrying, _isCrying);
        }

        if (Input.GetKey(KeyCode.S))
        {
            _isCrying = false;
            _animator.SetBool(IsCrying, _isCrying);
            
        }
        
    }
    
    
}
}