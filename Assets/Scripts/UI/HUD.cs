using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerActions player;

    private VisualElement container;
    private Label label;

    private string plusDefaultClass;
    private string minusDefaultClass;
    private string upAnimationClass;


    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        container = root.Q<VisualElement>("HUDContainer");
        label = container.Q<Label>("ScrapText");
    }

    void Start()
    {
        UpdateScrap(player.currentScrapAmount);
        plusDefaultClass = "plus-scrap-default";
        minusDefaultClass = "minus-scrap-default";
        upAnimationClass = "label-scrap-up";
    }

    void OnEnable()
    {
        player.onScrapChanged.AddListener(UpdateScrap);
        player.onScrapGiven.AddListener(SetUpLabelPlus);
        player.onScrapSpent.AddListener(SetUpLabelMinus);
    }

    void OnDisable()
    {
        player.onScrapChanged.RemoveListener(UpdateScrap);
        player.onScrapGiven.RemoveListener(SetUpLabelPlus);
        player.onScrapSpent.RemoveListener(SetUpLabelMinus);
    }

    void UpdateScrap(int value)
    {
        label.text = $"{value} Scrap remaining";
    }

    private void SetUpLabelPlus(string text)
    {
        StartCoroutine(CreateLabel(text, plusDefaultClass, upAnimationClass));
    }

    private void SetUpLabelMinus(string text)
    {
        StartCoroutine(CreateLabel(text, minusDefaultClass, upAnimationClass));
    }

    public IEnumerator CreateLabel(string text, string defaultClass, string animationClass)
{
    Label label = new Label(text);
    label.AddToClassList(defaultClass);
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
}
