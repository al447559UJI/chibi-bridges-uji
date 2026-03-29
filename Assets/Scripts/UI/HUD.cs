using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerActions player;
    private Label label;
    private Label plusScrap;

    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        label = root.Q<Label>("ScrapText");
        plusScrap = root.Q<Label>("PlusScrap");
    }

    void Start()
    {
        UpdateScrap(player.currentScrapAmount);
    }

    void OnEnable()
    {
        player.onScrapChanged.AddListener(UpdateScrap);
        player.onScrapGiven.AddListener(() => StartCoroutine(AnimatePlusScrap()));
    }

    void OnDisable()
    {
        player.onScrapChanged.RemoveListener(UpdateScrap);
    }

    void UpdateScrap(int value)
    {
        label.text = $"{value} Scrap remaining";
    }

    private IEnumerator AnimatePlusScrap()
    {
        // TODO: ResetAnimation()
        // Then do the rest.
        plusScrap.RemoveFromClassList("plus-scrap-invisible");
        plusScrap.AddToClassList("plus-scrap-up");
        yield return new WaitForSeconds(0.7f);
        plusScrap.RemoveFromClassList("plus-scrap-up");
        plusScrap.AddToClassList("plus-scrap-invisible");
    }
}
