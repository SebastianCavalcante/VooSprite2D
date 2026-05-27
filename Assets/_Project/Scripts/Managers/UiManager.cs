using System;
using _Project.Scripts.Coletables;
using _Project.Scripts.Data;
using _Project.Scripts.Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.Scripts.Managers
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager Instance;
        
        private GameObject _player;
        private UIDocument _document;
        
        private Label _lifeLabel;
        private Label _timerLabel;
        private Label _shieldLabel;
        private Label _timerCountLabel;
        private Label _currentScoreLabel;
        private Label _hightScoreLabel;
        private Label _gameOverLabel;
        
        private Image _lifeImage;
        private Image _timerImage;
        private Image _shildImage;
        
        private Button _restartButton;
        private Button _easyButton;
        private Button _normalButton;
        private Button _hardButton;
        
        private readonly Color32 _opacityColor = new Color32(255, 255, 255, 10);
        private readonly Color32 _visibleColor = new Color32(255, 255, 255, 255);
        private readonly Color32 _negativeColor = new Color32(255, 0, 0, 255);
        private readonly Color32 _positiveColor = new Color32(33, 255, 0, 255);
        
        private void Awake()
        {
            Instance = this;
            _document = GetComponent<UIDocument>();
        }

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            
            _gameOverLabel = _document.rootVisualElement.Q<Label>("gameOverLabel");
            _gameOverLabel.style.display = DisplayStyle.None;
            _currentScoreLabel = _document.rootVisualElement.Q<Label>("currentScore");
            _hightScoreLabel = _document.rootVisualElement.Q<Label>("hightScore");
            int hightScore = LoadData.Instance.LoadHightScore();
            _hightScoreLabel.text = hightScore.ToString();
            _timerLabel = _document.rootVisualElement.Q<Label>("timerLabel");
            _timerCountLabel = _document.rootVisualElement.Q<Label>("timerCount");
            _timerImage = _document.rootVisualElement.Q<Image>("timerImage");
            _timerImage.tintColor = _opacityColor;
            _timerCountLabel.style.color = new StyleColor(_opacityColor);
            _timerCountLabel.text = "00:00";
            
            _shieldLabel = _document.rootVisualElement.Q<Label>("shieldCount");
            _shieldLabel.style.color = new StyleColor(_opacityColor);
            _shildImage = _document.rootVisualElement.Q<Image>("shieldImage");
            _shildImage.tintColor = _opacityColor;

            _lifeLabel = _document.rootVisualElement.Q<Label>("lifeCount");
            _lifeImage = _document.rootVisualElement.Q<Image>("lifeImage");
            _lifeImage.tintColor = _visibleColor;
            
            if (_player != null  && _player.TryGetComponent(out Player.PlayerLife playerLife))
            {
                _lifeLabel.text = playerLife.Life.ToString();
            }
            _lifeLabel.style.color = new StyleColor(_positiveColor);
            
            _restartButton = _document.rootVisualElement.Q<Button>("restartButton");
            _restartButton.clickable.clicked += () => GameManager.Instance.RestartGame();
            _restartButton.style.display = DisplayStyle.None;
        }

        private void Update()
        {
            _currentScoreLabel.text = $"{TimeScore.Instance.currentScore}";
        }

        private void OnEnable()
        {
            GameEvents.EventShieldTimeChanged += UpdateShieldLabel;
            GameEvents.EventReserveTimeChaged += UpdateTimerRerserve;
            GameEvents.EventGameTimeChanged += UpdateGameTime;
            GameEvents.EventGameOverTriggered += ShowGameOverCase;
        }

        private void OnDisable()
        {
            GameEvents.EventShieldTimeChanged -= UpdateShieldLabel;
            GameEvents.EventReserveTimeChaged -= UpdateTimerRerserve;
            GameEvents.EventGameTimeChanged -= UpdateGameTime;
            GameEvents.EventGameOverTriggered -= ShowGameOverCase;
        }
        
        public void ShowRestartButton()
        {
            _restartButton.style.display = DisplayStyle.Flex;
        }

        public void UpdateLifeCountEvent(int bonus)
        {
            Color targetColor;
            if (bonus == 0) 
            {
               targetColor = _opacityColor;
               _lifeImage.tintColor = _opacityColor;
            }
            else if (bonus == 1)
            {
                targetColor = _negativeColor;
            }
            else
            {
                targetColor = _positiveColor;
            }

            _lifeLabel.style.color = targetColor;
            _lifeLabel.text = bonus.ToString();
        }

        private void UpdateGameTime(float currentTime)
        {
            _timerLabel.text = Mathf.FloorToInt(currentTime).ToString("");
        }

        public void UpdateShieldLabel(float timeInSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(timeInSeconds);

            _shieldLabel.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);

            Color targetColor;
            if (timeInSeconds <= 0)
            {
                targetColor = _opacityColor;
            }
            else
            {
                if (timeInSeconds > 5)
                {
                    targetColor = _visibleColor;
                }
                else
                {
                    targetColor = _negativeColor;
                }
            }

            _shieldLabel.style.color = targetColor;
            _shildImage.tintColor = timeInSeconds > 0 ? _visibleColor : _opacityColor;
        }

        public void UpdateTimerRerserve(float reserveTimeSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(reserveTimeSeconds);
            _timerCountLabel.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);

            Color targetColor;

            if (reserveTimeSeconds <= 0)
            {
                targetColor = _opacityColor;
            }
            else if (reserveTimeSeconds < 5)
            {
                targetColor = _negativeColor;
            }
            else
            {
                targetColor = _positiveColor;
            }
            _timerCountLabel.style.color = targetColor;
        }

        public void ShowGameOverCase(string gameOverCase)
        {
            _gameOverLabel.style.color = Color.red;
            _gameOverLabel.style.display = DisplayStyle.Flex;
            _gameOverLabel.text = gameOverCase;
            ShowRestartButton();
        }
    }
}