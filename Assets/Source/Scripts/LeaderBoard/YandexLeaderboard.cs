using Scripts.Constant;
using Scripts.Logic;
using UnityEngine;
using YG;
using YG.Utils.LB;

namespace Scripts.LeaderBoard
{
    [RequireComponent(typeof(LeaderboardYG))]
    public class YandexLeaderboard : MonoBehaviour
    {
        [SerializeField] private GameLogic _gameLogic;

        private LeaderboardYG _leaderboardYg;

        private void Awake()
        {
            _leaderboardYg = GetComponent<LeaderboardYG>();
            _gameLogic.LevelEnded += OnLevelEnded;
            YandexGame.onGetLeaderboard += OnGetLeaderboard;
        }

        private void OnDestroy()
        {
            _gameLogic.LevelEnded -= OnLevelEnded;
            YandexGame.onGetLeaderboard -= OnGetLeaderboard;
        }

        private void OnGetLeaderboard(LBData lbData) =>
            PlayerPrefs.SetInt(PlayerPrefNames.BestScore, lbData.thisPlayer.score);

        private void OnLevelEnded(int _) =>
            _leaderboardYg.UpdateLB();
    }
}