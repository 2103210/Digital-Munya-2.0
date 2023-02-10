using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TefDara.UI
{


    public class TurnToCamera : MonoBehaviour
    {
        private Transform _thisTransform;


        private void Awake()
        {
            _thisTransform = transform;
        }

        void Update()
        {
            if (Camera.main != null)
            {
                var rotation = Camera.main.transform.rotation;
                _thisTransform.LookAt(_thisTransform.transform.position + rotation * Vector3.back, rotation * Vector3.up);
            }
        }
    }
}
