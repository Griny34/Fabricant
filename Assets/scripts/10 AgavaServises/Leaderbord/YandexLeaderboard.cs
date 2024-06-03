using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YandexLeaderboard : MonoBehaviour
{
    private const string LeaderboardName = "Leaderboard";
    private const string AnonymousName = "Anonymous";

    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new ();

    [SerializeField] private LeaderboardView _leaderboardView;
    [SerializeField] private LeaderboardView _leaderboardView2;

    public void SetPlayerScor(int scor)
    {
        if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
        {
            return;
        }


        if (Agava.WebUtility.AdBlock.Enabled == true)
        {
            return;
        }


        if (PlayerAccount.IsAuthorized == false)
        {
            return;
        }

        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if(result == null || result.score < scor)
                Leaderboard.SetScore(LeaderboardName, scor);
        });
    }

    public void Fill()
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        _leaderboardPlayers.Clear();

        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            foreach(var entry in result.entries)
            {
                int rank = entry.rank;
                int score = entry.score;
                string name = entry.player.publicName;

                if(string.IsNullOrEmpty(name))
                    name = AnonymousName;

                _leaderboardPlayers.Add(new LeaderboardPlayer(name, score, rank));
            }

            _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
            _leaderboardView2.ConstructLeaderboard(_leaderboardPlayers);
        });
    }
}
