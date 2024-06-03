using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardPlayer : MonoBehaviour
{
    public LeaderboardPlayer(string name, int score, int rank)
    {
        Name = name;
        Score = score;
        Rank = rank;
    }

    public string Name { get; private set; }
    public int Score { get; private set; }
    public int Rank { get; private set; }
}
