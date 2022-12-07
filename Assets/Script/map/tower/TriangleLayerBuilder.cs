using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;

public class TriangleLayerBuilder : TowerLayerBulder
{
    public TriangleLayerBuilder(BuildParams _buildParams) : base(_buildParams)
    {
    }

    protected override TowerLayer BuildLayer(Transform parent, int length)
    {
        float unit = 1f;
        float maxOffset = Mathf.Cos(60 * Mathf.Deg2Rad) * length * unit - unit / 2;

        TowerLayer curBricks = new TowerLayer();

        // build bottom
        for (int index = 0; index < length; index++)
        {
            GameObject brick = GetNewBrick();
            float x = -maxOffset + index * unit;
            float z = -maxOffset;
            Vector3 ps = new Vector3(x, 0, z);
            brick.transform.parent = parent;
            brick.transform.localPosition = ps;
            curBricks.Add(brick);
        }

        //// build left
        float perOffserX = Mathf.Cos(60 * Mathf.Deg2Rad) * unit;
        float perOffserZ = Mathf.Sin(60 * Mathf.Deg2Rad) * unit;
        for (int index = 1; index < length; index++)
        {
            GameObject brick = GetNewBrick();
            float x = -maxOffset + index * perOffserX;
            float z = -maxOffset + index * perOffserZ;
            Vector3 ps = new Vector3(x, 0, z);
            brick.transform.parent = parent;
            brick.transform.localPosition = ps;
            curBricks.Add(brick);
        }

        // build right
        float maxO = -maxOffset + perOffserZ * (length - 1);
        for (int index = 0; index < length - 2; index++)
        {
            GameObject brick = GetNewBrick();
            float x = (index + 1) * perOffserX;
            float z = maxO - (index + 1) * perOffserZ;
            Vector3 ps = new Vector3(x, 0, z);
            brick.transform.parent = parent;
            brick.transform.localPosition = ps;
            curBricks.Add(brick);
        }

        return curBricks;
    }
}
