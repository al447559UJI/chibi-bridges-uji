using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerActions player;
    private Label label;

    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        label = root.Q<Label>("ScrapText");
    }

    void Start()
    {
        UpdateScrap(player.currentScrapAmount);
    }

    void OnEnable()
    {
        player.onScrapChanged.AddListener(UpdateScrap);
    }

    void OnDisable()
    {
        player.onScrapChanged.RemoveListener(UpdateScrap);
    }

    void UpdateScrap(int value)
    {
        label.text = $"{value} Scrap remaining";
    }
}
