using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MatchModel : MonoBehaviour
{
    [SerializeField] private InterstitialService _interstishelServise;
    [SerializeField] private MatchModelSO[] _allMatch;
    [SerializeField] private RealizationReward _realizationReward;
    [SerializeField] private YndexLeaderBoardMini _mini;
    [SerializeField] private PauseController _controlerPause;
    [SerializeField] private UserInterface _userInterface;  

    [Header("Timer")]
    [SerializeField] private Timer _gameTimer;

    [Header("Improvement")]
    [SerializeField] private ImprovmentSpawner _improvmentSpawnerChair;
    [SerializeField] private ImprovmentArmchair _improvmentSpawnerArmChair;
    [SerializeField] private ImprovmentChairOnWheel _improvmentSpawnerOnWheel;
    [SerializeField] private ImprovmentSpawner _improvmentSpawnerDouble;
    [SerializeField] private ImprovmentSpawner _improvmentSpawnerTable;
    [SerializeField] private ImprovmentMateriale _improvmentMaterialeLeather;
    [SerializeField] private ImprovmentMateriale _improvmentMaterialeWheel;
    [SerializeField] private ImprovmentWareHouse _improvmentWareHouse;
    [SerializeField] private ImprovmentWareHouse _improvmentWareHouse2;
    [SerializeField] private Upgrade _upgrade;

    [Header("Events")]
    [SerializeField] private UnityEvent onFinishing;
    [SerializeField] private UnityEvent onFinished;
    [SerializeField] public UnityEvent OnMatchChanged;
    [SerializeField] private UnityEvent onWin;

    private int _currentMatchIndex;

    public event UnityAction OnFinishing
    {
        add => onFinishing?.AddListener(value);
        remove => onFinishing?.RemoveListener(value);
    }

    public event UnityAction OnFinished
    {
        add => onFinished?.AddListener(value);
        remove => onFinished?.RemoveListener(value);
    }

    public static MatchModel Instace { get; private set; }

    public MatchModelSO CurrentMatch => _allMatch[_currentMatchIndex];

    private void Awake()
    {
        if (Instace != null)
        {
            Destroy(Instace);
            return;
        }

        Instace = this;
    }

    private void Start()
    {
        Initialize();

        _gameTimer.OnDone += () =>
        {
            FinishMatch();
        };
    }

    public void Initialize()
    {
        _gameTimer.StartTimer(CurrentMatch.Time);
    }

    public void StartNextMatch()
    {
        _interstishelServise.ShowInterstitial(StartNextLevel);
    }

    private void StartNextLevel()
    {
        Wallet.Instance.RestartSalary();
        
        _mini.SetPlayerScor(Wallet.Instance.GetMoney());

        SaveGame();

        if (_currentMatchIndex >= _allMatch.Length)
        {
            SceneManager.LoadScene(1);
            return;
        }

        _realizationReward.OpenSpawner();

        _gameTimer.Stop();
        Initialize();
    }

    public void ExitMenu()
    {
        Wallet.Instance.RestartSalary();
        _mini.SetPlayerScor(Wallet.Instance.GetMoney());
        SaveGame();
        SceneManager.LoadScene(1);
    }

    private void FinishMatch()
    {
        Time.timeScale = 0;
        onFinishing?.Invoke();

        StartCoroutine(Utils.MakeActionDelay(0f, () =>
        {
            onFinished?.Invoke();
        }));
    }
    
    private void SaveGame()
    {
        Wallet.Instance.SaveMoney();

        _improvmentSpawnerChair.SaveValueCounter();
        _improvmentSpawnerArmChair.SaveValueCounter();
        _improvmentSpawnerOnWheel.SaveValueCounter();
        _improvmentSpawnerDouble.SaveValueCounter();
        _improvmentSpawnerTable.SaveValueCounter();
        _improvmentMaterialeLeather.SaveValueCounter();
        _improvmentMaterialeWheel.SaveValueCounter();
        _improvmentWareHouse.SaveValueCounter();
        _improvmentWareHouse2.SaveValueCounter();

        _upgrade.LoadTryes();
    }

    private IEnumerator CloseMenu()
    {
        yield return new WaitForSeconds(1f);

        OnMatchChanged?.Invoke();
    }
}