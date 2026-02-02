using UITKUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Pong.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private GameInfo gameInfo;
    
        private const string PongButtonName = "Pong";
        private const string CubeButtonName = "Cube";

        private const string CubeSceneName = "scenes/UIToolKit/CubeScene";
        private const string PongSceneName = "scenes/Game";
        private const string TextFieldName = "PlayerName";
    
    
        private Button _pong;
        private Button _cube;
        private Button _clearSavedData;
        private Button _confirmDelete;
        private Button _cancelDelete;

        private VisualElement _deleteUI;
        private VisualElement _root;

        private TextField _textField;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
        }

        void OnEnable()
        {
            _pong = _root.Q<Button>(PongButtonName);
            Validation.CheckQuery(_pong, PongButtonName);
            _pong?.RegisterCallback<ClickEvent>(evt=> LoadScene(PongSceneName));
        
            _cube = _root.Q<Button>(CubeButtonName);
            Validation.CheckQuery(_cube, CubeButtonName);
            _cube?.RegisterCallback<ClickEvent>(evt => LoadScene(CubeSceneName));
            _textField = _root.Q<TextField>(TextFieldName);
            _deleteUI = _root.Q<VisualElement>("DeleteUI");
            _clearSavedData = _root.Q<Button>("DeleteDataButton");
            _clearSavedData?.RegisterCallback<ClickEvent>(evt => DeleteWindowOn());
            _confirmDelete = _root.Q<Button>("ConfirmDelete");
            _confirmDelete?.RegisterCallback<ClickEvent>(evt => ClearData());
            _cancelDelete = _root.Q<Button>("CancelDelete");
            _cancelDelete?.RegisterCallback<ClickEvent>(evt => DeleteWindowOff());
            DeleteWindowOff();

            PlayerButtons();
            EnemyButtons();
        }

        private void ClearData()
        {
            gameInfo.ResetInfo();
            DeleteWindowOff();
        }


        private void DeleteWindowOn()
        {
            _deleteUI.style.display = DisplayStyle.Flex;
        }
        private void DeleteWindowOff()
        {
            _deleteUI.style.display = DisplayStyle.None;
        }

        private void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    
        private void PlayerButtons()
        {
            var playerButtons = _root.Q<GroupBox>("PlayerGroupBox").Query<Button>().ToList();
            foreach (var btn in playerButtons)
            {
                btn.RegisterCallback<ClickEvent>(evt =>
                {
                    Color pickedColor = btn.resolvedStyle.backgroundColor;
                    gameInfo.playerColor = pickedColor;
                });
            }
        }

        private void EnemyButtons()
        {
            var EnemyButtons = _root.Q<GroupBox>("EnemyGroupBox").Query<Button>().ToList();
            foreach (var btn in EnemyButtons)
            {
                btn.RegisterCallback<ClickEvent>(evt =>
                {
                    Color pickedColor = btn.resolvedStyle.backgroundColor;
                    gameInfo.enemyColor = pickedColor;
                });
            }
        }
    }
}
