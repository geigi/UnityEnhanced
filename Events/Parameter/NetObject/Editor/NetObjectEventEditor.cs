#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace UE.Events
{
    [CustomEditor(typeof(NetObjectEvent), true)]
    [CanEditMultipleObjects]
    public class NetObjectEventEditor : ParameterEventEditor<object, NetObjectEvent>
    {
    }
}
#endif