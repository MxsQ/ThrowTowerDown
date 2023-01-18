using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayInFoPanel : BasePanel
{
    [SerializeField] RectTransform processBar;
    [SerializeField] Text processText;
    [SerializeField] float maxBarLength = 400;

    private void Start()
    {
        base.Start();
        GameManagers.OnLevelProgressChange += UpdateProcess;
    }

    public void UpdateProcess(float percent)
    {
        processBar.sizeDelta = new Vector2(percent * maxBarLength, processBar.sizeDelta.y);
        processText.text = (percent * 100).ToString("0.0") + "%";
    }

    public override Panel Name()
    {
        return Panel.PlayInfo;
    }
}
