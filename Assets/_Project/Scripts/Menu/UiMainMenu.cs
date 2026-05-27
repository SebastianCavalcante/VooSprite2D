using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.Scripts.Menu
{
    public class UiMainMenu : MonoBehaviour
    {
        private Label _title;
        private Button _playButton;
        private UIDocument _menuDocument;
        
        private void Awake()
        {
            _menuDocument = GetComponent<UIDocument>();
        }

        private void Start()
        {
            _playButton = _menuDocument.rootVisualElement.Q<Button>("PlayButton");
            _playButton.style.display = DisplayStyle.Flex;
            _playButton.clickable.clicked += OpenLevelPanel;
            
            _title = _menuDocument.rootVisualElement.Q<Label>("Title");
            _title.style.display = DisplayStyle.Flex;
        }

        private void OpenLevelPanel()
        {
            _playButton.style.display = DisplayStyle.None;
            _title.style.display = DisplayStyle.None;
            MenuManager.Instance.LoadGameLevel();
        }
    }
}