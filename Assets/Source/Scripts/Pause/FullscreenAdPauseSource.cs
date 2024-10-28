using UnityEngine;
using YG;

namespace Scripts.Pause
{
    public class FullscreenAdPauseSource : PauseSource
    {
        [SerializeField] private YandexGame _yandexGame;

        private void Awake()
        {
            _yandexGame.OpenFullscreenAd.AddListener(OnOpenFullscreenAd);
            _yandexGame.CloseFullscreenAd.AddListener(OnCloseFullscreenAd);
        }

        private void OnDestroy()
        {
            _yandexGame.OpenFullscreenAd.RemoveListener(OnOpenFullscreenAd);
            _yandexGame.CloseFullscreenAd.RemoveListener(OnCloseFullscreenAd);
        }

        private void OnOpenFullscreenAd() =>
            Activate();

        private void OnCloseFullscreenAd() =>
            Deactivate();
    }
}