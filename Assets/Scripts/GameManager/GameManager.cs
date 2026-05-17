using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameSettingsData settings;
    [SerializeField] private PlayerController player;

    public UIScale CurrentUIScale => settings.uiScale;

    public UnityEvent<UIScale> OnUIScaleChanged;
    public UnityEvent<int> OnScoreChanged;

    public int score {get; private set;} = 0; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        ApplySettings();
        player.health.onDeath.AddListener(HandlePlayerDeath);
    }
    public void SetUIScale(UIScale newScale)
    {
        settings.uiScale = newScale;
        OnUIScaleChanged?.Invoke(newScale);
    }
    private void ApplySettings()
    {
        Time.timeScale = settings.debugTimeScale;
    }

    private IEnumerator RestartLevel()
    {
        DebugRegistry.Clear();
        yield return new WaitForSeconds(settings.deathTimer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HandlePlayerDeath()
    {
        StartCoroutine(RestartLevel());
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        OnScoreChanged.Invoke(score);
    }
}