using System;
using System.Collections;
using TefDara.Lore;
using TefDara.OK.Input;
using TefDara.UI;
using TMPro;
using UnityEngine;

namespace TefDara.Managers
{
    public enum GameState
    {
        Running,
        UI,
        Paused,
        Lore
    }

    public class GameManager : Singleton<GameManager>
    {
        public event Action<GameState, GameState> GameStateChanged;
        [SerializeField] private FPSController playerController;
        [SerializeField] private Page welcomeScreen;
        [SerializeField] private PageInteractive loreUi;
        [SerializeField] private Page messagePage;
        [SerializeField] private int textLoreCount;
        [SerializeField] private int ivoryLoreCount;
        public GameState currentGameState;
        private GameState _prevGameState;
        private Page _currentPage;
        private Lore.Lore _currentLoreObject;
        private int currentTextLoreCount;
        private int currentIvoryLoreCount;
        protected override void Awake()
        {
            base.Awake();
            UpdateGameState(GameState.UI);
            welcomeScreen.Close();
            loreUi.Close();
        }

        private void UpdateGameState(GameState state)
        {
            _prevGameState = currentGameState;
            currentGameState = state;

            switch (currentGameState)
            {
                case GameState.Running:
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.enabled = true;
                    break;

                case GameState.Paused:
                    Cursor.lockState = CursorLockMode.None;
                    playerController.enabled = false;
                    break;

                case GameState.UI:
                    Cursor.lockState = CursorLockMode.None;
                    playerController.enabled = false;
                    break;
                case GameState.Lore:
                    Cursor.lockState = CursorLockMode.None;
                    playerController.enabled = false;
                    break;
                    
            }

            GameStateChanged?.Invoke(currentGameState, _prevGameState);
        }

        private void ToggleUICamera(bool state)
        {
            //uiCamera.gameObject.SetActive(state);
        }

        public void OnPageOpened(Page page)
        {
            _currentPage = page;
            UpdateGameState(GameState.UI);
        }

        public void ClosePage(Page page)
        {
            page.Close();
        }

        public void OnLoreInteracted(Lore.Lore lore)
        {
            UpdateGameState(GameState.Lore);
            LoreData loreData = lore.loreData;
            _currentLoreObject = lore;
            _currentLoreObject.gameObject.SetActive(false);
            if (!lore.isFound)
            {
                ShowLoreMessage(lore, loreData);
                return;
            }

            OpenLorePage(loreData);
        }

        private void OpenLorePage(LoreData loreData)
        {
            _currentPage = loreUi;
            loreUi.image.sprite = loreData.image;
            loreUi.text.text = loreData.text;
            loreUi.Open();
        }

        private void ShowLoreMessage(Lore.Lore lore, LoreData loreData)
        {
            StartCoroutine(LoreMessageRoutine(lore, loreData));
        }

        private IEnumerator LoreMessageRoutine(Lore.Lore lore, LoreData loreData)
        {
            string type = loreData.loreType.ToString();
            int currentCount;
            int totalCount;
            
            switch (loreData.loreType)
            {
                case LoreType.Ivory:
                    currentIvoryLoreCount++;
                    currentCount = currentIvoryLoreCount;
                    totalCount = ivoryLoreCount;
                    break;
                case LoreType.Text:
                    currentTextLoreCount++;
                    currentCount = currentTextLoreCount;
                    totalCount = textLoreCount;
                    break;
                default:
                    currentCount = 0;
                    totalCount = 0;
                    break;
            }

            string message = type + " " + currentCount + " of" + " " + totalCount + " discovered";
            
            messagePage.gameObject.SetActive(true);
            messagePage.text.text = message;
            
            yield return new WaitForSeconds(2f);
            
            messagePage.gameObject.SetActive(false);
            OpenLorePage(loreData);
        }

        public void CloseLoreUI()
        {
            _currentLoreObject.gameObject.SetActive(true);
            PlayGame();
        }
        
        public void PlayGame()
        {
            if(_currentPage != null && _currentPage.isOpen)
                _currentPage.Close();
            
            UpdateGameState(GameState.Running);
        }

        public void PauseGame()
        {
            
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        
    }
}
