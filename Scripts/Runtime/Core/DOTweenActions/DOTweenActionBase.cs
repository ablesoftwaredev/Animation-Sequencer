﻿using System;
using DG.Tweening;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [Serializable]
    public abstract class DOTweenActionBase
    {
        public enum AnimationDirection
        {
            To, 
            From
        }
        
        
        [SerializeField]
        protected AnimationDirection direction;

        [SerializeField]
        protected CustomEase ease = CustomEase.InOutCirc;
        [SerializeField]
        protected bool isRelative;


        public virtual Type TargetComponentType { get; }
        public abstract string DisplayName { get; }

        protected DOTweenActionBase()
        {
        }
        
        protected DOTweenActionBase(bool isRelative)
        {
            this.isRelative = isRelative;
        }
        
        protected abstract Tweener GenerateTween_Internal(GameObject target, float duration);

        public Tween GenerateTween(GameObject target, float duration)
        {
            Tweener tween = GenerateTween_Internal(target, duration);
            if (direction == AnimationDirection.From)
                // tween.SetRelative() does not work for From variant of "Move To Anchored Position", it must be set
                // here instead. Not sure if this is a bug in DOTween or expected behaviour...
                tween.From(isRelative: isRelative);

            tween.SetEase(ease);
            tween.SetRelative(isRelative);
            return tween;
        }

        public abstract void ResetToInitialState();
    }
}
