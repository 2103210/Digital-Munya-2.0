using System;
using TefDara.OK.Input;
using TMPro;
using UnityEngine;

namespace TefDara.UI
{
    public class PageInteractive : Page
    {
        [SerializeField] private RectTransform mesh;
        [SerializeField] private float rotationSpeed;

        private void OnEnable()
        {
            InputManager.Instance.OnUiRotate += OnRotation;
        }

        private void OnRotation(Vector2 mousePos)
        {
            float xRot = mousePos.x * Time.deltaTime;
            float yRot = mousePos.y * Time.deltaTime;
            
             mesh.Rotate(Vector3.down, xRot);
            mesh.Rotate(Vector3.right, yRot);
            //mesh.Rotate(xRot, yRot, 0,Space.World);
        }
    }
}