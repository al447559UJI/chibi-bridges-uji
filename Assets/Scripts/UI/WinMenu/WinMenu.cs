using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class WinMenu : MonoBehaviour
{

    private Button mainMenuButton;

    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement screenContainer = root.Q<VisualElement>("ScreenContainer");
        VisualElement middleContainer = screenContainer.Q<VisualElement>("ScreenContainer");
        mainMenuButton = middleContainer.Q<Button>("MainMenuButton");
    }

    void OnEnable()
    {
        mainMenuButton.clicked += OnMainMenuButtonClicked;
    }

    private void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
