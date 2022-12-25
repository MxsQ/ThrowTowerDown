using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    [SerializeField] public GameObject brick;
    [SerializeField] public Config conifg;
    [SerializeField] public GameObject bullect;

    public static GameManagers Instance;

    public bool inGame = true;


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

}

[System.Serializable]
public class Config
{
    public float rotateCircleDis = 100;
    public Material defaultBrickMetarial;
}