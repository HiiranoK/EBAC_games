using System.Net.NetworkInformation;
using UITKUtils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Pong.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class PauseMenu : MonoBehaviour
    {
        private VisualElement _pauseRoot;
        private InputAction _pauseAction;
        private bool _isPaused = false;
    
        private const string MainMenuButtonName = "MainMenuButton";
        private const string MainMenuSceneName = "scenes/Menu";
        private Button _mainMenuButton;

        private VisualElement _colorSelector;

        void Awake()
        {
            _pauseRoot = GetComponent<UIDocument>().rootVisualElement;
            _pauseRoot.style.display = DisplayStyle.None;
            _pauseAction = InputSystem.actions.FindAction("Cancel");
        }

        void OnEnable()
        {
            if (_pauseAction != null)
                _pauseAction.performed += OnPausePerformed;
        
            _mainMenuButton = _pauseRoot.Q<Button>(MainMenuButtonName);
            Validation.CheckQuery(_mainMenuButton, MainMenuButtonName);
            _mainMenuButton?.RegisterCallback<ClickEvent>(evt => LoadScene(MainMenuSceneName));
        }
        
        private void LoadScene(string sceneName)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneName);
        }

        void OnDisable()
        {
            if (_pauseAction != null)
                _pauseAction.performed -= OnPausePerformed;
        }

        private void OnPausePerformed(InputAction.CallbackContext context)
        {
            TogglePause();
        }
        private void TogglePause()
        {
            _isPaused = !_isPaused;
            Time.timeScale = _isPaused ? 0 : 1;
        
            _pauseRoot.style.display = _isPaused ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}