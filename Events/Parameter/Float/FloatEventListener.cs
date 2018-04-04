﻿using UnityEngine;
using UnityEngine.Events;

namespace UE.Events
{
    public class FloatEventListener : ParameterEventListener<float, FloatEvent>
    {
        [SerializeField]
        [Tooltip("Event to register with.")] 
        private FloatEvent Event;

        [SerializeField]
        [Tooltip("Response to invoke when Event is raised.")] 
        private FloatUnityEvent OnTriggered;

        protected override ParameterEvent<float, FloatEvent> GenericEvent => Event;
        protected override UnityEvent<float> GenericResponse => OnTriggered;
    }
}