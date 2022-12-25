using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
        int count = 0;

        // build bottom
        for (int index = 0; index < length; index++)
        {
            float x = -maxOffset + index * unit;
            float z = -maxOffset;
            GameObject brick = BuildBrickOn(x, z, parent, curBricks, count.ToString());
            curBricks.Add(brick);
            count++;
        }

        //// build left
        float perOffserX = Mathf.Cos(60 * Mathf.Deg2Rad) * unit;
        float perOffserZ = Mathf.Sin(60 * Mathf.Deg2Rad) * unit;
        for (int index = 1; index < length; index++)
        {
            float x = -maxOffset + index * perOffserX;
            float z = -maxOffset + index * perOffserZ;
            GameObject brick = BuildBrickOn(x, z, parent, curBricks, count.ToString());
            curBricks.Add(brick);
            count++;
        }

        // build right
        float maxO = -maxOffset + perOffserZ * (length - 1);
        for (int index = 0; index < length - 2; index++)
        {
            float x = (index + 1) * perOffserX;
            float z = maxO - (index + 1) * perOffserZ;
            GameObject brick = BuildBrickOn(x, z, parent, curBricks, count.ToString());
            curBricks.Add(brick);
            count++;
        }

        return curBricks;
    }
}
