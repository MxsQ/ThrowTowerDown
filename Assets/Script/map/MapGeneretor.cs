using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;
using static TowerLayerBulder;

public class MapGeneretor : MonoBehaviour
{
    [SerializeField] public int mapIndex;
    [SerializeField] Transform host;
    [SerializeField] GameObject bricks;
    [SerializeField] public ColorBind[] colorBind;
    [SerializeField] public Level[] levels;

    List<GameObject> allBrick = new List<GameObject>();
    List<TowerLayer> towerLayers = new List<TowerLayer>();

    public static MapGeneretor Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<TowerLayer> BuildTower()
    {
        /*   foreach (GameObject obj in allBrick)
           {
               DestroyImmediate(obj);
           }*/
        towerLayers.Clear();

        for (int index = host.childCount - 1; index >= 0; index--)
        {
            DestroyImmediate(host.GetChild(index).gameObject);
        }
        allBrick.Clear();

        var shapes = levels[mapIndex].towerLayer;
        float startMark = 0;
        float startY = 0;
        float brickHight = 1;
        float startMass = 0;
        float increaseMass = 2;

        //float totalNum = 0;

        for (int i = 0; i < shapes.Length; i++)
        {
            startMass += increaseMass * shapes[i].hight;
            //totalNum += shapes[i].hight;
        }

        for (int i = 0; i < shapes.Length; i++)
        {
            //startMass = Mathf.Pow(2, totalNum);
            BuildParams buildParams = new BuildParams(shapes[i], startY, brickHight, startMass, increaseMass, 1, host, bricks, colorBind, levels[mapIndex].seek);
            TowerLayerBulder bulder = GetBuilder(shapes[i].shape, buildParams);
            List<TowerLayer> tmpTowerLayers = bulder.Build();
            towerLayers.AddRange(tmpTowerLayers);
            startY += towerLayers.Count * brickHight;
            startMark += towerLayers.Count;
            startMass -= increaseMass * shapes[i].hight;
            //totalNum -= shapes[i].hight;
        }

        return new List<TowerLayer>(towerLayers);
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
    public int seek;
    public int ShowLayerNum = 8;
    public TowerShaper[] towerLayer;
}

[System.Serializable]
public class ColorBind
{
    public BrickColor color;
    public Material material;
}
