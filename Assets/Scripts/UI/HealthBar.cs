using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float fps = 10f;
    [SerializeField] private PlayerHealth player;
    private VisualElement container;
    private VisualElement heart;
    private Label label;
    private int currentFrame;
    private float timer;

    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        container = root.Q<VisualElement>("Container");
        heart = container.Q<VisualElement>("Heart");
        VisualElement battery = container.Q<VisualElement>("Battery");
        label = battery.Q<Label>("HP");
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
        label.text = $"{health}%";
    }
    
    void OnEnable()
    {
        player.onHealthChanged.AddListener(UpdateHealth);
    }

    void OnDisable()
    {
        player.onHealthChanged.RemoveListener(UpdateHealth);
    }
}
