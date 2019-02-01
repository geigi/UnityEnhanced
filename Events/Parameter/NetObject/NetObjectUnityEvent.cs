using System;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace UE.Events
{
    [Serializable]
    public class NetObjectUnityEvent : UnityEvent<object>
    {
    }
}