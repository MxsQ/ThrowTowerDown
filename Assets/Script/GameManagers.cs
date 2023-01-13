using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    [SerializeField] public GameObject brick;
    [SerializeField] public Config conifg;
    [SerializeField] public GameObject bullect;

    public static GameManagers Instance;

    public static event Action OnGameStart;

    public bool inGame = true;

    float cameraYOffestToTower = 4;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LevelManager.Instance.LoadNextLevel();
    }


    public Material GetByColor(BrickColor color)
    {
        foreach (ColorBind bind in MapGeneretor.Instance.colorBind)
        {
            if (bind.color == color)
            {
                return bind.material;
            }
        }
        return null;
    }


    public void OnValidTowerLayerChange(int topIndex)
    {
        CameraMananger.Instance.DownTo(topIndex - cameraYOffestToTower);
    }

    public void InvokeGameStart()
    {
        float hight = LevelManager.Instance.GetTowerHight() - cameraYOffestToTower;
        CameraMananger.Instance.UpTo(hight);
        OnGameStart?.Invoke();
    }
}

[System.Serializable]
public class Config
{
    public float rotateCircleDis = 100;
    public Material defaultBrickMetarial;

    [Header("Camera")]
    public float CameraChangeTimeOnStart = 2f;
    public float CameraChangeRotateSpeedOnStart = 180f;
    public float CameraChangeTimeOnDown = 0.5f;
}