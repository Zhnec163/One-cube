using Scripts.Logic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Scripts.UI
{
    public class Reward : MonoBehaviour
    {
        private const int Id = 1;

        [SerializeField] public int _coefficient;
        [SerializeField] public Score _Score;
        [SerializeField] private Button _ads;
        [SerializeField] private Button _confirmation;
        [SerializeField] private Button _cancellation;
        [SerializeField] private UserInterfaceController _userInterfaceController;

        private void Awake()
        {
            YandexGame.RewardVideoEvent += OnRewarded;
            _ads.onClick.AddListener(OnClickAds);
            _confirmation.onClick.AddListener(OnClickConfirmation);
            _cancellation.onClick.AddListener(OnClickCancellation);
        }

        private void OnDestroy()
        {
            YandexGame.RewardVideoEvent -= OnRewarded;
            _ads.onClick.RemoveListener(OnClickAds);
            _confirmation.onClick.RemoveListener(OnClickConfirmation);
            _cancellation.onClick.RemoveListener(OnClickCancellation);
        }

        private void OnClickAds() =>
            _userInterfaceController.ShowRewardPopup();

        private void OnRewarded(int _) =>
            _Score.AddCoefficient(_coefficient);

        private void OnClickConfirmation()
        {
            _userInterfaceController.CloseRewardPopup();
            YandexGame.RewVideoShow(Id);
        }

        private void OnClickCancellation() =>
            _userInterfaceController.CloseRewardPopup();
    }
}