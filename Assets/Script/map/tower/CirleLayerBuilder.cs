using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;

public class CirleLayerBuilder : TowerLayerBulder
{
    public CirleLayerBuilder(BuildParams _buildParams) : base(_buildParams)
    {
    }

    protected override TowerLayer BuildLayer(Transform parent, int length)
    {
        var girth = length;
        float perAngle = 360f / girth;

        float radius = towerShape.brickWidth / 2 / Mathf.Sin(perAngle / 2 * Mathf.Deg2Rad);


        TowerLayer curBricks = new TowerLayer();
        for (int index = 0; index < girth; index++)
        {
            float nowAngle = index * perAngle;
            float x = radius * Mathf.Cos(nowAngle * Mathf.Deg2Rad);
            float z = radius * Mathf.Sin(nowAngle * Mathf.Deg2Rad);
            GameObject brick = BuildBrickOn(x, z, parent, curBricks, index.ToString());
            curBricks.Add(brick);
        }

        return curBricks;
    }
}
