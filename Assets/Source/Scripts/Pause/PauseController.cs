using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Pause
{
    public class PauseController : MonoBehaviour
    {
        private List<PauseSource> _pauseSources;

        public void Init(List<PauseSource> pauseSources)
        {
            _pauseSources = pauseSources;
            _pauseSources.ForEach(source => source.ActivityChanged += OnChanged);
        }

        private void OnDestroy() =>
            _pauseSources.ForEach(source => source.ActivityChanged -= OnChanged);

        private void OnChanged()
        {
            if (AnySourceActive())
                Pause();
            else
                Unpause();
        }

        private void Pause()
        {
            AudioListener.pause = true;
            SetTimeScaleToZero();
        }

        private void Unpause()
        {
            AudioListener.pause = false;
            SetTimeScaleToOne();
        }

        private void SetTimeScaleToZero() =>
            Time.timeScale = 0F;

        private void SetTimeScaleToOne() =>
            Time.timeScale = 1F;

        private bool AnySourceActive() =>
            _pauseSources.Any(source => source.IsActive);
    }
}