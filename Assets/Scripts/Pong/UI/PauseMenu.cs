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
        [SerializeField] private GameInfo gameInfo;
        
        [SerializeField] private SpriteRenderer playerSprite;
        [SerializeField] private SpriteRenderer enemySprite;
    
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

            string  sceneName = SceneManager.GetActiveScene().name;
            _colorSelector = _pauseRoot.Q<VisualElement>("ColorSelector");
            _colorSelector.style.display = DisplayStyle.None;
            if (sceneName == "Game")
            {
                _colorSelector.style.display = DisplayStyle.Flex;
                PlayerButtons();
                EnemyButtons();
                ChangeColor(playerSprite,gameInfo.playerColor);
                ChangeColor(enemySprite,gameInfo.enemyColor);
            }
            
        }

        private void PlayerButtons()
        {
            var playerButtons = _pauseRoot.Q<GroupBox>("PlayerGroupBox").Query<Button>().ToList();
            foreach (var btn in playerButtons)
            {
                btn.RegisterCallback<ClickEvent>(evt =>
                {
                    Color pickedColor = btn.resolvedStyle.backgroundColor;
                    gameInfo.playerColor = pickedColor;
                    ChangeColor(playerSprite, pickedColor);
                });
            }
        }

        private void EnemyButtons()
        {
            var EnemyButtons = _pauseRoot.Q<GroupBox>("EnemyGroupBox").Query<Button>().ToList();
            foreach (var btn in EnemyButtons)
            {
                btn.RegisterCallback<ClickEvent>(evt =>
                {
                    Color pickedColor = btn.resolvedStyle.backgroundColor;
                    gameInfo.enemyColor = pickedColor;
                    ChangeColor(enemySprite, pickedColor);
                });
            }
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

        private void ChangeColor(SpriteRenderer sprite, Color color)
        {
            sprite.color = color;
        }
    }
}