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
        yield return new WaitForSeconds(settings.deathTimer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HandlePlayerDeath()
    {
        StartCoroutine(RestartLevel());
    }
}