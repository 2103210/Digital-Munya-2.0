using System;
using UnityEditor;

namespace TefDara.Managers
{
    public class UIManager: Singleton<UIManager>
    {
        private void OnEnable()
        {
            GameManager.Instance.GameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState currentState, GameState prevState)
        {
                
        }
    }
}