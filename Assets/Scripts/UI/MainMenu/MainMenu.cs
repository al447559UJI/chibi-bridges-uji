using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{

    private Button startButton;
    private Button optionsButton;
    private Button quitButton;

    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("StartButton");
        optionsButton = root.Q<Button>("OptionsButton");
        quitButton = root.Q<Button>("QuitButton");
    }
    void OnEnable()
    {
        startButton.clicked += OnStartButtonClicked;
        optionsButton.clicked += OnOptionsButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;
    }


    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Level");
    }
    private void OnOptionsButtonClicked()
    {
        Debug.Log("Options pressed.");
    }
    private void OnQuitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
