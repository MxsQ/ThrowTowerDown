using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;
using static TowerLayerBulder;

public class MapGeneretor : MonoBehaviour
{
    [SerializeField] int mapIndex;
    [SerializeField] Transform host;
    [SerializeField] GameObject[] bricks;
    [SerializeField] Level[] levels;

    List<GameObject> allBrick = new List<GameObject>();

    public void BuildTower()
    {
        /*   foreach (GameObject obj in allBrick)
           {
               DestroyImmediate(obj);
           }*/

        for (int index = host.childCount - 1; index >= 0; index--)
        {
            DestroyImmediate(host.GetChild(index).gameObject);
        }
        allBrick.Clear();

        var shapes = levels[mapIndex].towerLayer;
        float startMark = 0;
        float startY = 0;
        float brickHight = 1;

        for (int i = 0; i < shapes.Length; i++)
        {
            BuildParams buildParams = new BuildParams(shapes[i], startY, 1, brickHight, host, bricks);
            TowerLayerBulder bulder = GetBuilder(shapes[i].shape, buildParams);
            List<TowerLayer> towerLayers = bulder.Build();
            startY += towerLayers.Count * brickHight;
            startMark += towerLayers.Count;
        }
    }

    private TowerLayerBulder GetBuilder(Shape shape, BuildParams param)
    {
        switch (shape)
        {
            case Shape.Circle:
                return new CirleLayerBuilder(param);
                break;

            case Shape.Square:
                return new CubeLayerBuilder(param);
                break;

            case Shape.Triangle:
                return new TriangleLayerBuilder(param);
                break;

            default:
                return new CirleLayerBuilder(param);
        }
    }

}

[System.Serializable]
public class Level
{
    public TowerShaper[] towerLayer;
}
