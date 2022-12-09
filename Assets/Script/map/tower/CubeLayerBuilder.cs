using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;

public class CubeLayerBuilder : TowerLayerBulder
{
    public CubeLayerBuilder(BuildParams _buildParams) : base(_buildParams)
    {
    }

    protected override TowerLayer BuildLayer(Transform parent, int length)
    {
        float unit = 1f;
        float maxLeftOffset = Mathf.Sin(90 * Mathf.Deg2Rad) * length * unit / 2 - unit / 2;
        float topOffset = maxLeftOffset;

        TowerLayer curBricks = new TowerLayer();

        // build Top
        for (int index = 0; index < length; index++)
        {
            float x = -maxLeftOffset + index * unit;
            float z = topOffset;
            GameObject brick = BuildBrickOn(x, z, parent, curBricks);
            curBricks.Add(brick);
        }

        // build Bottom
        for (int index = 0; index < length; index++)
        {
            float x = -maxLeftOffset + index * unit;
            float z = -topOffset;
            GameObject brick = BuildBrickOn(x, z, parent, curBricks);
            curBricks.Add(brick);
        }

        // build left
        for (int index = 0; index < length - 2; index++)
        {
            float x = -maxLeftOffset;
            float z = topOffset - index * unit - unit;
            GameObject brick = BuildBrickOn(x, z, parent, curBricks);
            curBricks.Add(brick);
        }

        // build right
        for (int index = 0; index < length - 2; index++)
        {
            float x = maxLeftOffset;
            float z = topOffset - index * unit - unit;
            GameObject brick = BuildBrickOn(x, z, parent, curBricks);
            curBricks.Add(brick);
        }

        return curBricks;
    }
}
