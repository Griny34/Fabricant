using TMPro;
using UnityEngine;

namespace AgavaServices
{
    public class LeaderboardElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playerName;
        [SerializeField] private TMP_Text _playerRank;
        [SerializeField] private TMP_Text _playerScore;

        public void Initialize(LeaderboardPlayer leaderboardPlayer)
        {
            _playerName.text = leaderboardPlayer.Name;
            _playerRank.text = leaderboardPlayer.Rank.ToString();
            _playerScore.text = leaderboardPlayer.Score.ToString();
        }
    }
}