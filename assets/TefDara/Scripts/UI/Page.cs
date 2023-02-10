using System;
using TefDara.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TefDara.UI
{
    public class Page : MonoBehaviour
    {
        public Image image;
        public TextMeshProUGUI text;
        public bool isOpen;
        protected virtual void OnValidate()
        {
            text = GetComponentInChildren<TextMeshProUGUI>(true);
        }

        public void Open()
        {
            gameObject.SetActive(true);
            GameManager.Instance.OnPageOpened(this);
            isOpen = true;
        }

        public void Close()
        {
            gameObject.SetActive(false);
            isOpen = false;
            Debug.Log("Close");
        }
    }
}