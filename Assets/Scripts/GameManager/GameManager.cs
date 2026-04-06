using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameSettingsData settings;

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


}
