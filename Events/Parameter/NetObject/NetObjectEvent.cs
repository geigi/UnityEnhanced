using UnityEngine;
using UnityEngine.Events;

namespace UE.Events
{
    [CreateAssetMenu(menuName = "Events/Event(NetObject)")]
    public class NetObjectEvent : ParameterEvent<object, NetObjectEvent>
    {
        [SerializeField]
        private NetObjectUnityEvent OnTriggered = new NetObjectUnityEvent();

        protected override UnityEvent<object> OnEventTriggered => OnTriggered;
    }
}