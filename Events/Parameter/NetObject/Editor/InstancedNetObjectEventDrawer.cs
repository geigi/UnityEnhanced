#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace UE.Events
{
    [CustomPropertyDrawer(typeof(InstancedNetObjectEvent))]
    public class InstancedNetObjectEventDrawer : InstancedParameterEventDrawer<object, NetObjectEvent>
    {
    }
}
#endif
