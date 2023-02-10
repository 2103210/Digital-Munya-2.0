using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TefDara.Misc
{
    public class RemoveComponent : MonoBehaviour
    {
        [SerializeField] private MeshCollider[] components;

        public void RemoveTheComponents()
        {

            foreach (var VARIABLE in components)
            {
                DestroyImmediate(VARIABLE);
            }

            DestroyImmediate(this);
        }

        public void UpdateComponentList()
        {

            components = GetComponentsInChildren<MeshCollider>();
        }
    }

}
