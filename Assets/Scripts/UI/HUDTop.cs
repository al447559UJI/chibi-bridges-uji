using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDTop : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float fps = 10f;
    [SerializeField] private PlayerHealth player;
    private VisualElement container;
    private VisualElement heart;
    private VisualElement battery;
    private Label hp;
    private Label score;
    private int currentFrame;
    private float timer;

    private string currentFontClass;
    private string currentBatteryClass;
    private string currentHeartClass;

    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        container = root.Q<VisualElement>("Container");
        heart = container.Q<VisualElement>("Heart");
        battery = container.Q<VisualElement>("Battery");
        score = container.Q<Label>("Score");
        hp = battery.Q<Label>("HP");
    }

    void Start()
    {
        UpdateFontSize(GameManager.instance.CurrentUIScale);
        UpdateBatterySize(GameManager.instance.CurrentUIScale);
        UpdateHeartSize(GameManager.instance.CurrentUIScale);
        UpdateScore(GameManager.instance.score);
    }

    void Update()
    {
        if (frames == null || frames.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= 1f / fps)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % frames.Length;

            heart.style.backgroundImage = new StyleBackground(frames[currentFrame]);
        }
    }

    private void UpdateHealth(int health)
    {
        hp.text = $"{health}%";
    }

    private void UpdateScore(int newScore)
    {
        score.text = $"Score: {newScore}";
    }

    void OnEnable()
    {
        player.onHealthChanged.AddListener(UpdateHealth);
        StartCoroutine(DelayedEventConnection());
    }

    void OnDisable()
    {
        player.onHealthChanged.RemoveListener(UpdateHealth);
        GameManager.instance?.OnUIScaleChanged.RemoveListener(HandleUIScaleChanged);
        GameManager.instance.OnScoreChanged.RemoveListener(UpdateScore);
    }

    private void HandleUIScaleChanged(UIScale scale)
    {
        UpdateFontSize(scale);
        UpdateBatterySize(scale);
        UpdateHeartSize(scale);
    }

    public void UpdateFontSize(UIScale scale)
    {
        string newClass = CSSClasses.GetByScale(CSSClasses.FontSizes, scale);

        if (!string.IsNullOrEmpty(currentFontClass))
            hp.RemoveFromClassList(currentFontClass);
            score.RemoveFromClassList(currentFontClass);

        hp.AddToClassList(newClass);
        score.AddToClassList(newClass);
        currentFontClass = newClass;
    }

    private void UpdateBatterySize(UIScale scale)
    {
        string newClass = CSSClasses.GetByScale(CSSClasses.BatterySizes, scale);

        if (!string.IsNullOrEmpty(currentBatteryClass))
            battery.RemoveFromClassList(currentBatteryClass);

        battery.AddToClassList(newClass);
        currentBatteryClass = newClass;
    }

    private void UpdateHeartSize(UIScale scale)
    {
        string newClass = CSSClasses.GetByScale(CSSClasses.HeartSizes, scale);

        if (!string.IsNullOrEmpty(currentHeartClass))
            heart.RemoveFromClassList(currentHeartClass);

        heart.AddToClassList(newClass);
        currentHeartClass = newClass;
    }

    /* Since Unity is trash, I have to delay the AddListeners for one frame 
       because the GameManager instance isn't loaded when the OnEnable() function is executed. 

       Use Godot next time. */
    private IEnumerator DelayedEventConnection()
    {
        yield return null;
        GameManager.instance.OnUIScaleChanged.AddListener(HandleUIScaleChanged);
        GameManager.instance.OnScoreChanged.AddListener(UpdateScore);
    }
}
