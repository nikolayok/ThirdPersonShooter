using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;
    private int _moveXAnimationParameterId;
    private int _moveZAnimationParameterId;
    private Vector2 _animationVelocity;
    [SerializeField] private float _animationSmoothTime = 0.1f;
    private int _jumpAnimation;
    private float _animationPlayTransition = 0.15f;
    private Vector2 _currentAnimationBlendVector;

    private int _recoilAnimation;

    private void Awake() 
    {
        _animator = GetComponent<Animator>();

        _jumpAnimation = Animator.StringToHash("Pistol Jump");
        _moveXAnimationParameterId = Animator.StringToHash("MoveX");
        _moveZAnimationParameterId = Animator.StringToHash("MoveZ");
        _recoilAnimation = Animator.StringToHash("PistolShootRecoil");

    }

    public Vector2 GetCurrentAnimationBlendVector()
    {
        return _currentAnimationBlendVector;
    }

    public void PlayJumpAnimation()
    {
        _animator.CrossFade(_jumpAnimation, _animationPlayTransition);
    }

    public void SmoothBlendAnimation(Vector2 input)
    {
        _currentAnimationBlendVector = Vector2.SmoothDamp(_currentAnimationBlendVector, input, ref _animationVelocity, _animationSmoothTime);
    }

    public void AnimateMoving()
    {
        _animator.SetFloat(_moveXAnimationParameterId, _currentAnimationBlendVector.x);
        _animator.SetFloat(_moveZAnimationParameterId, _currentAnimationBlendVector.y);       
    }

    public void PistolShoot()
    {
        _animator.CrossFade(_recoilAnimation, _animationPlayTransition);
    }
}
