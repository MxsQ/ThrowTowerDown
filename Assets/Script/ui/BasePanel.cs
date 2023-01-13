using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    public void Start()
    {
        UIManager.Instance.Register(this, Name());
        //OnStart();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public abstract Panel Name();

    // protected virtual void OnStart() { }
}
