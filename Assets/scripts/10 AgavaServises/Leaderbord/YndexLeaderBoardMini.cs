using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YndexLeaderBoardMini : MonoBehaviour
{
    private const string LeaderboardName = "Leaderboard";

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
            if (result == null || result.score < scor)
                Leaderboard.SetScore(LeaderboardName, scor);
        });
    }
}
