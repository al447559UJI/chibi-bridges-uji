using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DebugMenu : MonoBehaviour
{
    private InputAction debugAction;
    private bool visible;

    private VisualElement container;

    private class DebugEntry
    {
        public Func<string> valueGetter;
        public Label label;
    }

    private readonly List<DebugEntry> entries = new();

    void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        container = root.Q<ScrollView>("debug-container");

        // OFF by default
        root.style.display = DisplayStyle.None;
        visible = false;
    }

    void Start()
    {
        debugAction = InputSystem.actions.FindAction("DebugMenu");

        foreach (var field in DebugRegistry.Fields)
        {
            AddField(field.Item1, field.Item2);
        }
    }

    public void AddField(string name, Func<object> getter)
    {
        Label label = new Label();
        container.Add(label);

        entries.Add(new DebugEntry
        {
            label = label,
            // Create a function that returns "name: current value" for this debug entry
            valueGetter = () => $"{name}: {getter()}"
        });
    }

    void Update()
    {
        if (debugAction.WasPressedThisFrame())
        {
            visible = !visible;
            GetComponent<UIDocument>().rootVisualElement.style.display =
                visible ? DisplayStyle.Flex : DisplayStyle.None;
        }

        if (visible)
        {
            foreach (var entry in entries)
            {
                entry.label.text = entry.valueGetter();
            }
        }
    }
}
