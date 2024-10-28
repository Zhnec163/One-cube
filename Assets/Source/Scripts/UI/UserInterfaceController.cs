using Scripts.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Scripts.UI
{
    public class UserInterfaceController : MonoBehaviour
    {
        [SerializeField] private GameObject _settingsMenu;
        [SerializeField] private GameObject _winnerMenu;
        [SerializeField] private GameObject _rewardPopup;
        [SerializeField] private GameObject _endGamePanel;
        [SerializeField] private GameObject _leaderboardView;
        [SerializeField] private GameObject _authorizationPopup;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _adsButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _adConfirmationButton;
        [SerializeField] private Button _adDeniedButton;
        [SerializeField] private Button _authorizationConfirmationButton;
        [SerializeField] private Button _authorizationDeniedButton;
        [SerializeField] private TMP_Text _scoreText;

        private bool _isLeaderboardActive;
        private GameLogic _gameLogic;

        public void Init(GameLogic gameLogic)
        {
            _gameLogic = gameLogic;
            _gameLogic.LevelEnded += OnLevelEnded;
            _gameLogic.GameEnded += OnGameEnded;
            _adsButton.onClick.AddListener(OnClickAdsButton);
            _adConfirmationButton.onClick.AddListener(OnClickConfirmationButton);
            _adDeniedButton.onClick.AddListener(OnClickDeniedButton);
            _settingsButton.onClick.AddListener(OnClickSettingsButton);
            _resumeButton.onClick.AddListener(OnClickResumeButton);
            _nextLevelButton.onClick.AddListener(OnClickNextLevelButton);
            _leaderboardButton.onClick.AddListener(OnClickLeaderboardButton);
            _authorizationConfirmationButton.onClick.AddListener(OnClickAuthorizationConfirmationButton);
            _authorizationDeniedButton.onClick.AddListener(OnClickAuthorizationDeniedButton);
        }

        private void OnDestroy()
        {
            _gameLogic.LevelEnded -= OnLevelEnded;
            _gameLogic.GameEnded -= OnGameEnded;
            _adsButton.onClick.RemoveListener(OnClickAdsButton);
            _adConfirmationButton.onClick.RemoveListener(OnClickConfirmationButton);
            _settingsButton.onClick.RemoveListener(OnClickSettingsButton);
            _resumeButton.onClick.RemoveListener(OnClickResumeButton);
            _nextLevelButton.onClick.RemoveListener(OnClickNextLevelButton);
            _leaderboardButton.onClick.RemoveListener(OnClickLeaderboardButton);
            _authorizationConfirmationButton.onClick.RemoveListener(OnClickAuthorizationConfirmationButton);
            _authorizationDeniedButton.onClick.RemoveListener(OnClickAuthorizationDeniedButton);
        }

        public void ShowRewardPopup()
        {
            _rewardPopup.gameObject.SetActive(true);
            CloseSettingsButton();
        }

        public void CloseRewardPopup()
        {
            _rewardPopup.gameObject.SetActive(false);
            ShowSettingsButton();
        }

        private void OnClickConfirmationButton() =>
            ShowInterface();

        private void OnClickDeniedButton() =>
            ShowInterface();

        private void ShowInterface()
        {
            ShowLeaderboardButton();
            ShowSettingsButton();
            ShowAdsButton();
        }

        private void OnClickAdsButton()
        {
            CloseSettingsButton();
            CloseLeaderboardButton();
            CloseAdsButton();
        }

        private void OnClickNextLevelButton() =>
            CloseWinnerMenu();

        private void OnGameEnded() =>
            _endGamePanel.SetActive(true);

        private void OnLevelEnded(int score)
        {
            _scoreText.text = score.ToString();
            ShowWinnerMenu();
        }

        private void OnClickLeaderboardButton()
        {
            if (_isLeaderboardActive)
            {
                _isLeaderboardActive = false;
                CloseLeaderboardView();
                ShowSettingsButton();
                ShowAdsButton();
            }
            else
            {
                if (YandexGame.auth)
                {
                    _isLeaderboardActive = true;
                    ShowLeaderboardView();
                    CloseSettingsButton();
                    CloseAdsButton();
                }
                else
                {
                    ShowAuthorizationPopup();
                }
            }
        }

        private void OnClickAuthorizationConfirmationButton()
        {
            YandexGame.AuthDialog();
            CloseAuthorizationPopup();
        }

        private void OnClickAuthorizationDeniedButton() =>
            CloseAuthorizationPopup();

        private void ShowAuthorizationPopup() =>
            _authorizationPopup.SetActive(true);

        private void CloseAuthorizationPopup() =>
            _authorizationPopup.SetActive(false);

        private void ShowLeaderboardView() =>
            _leaderboardView.SetActive(true);

        private void CloseLeaderboardView() =>
            _leaderboardView.SetActive(false);

        private void ShowWinnerMenu() =>
            _winnerMenu.SetActive(true);

        private void CloseWinnerMenu() =>
            _winnerMenu.SetActive(false);

        private void OnClickSettingsButton() =>
            ShowSettingsMenu();

        private void OnClickResumeButton() =>
            CloseSettingsMenu();

        private void ShowSettingsButton() =>
            _settingsButton.gameObject.SetActive(true);

        private void CloseSettingsButton() =>
            _settingsButton.gameObject.SetActive(false);

        private void ShowSettingsMenu() =>
            _settingsMenu.SetActive(true);

        private void CloseSettingsMenu() =>
            _settingsMenu.SetActive(false);

        private void ShowAdsButton() =>
            _adsButton.gameObject.SetActive(true);

        private void CloseAdsButton() =>
            _adsButton.gameObject.SetActive(false);

        private void ShowLeaderboardButton() =>
            _leaderboardButton.gameObject.SetActive(true);

        private void CloseLeaderboardButton() =>
            _leaderboardButton.gameObject.SetActive(false);
    }
}