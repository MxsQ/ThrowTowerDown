using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Tower;
using static TowerLayerBulder;

public abstract class TowerLayerBulder
{
    protected TowerShaper towerShape;
    protected BuildParams buildParams;
    protected List<TowerLayer> layers;

    public TowerLayerBulder(BuildParams _buildParams)
    {
        buildParams = _buildParams;
        towerShape = _buildParams.towerLayer;
    }

    public List<TowerLayer> Build()
    {
        layers = new List<TowerLayer>();


        for (int i = 0; i < towerShape.hight; i++)
        {
            // position center
            Vector3 centerPs = Vector3.zero;
            float centerRoteAnle = towerShape.centerIncreaseAngle * i;
            centerPs.x = towerShape.centerZOffset * Mathf.Sin(centerRoteAnle * Mathf.Deg2Rad);
            centerPs.z = towerShape.centerZOffset * Mathf.Cos(centerRoteAnle * Mathf.Deg2Rad);
            centerPs.y = buildParams.startY + buildParams.brickHight * i;

            GameObject curLayerObj = new GameObject();
            curLayerObj.name = "layer" + (buildParams.startMark + i);
            curLayerObj.transform.parent = buildParams.host;
            curLayerObj.transform.position = centerPs;

            // position layer face
            float roteAngle = towerShape.startRotateAngle + towerShape.increseAngle * i;
            Quaternion qua = Quaternion.Euler(Vector3.up * (roteAngle % 360));
            curLayerObj.transform.rotation = qua;

            int length = towerShape.length + towerShape.increateLength * i;
            var layerRecord = BuildLayer(curLayerObj.transform, length);

            layers.Add(layerRecord);
        }
        return layers;
    }

    protected abstract TowerLayer BuildLayer(Transform parent, int length);

    public class BuildParams
    {
        public GameObject[] bricks;
        public TowerShaper towerLayer;
        public float startY;
        public float brickHight;
        public Transform host;
        public float startMark;

        public BuildParams(TowerShaper _towerLayer, float _startY, float _brickHight, float _startMark, Transform _host, GameObject[] _bricks)
        {
            towerLayer = _towerLayer;
            startY = _startY;
            brickHight = _brickHight;
            startMark = _startMark;
            host = _host;
            bricks = _bricks;
        }
    }
}
