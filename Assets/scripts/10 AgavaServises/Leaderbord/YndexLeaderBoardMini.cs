using Agava.YandexGames;
using UnityEngine;

public class YndexLeaderBoardMini : MonoBehaviour
{
    private const string LeaderboardName = "Leaderboard";

#if !UNITY_EDITOR && UNITY_WEBGL
    

#endif
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
