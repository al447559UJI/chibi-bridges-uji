using UnityEngine;
using UnityEngine.UIElements;

public class BuildUI : MonoBehaviour
{
    [SerializeField] private ActionStateManager stateManager;

    private VisualElement top;
    private VisualElement bottom;


    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        top = root.Q<VisualElement>("BuildUITop");
        bottom = root.Q<VisualElement>("BuildUIDown");
    }

    void OnEnable()
    {
        stateManager.onStateEntered.AddListener(HandleStateChanged);
    }

    void OnDisable()
    {
        stateManager.onStateEntered.RemoveListener(HandleStateChanged);
    }

    private void HandleStateChanged(ActionStateType type)
    {
        if (type == ActionStateType.BUILD)
            ShowUI();
        else
            HideUI();
    }

    private void ShowUI()
    {
        top.AddToClassList("build-ui-up-active");
        bottom.AddToClassList("build-ui-down-active");
        top.RemoveFromClassList("build-ui-up-inactive");
        bottom.RemoveFromClassList("build-ui-down-inactive");
    }

    private void HideUI()
    {
        top.RemoveFromClassList("build-ui-up-active");
        bottom.RemoveFromClassList("build-ui-down-active");
        top.AddToClassList("build-ui-up-inactive");
        bottom.AddToClassList("build-ui-down-inactive");
    }
}
