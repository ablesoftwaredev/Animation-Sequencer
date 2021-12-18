﻿using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [Serializable]
    public sealed class ScaleDOTweenAction : DOTweenActionBase
    {
        public ScaleDOTweenAction(Vector3 scale, AxisConstraint axisConstraint)
        {
            this.scale = scale;
            this.axisConstraint = axisConstraint;
        }

        public ScaleDOTweenAction()
        {
        }

        public override Type TargetComponentType => typeof(Transform);
        public override string DisplayName => "Scale to Size";

        [SerializeField]
        private Vector3 scale;
        [SerializeField]
        private AxisConstraint axisConstraint;

        private Vector3? previousState;
        private GameObject previousTarget;

        protected override Tweener GenerateTween_Internal(GameObject target, float duration)
        {
            previousState = target.transform.localScale;
            previousTarget = target;
            
            TweenerCore<Vector3, Vector3, VectorOptions> scaleTween = target.transform.DOScale(scale, duration).SetEase(ease);
            scaleTween.SetOptions(axisConstraint);

            return scaleTween;
        }

        public override void ResetToInitialState()
        {
            if (!previousState.HasValue)
                return;

            previousTarget.transform.localScale = previousState.Value;
        }
    }
}
