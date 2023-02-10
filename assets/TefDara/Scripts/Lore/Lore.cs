using System;
using TefDara.Managers;
using TefDara.OK.Input;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace TefDara.Lore
{
    public class Lore : MonoBehaviour
    {
        public bool isFound;
        public LoreData loreData;
        [SerializeField] private bool is3D;
        [SerializeField] private TextMeshProUGUI _text;

        private bool _isPlayerInTriggerZone;
        private void OnEnable()
        {
            InputManager.Instance.OnInteract += Interact;
            _text.gameObject.SetActive(false);
            
            if (is3D)
            {
                InputManager.Instance.OnUiRotate += Rotate;
            }
        }

        private void Rotate(Vector2 mousePos)
        {
            
        }

        private void Interact()
        {
            if (_isPlayerInTriggerZone)
            {
                GameManager.Instance.OnLoreInteracted(this);
                isFound = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _text.gameObject.SetActive(true);
            _isPlayerInTriggerZone = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _text.gameObject.SetActive(false);

            _isPlayerInTriggerZone = false;
        }
    }
}