using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildUI : MonoBehaviour
{
    [SerializeField] private ActionStateManager stateManager;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    public float zoomInLevel { get; private set; } = 6;
    public float zoomOutLevel { get; private set; } = 10;

    private bool isZoomedIn = true;

    private VisualElement top;
    private VisualElement bottom;

    private string topEnabledClass;
    private string bottomEnabledClass;

    private string topDisabledClass;
    private string bottomDisabledClass;


    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        top = root.Q<VisualElement>("BuildUITop");
        bottom = root.Q<VisualElement>("BuildUIDown");
    }

    void Start()
    {
        topEnabledClass = CSSClasses.BuildUI[CSSClasses.BuildUIKeys.TOP_ENABLED];
        bottomEnabledClass = CSSClasses.BuildUI[CSSClasses.BuildUIKeys.BOTTOM_ENABLED];
        topDisabledClass = CSSClasses.BuildUI[CSSClasses.BuildUIKeys.TOP_DISABLED];
        bottomDisabledClass = CSSClasses.BuildUI[CSSClasses.BuildUIKeys.BOTTOM_DISABLED];
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
        {
            ShowUI();
            ZoomOut();
                            isZoomedIn = false;
        }
        else
        {
            HideUI();
            if (!isZoomedIn)
            {
                isZoomedIn = true;
                ZoomIn();
            }
        }
    }

    private void ShowUI()
    {
        top.AddToClassList(topEnabledClass);
        bottom.AddToClassList(bottomEnabledClass);

        top.RemoveFromClassList(topDisabledClass);
        bottom.RemoveFromClassList(bottomDisabledClass);
    }

    private void HideUI()
    {
        top.RemoveFromClassList(topEnabledClass);
        bottom.RemoveFromClassList(bottomEnabledClass);

        top.AddToClassList(topDisabledClass);
        bottom.AddToClassList(bottomDisabledClass);
    }

    private void ZoomIn()
    {
        cinemachineCamera.Lens.OrthographicSize = zoomInLevel;
    }

    private void ZoomOut()
    {
        cinemachineCamera.Lens.OrthographicSize = zoomOutLevel;
    }
}
