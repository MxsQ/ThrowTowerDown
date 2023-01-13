using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    Dictionary<Panel, BasePanel> panels = new Dictionary<Panel, BasePanel>();

    private void Awake()
    {
        Instance = this;
    }

    public void Register(BasePanel panel, Panel name)
    {
        if (!panels.ContainsKey(name))
        {
            panels.Add(name, panel);
        }
    }

    public void OnGameStartClick()
    {
        GameManagers.Instance.InvokeGameStart();
        panels[Panel.Main].Hide();
        panels[Panel.Player].Show();
    }
}

public enum Panel
{
    Main,
    Player,
    Skin,
}
