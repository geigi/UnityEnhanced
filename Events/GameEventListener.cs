﻿// ----------------------------------------------------------------------------
// Based on Work from Ryan Hipple, Unite 2017 - Game Architecture with Scriptable Objects
// ----------------------------------------------------------------------------

using UE.Instancing;
using UnityEngine;
using UnityEngine.Events;

namespace UE.Events
{
    public class GameEventListener : InstanceObserver
    {
        [Tooltip("When this is checked, the listener will still work when the game object is disabled.")]
        [SerializeField]
        private bool persistent;

        [Tooltip("Event to register with.")] [SerializeField]
        internal GameEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        private void OnEnable()
        {
            if (!persistent) Event.RegisterListener(this, key);
        }

        private void OnDisable()
        {
            if (!persistent) Event.UnregisterListener(this, key);
        }

        private void Awake()
        {
            if (persistent) Event.RegisterListener(this, key);
        }

        private void OnDestroy()
        {
            if (persistent) Event.UnregisterListener(this, key);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }

        public override IInstanciable GetTarget()
        {
            return Event;
        }
    }
}