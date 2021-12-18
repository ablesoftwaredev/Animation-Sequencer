﻿using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [Serializable]
    public abstract class AnchoredPositionMoveDOTweenActionBase : DOTweenActionBase
    {
        public override Type TargetComponentType => typeof(RectTransform);

        [SerializeField]
        private AxisConstraint axisConstraint;
        
        private RectTransform rectTransform;
        private Vector2 previousAnchorPosition;

        protected AnchoredPositionMoveDOTweenActionBase()
        {
        }

        protected AnchoredPositionMoveDOTweenActionBase(bool isRelative) : base(isRelative)
        {
        }

        protected override Tweener GenerateTween_Internal(GameObject target, float duration)
        {
            if (rectTransform == null)
            {
                rectTransform = target.transform as RectTransform;

                if (rectTransform == null)
                {
                    Debug.LogError($"{target} does not have {TargetComponentType} component");
                    return null;
                }
            }

            previousAnchorPosition = rectTransform.anchoredPosition;
            TweenerCore<Vector2, Vector2, VectorOptions> anchorPosTween = rectTransform.DOAnchorPos(GetPosition(), duration);

            anchorPosTween.SetOptions(axisConstraint);

            return anchorPosTween;
        }

        protected abstract Vector2 GetPosition();

        public override void ResetToInitialState()
        {
            if (rectTransform == null)
                return;

            rectTransform.anchoredPosition = previousAnchorPosition;
        }
    }
}
