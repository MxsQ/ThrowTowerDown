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
            Vector3 ps = new Vector3(x, 0, z);
            GameObject _brick = GameObject.Instantiate(buildParams.bricks[0]);
            _brick.transform.parent = parent;
            _brick.transform.localPosition = ps;
            curBricks.Add(_brick);
        }

        return curBricks;
    }
}