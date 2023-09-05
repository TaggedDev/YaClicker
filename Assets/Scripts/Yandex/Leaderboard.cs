using Economy;
using UnityEngine;
using YG;

namespace Yandex
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private PlayerResource resource;
        private float _timer = 2;
        
        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0)
                return;

            WriteDataToLeaderboard();
            _timer = 2;
        }

        private void WriteDataToLeaderboard()
        {
            YandexGame.NewLeaderboardScores("coins", (int)resource.ResourceBank);
        }
    }
}