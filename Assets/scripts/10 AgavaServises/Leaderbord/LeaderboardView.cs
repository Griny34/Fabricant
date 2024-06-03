using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    //[SerializeField] private Transform _container;
    //[SerializeField] private LeaderboardElement _leaderboardElementPrefab;

    [SerializeField] private List<LeaderboardElement> _spawnedElements = new();

    public void ConstructLeaderboard(List<LeaderboardPlayer> leaderboardPlayers)
    {
        //ClearLeaderboard();

        for(int i = 0; i < leaderboardPlayers.Count; i++)
        {
            _spawnedElements[i].Initialize(leaderboardPlayers[i]);
        }

        //foreach(LeaderboardPlayer player in leaderboardPlayers)
        //{
        //    LeaderboardElement leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _container);

        //    leaderboardElementInstance.Initialize(player.Name, player.Rank, player.Rank);

        //    _spawnedElements.Add(leaderboardElementInstance);
        //}
    }

    private void ClearLeaderboard()
    {
        foreach(var element in _spawnedElements)
        {
            Destroy(element);
        }

        _spawnedElements = new List<LeaderboardElement>();
    }
}
