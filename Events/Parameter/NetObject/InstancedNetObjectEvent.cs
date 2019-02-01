using System;
using Object = UnityEngine.Object;

namespace UE.Events
{
    [Serializable]
    public class InstancedNetObjectEvent : InstancedParameterEvent<object, NetObjectEvent>
    {
    }
}