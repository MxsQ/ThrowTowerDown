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

    int showingNum = 0;
    int curTopLayerIndex = 0;
    int curShowBottomIndex = 0;
    int towerHight = 0;

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
        showingNum = level.ShowLayerNum;
        towerHight = towerLayers.Count;
        curTopLayerIndex = towerHight - 1;
        curShowBottomIndex = curTopLayerIndex - showingNum + 1;
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

    public Level GetLevel()
    {
        return mapGeneretor.levels[levelIndex];
    }

    public int GetTowerHight()
    {
        return towerHight;
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

    // Check form top untill a layer is valid or to the bottom.
    IEnumerator CheckTower()
    {
        yield return new WaitForSeconds(0.5f);
        while (GameManagers.Instance.inGame)
        {
            int changeCount = 0;
            int curIndex = curTopLayerIndex;
            bool hasChange = false;

            while (curIndex >= 0)
            {
                TowerLayer towerLayer = towerLayers[curIndex];
                towerLayer.UpdateBrickState();
                if (towerLayer.isCollapse())
                {
                    changeCount++;
                    curIndex--;
                    hasChange = true;
                }
                else
                {
                    break;
                }
            }

            if (hasChange)
            {
                Debug.Log($"before change: top={curTopLayerIndex} bottom={curShowBottomIndex}");
                int losenTop = curShowBottomIndex - 1;
                curShowBottomIndex -= changeCount;
                curShowBottomIndex = curShowBottomIndex >= 0 ? curShowBottomIndex : 0;
                int losenCount = curTopLayerIndex - curShowBottomIndex - showingNum + 1;
                curTopLayerIndex -= changeCount;
                Debug.Log($"do change: loosentop={losenTop} losenCount={losenCount}");
                LosenNewLayer(losenCount, losenTop);
                Debug.Log(changeCount + " layer be destory.");
                NotifyValidTowerLayerChange();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void LosenNewLayer(int count, int topIndex)
    {
        if (count <= 0)
        {
            return;
        }

        for (int index = topIndex; count > 0; count--, index--)
        {
            towerLayers[index].Loosen();
        }
    }

    void NotifyValidTowerLayerChange()
    {
        GameManagers.Instance.OnValidTowerLayerChange(curTopLayerIndex);
    }
}
