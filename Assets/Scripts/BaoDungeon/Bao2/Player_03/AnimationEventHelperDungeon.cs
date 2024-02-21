using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelperDungeon : MonoBehaviour
{
    public UnityEvent OnAnimationEventTriggered, OnAttackPeformed;

    public void TriggerEvent()
    {
        OnAnimationEventTriggered?.Invoke();
    }

    public void TriggerAttack()
    {
        OnAttackPeformed?.Invoke();
    }
}