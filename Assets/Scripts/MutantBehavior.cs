using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantBehavior : MonoBehaviour
{
    private Animator _animator;
    private float _timer = 0;
    private bool _isPatrolling = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _timer = 0;
        _isPatrolling = false;
    }

    private void Update()
    {
        TickTimerSetIsPatrolling();
    }

    private void TickTimerSetIsPatrolling()
    {
        _timer += Time.deltaTime;
        if (_timer >= 2)
        {
            _isPatrolling = !_isPatrolling;
            _animator.SetBool("IsPatrolling", _isPatrolling);
            _timer = 0;
        }
    }


}
