using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    [SerializeField] public GameObject brick;

    public static GameManagers Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LevelManager.Instance.LoadNextLevel();
    }
}
