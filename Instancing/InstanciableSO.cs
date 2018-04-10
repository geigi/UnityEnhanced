﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UE.Common;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace UE.Instancing
{
    /// <inheritdoc />
    /// <summary>
    /// This class enables Instancing for ScriptableObjects.  This needs to be inherited.
    /// After that, all instanced properties must be accesses via Instance(key). The key
    /// is used for a lookup in a dictionary. It is defined in an InstanceObserver to keep
    /// track of the different instaces of this SO. 
    /// </summary>
    /// <typeparam name="T">Derived type</typeparam>
    public abstract class InstanciableSO<T> : ScriptableObject, IInstanciable where T : InstanciableSO<T>
    {
        [SerializeField, HideInInspector] private bool instanced;

        /// <summary>
        /// In this dictionary the instances of the SO are stored.
        /// </summary>
        private Dictionary<Object, T> instances;


        public virtual bool Instanced => instanced;
        public int InstanceCount => instances?.Count ?? 0;

        public class InstanciableEvent : UnityEvent<T>
        {
        }
        
        /// <summary>
        /// This event is triggered whenever instances are added or removed.
        /// </summary>
        public readonly InstanciableEvent OnInstancesChanged = new InstanciableEvent();

        /// <summary>
        /// Removes references to all instances of this ScriptableObject.
        /// </summary>
        public void Clear()
        {
//            instances = instanced ? new Dictionary<Object, T> {{this, (T) this}} : null;
            instances = instanced ? new Dictionary<Object, T> { } : null;
            OnInstancesChanged.Invoke(this as T);
        }

        public ReadOnlyCollection<T> GetInstances()
        {
            if (instances == null)
            {
                return new ReadOnlyCollection<T>(new List<T>());
            }
            
            return new ReadOnlyCollection<T>(instances.Values.ToArray());
        }

        public Object[] Keys => instances?.Keys.ToArray();

        /// <summary>
        /// Returns an instance of this scriptable object based on the given key object.
        /// Returns the main object if it is null or instancing is not enabled. Should be
        /// used to access all instanced properties of this ScriptableObject.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Instance(Object key)
        {
            if (!instanced) return (T) this;
            if (key == null)
            {
                Logging.Warning(this, "Accessing the main object of an instanced system. This is " +
                                      "probably not intended. Did you forget to assign an instance key?");
                return (T) this;
            }

            if (instances == null) instances = new Dictionary<Object, T>();

            if (!instances.ContainsKey(key))
            {
                var instance = CreateInstance<T>();
                instance.name += "_" + key.GetInstanceID();
                instances.Add(key, instance);
                OnInstancesChanged.Invoke(this as T);
            }

            return instances[key];
        }

        
//        public T Instance(int hashInstanceId)
//        {
//            
//        }
    }

    /// <summary>
    /// This interface is implemented by InstanciatedSO and simplifies casting when generic parameter is unknown.
    /// </summary>
    public interface IInstanciable
    {
        /// <summary>
        /// Is instancing enabled for this object?
        /// </summary>
        bool Instanced { get; }

        /// <summary>
        /// How many instances are there?
        /// </summary>
        int InstanceCount { get; }

        /// <summary>
        /// Returns a collection of instance keys.
        /// </summary>
        Object[] Keys { get; }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(InstanciableSO<>), true)]
    [CanEditMultipleObjects]
    public class InstanciableSOEditor : Editor
    {
        private SerializedProperty m_Script;
        private SerializedProperty instanced;

        protected virtual void OnEnable()
        {
            m_Script = serializedObject.FindProperty("m_Script");
            instanced = serializedObject.FindProperty("instanced");
        }

        public override void OnInspectorGUI()
        {
            var instancedSO = target as IInstanciable;

            serializedObject.Update();

            GUI.enabled = false;
            EditorGUILayout.ObjectField(m_Script);
            GUI.enabled = true;


            const string tooltipInstancing =
                "When this is checked, this Object is automatically instaced at runtime. It then acts as a " +
                "template that can be reused. Scripts that utilize instancing need to inherit from " +
                "InstanceObserver.";

            instanced.boolValue = EditorGUILayout.Toggle(
                new GUIContent("Instanced", tooltipInstancing), instanced.boolValue);

            const string tooltipNo = "The number of instances currently referenced.";

            if (instancedSO.Instanced)
            {
                EditorGUI.indentLevel++;

//                EditorGUILayout.LabelField(new GUIContent("No. Instances", tooltipNo),
//                    new GUIContent(instancedSO.InstanceCount.ToString()));

                var keys = instancedSO.Keys;

                if (keys != null)
                {
                    EditorGUILayout.Space();
                    DrawInstanceListHeader();
                    
                    foreach (var key in keys)
                    {
                        DrawInstance(key);
                    }
                    
                    EditorGUILayout.Space();
                }


                EditorGUI.indentLevel--;
            }
            
            serializedObject.ApplyModifiedProperties();

            OnInspectorGUITop();

            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, "m_Script");
            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void OnInspectorGUITop()
        {
        }
        
        protected virtual void DrawInstanceListHeader()
        {
            EditorGUILayout.LabelField("Name", "Key");
        }

        protected virtual void DrawInstance(Object key)
        {
//            EditorGUILayout.LabelField(key.name);
            EditorGUILayout.LabelField(key.name, key.GetHashCode().ToString());
        }
    }
#endif
}