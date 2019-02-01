using UnityEngine;
using UnityEngine.Events;

namespace UE.Events
{
    [AddComponentMenu("Unity Enhanced/Events/Object Event Listener", 1)]
    public class NetObjectEventListener : ParameterEventListener<object, NetObjectEvent>
    {
        [SerializeField]
        [Tooltip("Event to register with.")] 
        private NetObjectEvent Event;

        [SerializeField]
        [Tooltip("Response to invoke when Event is raised.")] 
        private NetObjectUnityEvent OnTriggered;

        protected override ParameterEvent<object, NetObjectEvent> GenericEvent => Event;
        protected override UnityEvent<object> GenericResponse => OnTriggered;
    }
}