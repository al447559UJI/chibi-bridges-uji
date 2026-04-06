using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerActions player;

    private VisualElement container;
    private VisualElement scrapIcon;
    private string fontSize;
    private string currentFontClass;
    private string currentIconClass;
    private Label scrapText;

    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        container = root.Q<VisualElement>("HUDContainer");
        scrapIcon = container.Q<VisualElement>("ScrapIcon");
        scrapText = container.Q<Label>("ScrapText");
    }

    void Start()
    {
        UpdateScrap(player.currentScrapAmount);
        UpdateFontSize(GameManager.instance.CurrentUIScale);
        UpdateScrapIconSize(GameManager.instance.CurrentUIScale);
    }

    void OnEnable()
    {
        player.onScrapChanged.AddListener(UpdateScrap);
        player.onScrapGiven.AddListener(SetUpLabelPlus);
        player.onScrapSpent.AddListener(SetUpLabelMinus);

        GameManager.instance?.OnUIScaleChanged.AddListener(HandleUIScaleChanged);
    }

    void OnDisable()
    {
        player.onScrapChanged.RemoveListener(UpdateScrap);
        player.onScrapGiven.RemoveListener(SetUpLabelPlus);
        player.onScrapSpent.RemoveListener(SetUpLabelMinus);
    }

    void UpdateScrap(int value)
    {
        scrapText.text = $"{value} Scrap remaining";
    }

    private void SetUpLabelPlus(string text)
    {
        string plusDefault = CSSClasses.ScrapUI[CSSClasses.ScrapUIKeys.PLUS_DEFAULT];
        string upAnimation = CSSClasses.ScrapUI[CSSClasses.ScrapUIKeys.UP_ANIMATION];
        StartCoroutine(CreateLabel(text, plusDefault, upAnimation));
    }

    private void SetUpLabelMinus(string text)
    {
        string minusDefault = CSSClasses.ScrapUI[CSSClasses.ScrapUIKeys.MINUS_DEFAULT];
        string upAnimation = CSSClasses.ScrapUI[CSSClasses.ScrapUIKeys.UP_ANIMATION];
        StartCoroutine(CreateLabel(text, minusDefault, upAnimation));
    }

    public IEnumerator CreateLabel(string text, string defaultClass, string animationClass)
    {
        Label label = new Label(text);
        label.AddToClassList(defaultClass);
        label.AddToClassList(fontSize);
        container.Add(label);

        // Wait for next frame.
        yield return null;

        // At the end of the transition, destroy the label.
        label.RegisterCallback<TransitionEndEvent>(_ =>
        {
            label.RemoveFromHierarchy();
        });

        label.AddToClassList(animationClass);
    }

    private void HandleUIScaleChanged(UIScale scale)
    {
        UpdateFontSize(scale);
        UpdateScrapIconSize(scale);
    }

    public void UpdateFontSize(UIScale scale)
    {
        string newClass = fontSize = CSSClasses.GetByScale(CSSClasses.FontSizes, scale);

        if (!string.IsNullOrEmpty(currentFontClass))
            scrapText.RemoveFromClassList(currentFontClass);

        scrapText.AddToClassList(newClass);
        currentFontClass = newClass;
    }

    private void UpdateScrapIconSize(UIScale scale)
    {
        string newClass = CSSClasses.GetByScale(CSSClasses.ScrapIcon, scale);

        if (!string.IsNullOrEmpty(currentIconClass))
            scrapIcon.RemoveFromClassList(currentIconClass);

        scrapIcon.AddToClassList(newClass);
        // Debug.Log(newClass);
        currentIconClass = newClass;
    }
}
