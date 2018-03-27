﻿using UnityEngine;
using UnityEngine.Events;

namespace UE.Events
{
    [CreateAssetMenu(menuName = "Events/Event(bool)")]
    public class BoolEvent : ParameterEvent<bool>
    {
        [SerializeField]
        private BoolUnityEvent OnTriggered;

        protected override UnityEvent<bool> OnEventTriggered => OnTriggered;
    }
}