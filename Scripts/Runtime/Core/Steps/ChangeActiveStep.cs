﻿#if DOTWEEN_ENABLED
using System;
using DG.Tweening;
using UnityEngine;

namespace BrunoMikoski.AnimationSequencer
{
    [Serializable]
    public sealed class ChangeActiveStep : AnimationStep
    {
        public override string DisplayName => DisplayNames.ChangeActiveStep;

        [SerializeField]
        private GameObject targetGameObject;
        public GameObject TargetGameObject
        {
            get => targetGameObject;
            set => targetGameObject = value;
        }

        [SerializeField]
        private bool active;
        public bool Active
        {
            get => active;
            set => active = value;
        }

        private bool wasActive;

        public override void AddTween(Sequence animationSequence)
        {
            wasActive = targetGameObject.activeSelf;
            if (wasActive == active)
                return;

            Sequence behaviourSequence = DOTween.Sequence();
            behaviourSequence.SetDelay(Delay);
            behaviourSequence.AppendCallback(() => { targetGameObject.SetActive(active); });

            if (FlowType == FlowType.Join) animationSequence.Join(behaviourSequence);
            else animationSequence.Append(behaviourSequence);
        }

        public override void Reset()
        {
            targetGameObject.SetActive(wasActive);
        }

        public override string GetDisplayNameForEditor(int index)
        {
            string display = "NULL";
            if (targetGameObject != null)
                display = targetGameObject.name;
            return $"{index}. Set {display} Active: {active}";
        }
    }
}
#endif