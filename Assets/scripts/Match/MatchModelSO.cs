using UnityEngine;

[CreateAssetMenu(fileName = "Match 1", menuName = "ScriptableObjects/Match", order = 0)]
public class MatchModelSO : ScriptableObject
{
    [SerializeField] private int _money;
    [SerializeField] private int _time;
    [SerializeField] private int _numberLevel;

    public int Money => _money;

    public int Time => _time;

    public int NumberLevel => _numberLevel;
}
