using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Tower;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    MapGeneretor mapGeneretor;
    int levelIndex = 0;

    List<TowerLayer> towerLayers = new List<TowerLayer>();
    Spawn spawn;

    private void Awake()
    {
        Instance = this;
        mapGeneretor = FindObjectOfType<MapGeneretor>();
        levelIndex = mapGeneretor.mapIndex;
        spawn = FindObjectOfType<Spawn>();
    }

    public void LoadNextLevel()
    {
        towerLayers = mapGeneretor.BuildTower();
        Level level = mapGeneretor.levels[levelIndex];
        Debug.Log(towerLayers.Count + "  " + level.ShowLayerNum);
        for (int index = 0; index < towerLayers.Count - level.ShowLayerNum; index++)
        {
            towerLayers[index].Pin();
        }
        for (int index = towerLayers.Count - level.ShowLayerNum; index < towerLayers.Count; index++)
        {
            towerLayers[index].Loosen();
        }

        spawn.Reset();
        StartCoroutine("CheckTower");
    }

    public BrickColor GetValidBrickColor()
    {
        var allColorBind = MapGeneretor.Instance.colorBind;
        int maxSize = allColorBind.Length - 1;
        HashSet<BrickColor> validColor = new HashSet<BrickColor>();

        foreach (TowerLayer curLayer in towerLayers)
        {
            if (validColor.Count == maxSize)
            {
                break;
            }

            if (curLayer.frezz || curLayer.isCollapse())
            {
                continue;
            }

            foreach (var birck in curLayer.bricks)
            {
                validColor.Add(birck.color);
            }
        }
        BrickColor[] colors = validColor.ToArray();
        // Debug.Log("collaspe=" + towerLayers[towerLayers.Count - 1].isCollapse() + " freez=" + towerLayers[towerLayers.Count - 1].frezz);
        //foreach (Brick b in towerLayers[towerLayers.Count - 1].bricks)
        //{
        //    b.printD();
        //}

        if (validColor.Count == 0)
        {
            return allColorBind[0].color;
        }

        return colors[Random.Range(0, colors.Length)];
    }

    IEnumerator CheckTower()
    {
        yield return new WaitForSeconds(0.5f);
        while (GameManagers.Instance.inGame)
        {
            foreach (TowerLayer towerLayer in towerLayers)
            {
                towerLayer.UpdateBrickState();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
