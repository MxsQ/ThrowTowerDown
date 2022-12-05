using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;
using static TowerLayerBulder;

public class MapGeneretor : MonoBehaviour
{
    [SerializeField] Transform host;
    [SerializeField] GameObject[] bricks;
    [SerializeField] TowerShaper towerLayer;

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


        BuildParams buildParams = new BuildParams(towerLayer, 0, 1, 0, host, bricks);
        TowerLayerBulder bulder = new CirleLayerBuilder(buildParams);
        bulder.Build();
    }


}
