using System;
using UnityEngine;

namespace TefDara.Managers
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;
        public static T Instance
        {
            get => _instance;
            set
            {
                if (_instance == null)
                {
                    _instance = value;
                }
                else if (_instance != value)
                {
                    Destroy(value.gameObject);
                }
            }
        }
        
        public static bool IsInitialised
        {
            get => _instance != null;
        }

        protected virtual void Awake()
        {
            Instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}