using System.Collections.Generic;
using UnityEngine;

namespace AgavaServices
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private List<LeaderboardElement> _spawnedElements = new();

        public void ConstructLeaderboard(List<LeaderboardPlayer> leaderboardPlayers)
        {
            ClearLeaderboard();

            for (int i = 0; i < leaderboardPlayers.Count; i++)
            {
                _spawnedElements[i].Initialize(leaderboardPlayers[i]);
            }
        }

        private void ClearLeaderboard()
        {
            foreach (var element in _spawnedElements)
            {
                Destroy(element);
            }

            _spawnedElements = new List<LeaderboardElement>();
        }
    }
}