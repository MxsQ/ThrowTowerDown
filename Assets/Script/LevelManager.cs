using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    MapGeneretor mapGeneretor;

    private void Awake()
    {
        Instance = this;
        mapGeneretor = FindObjectOfType<MapGeneretor>();
    }

    public void LoadNextLevel()
    {
        var brickLayers = mapGeneretor.BuildTower();

    }
}
