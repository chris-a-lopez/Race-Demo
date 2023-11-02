using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceObjectAnimator : MonoBehaviour
{
    const string ANIMATION_MOVING = "RaceObjectMoving";
    const string ANIMATION_IDLE = "Idle";
    private Animator _animator;


    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }

    public void PlayRunAnimation()
    {
        _animator.Play(ANIMATION_MOVING);
    }

    public void StopRunAnimation()
    {
        _animator.Play(ANIMATION_IDLE);
    }
}
