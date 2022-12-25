using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;
using static TowerLayerBulder;

public abstract class TowerLayerBulder
{
    protected TowerShaper towerShape;
    protected BuildParams buildParams;
    protected List<TowerLayer> layers;
    private int curLayerNum = 0;

    public TowerLayerBulder(BuildParams _buildParams)
    {
        buildParams = _buildParams;
        towerShape = _buildParams.towerLayer;
        Random.InitState(buildParams.seek);
    }

    public List<TowerLayer> Build()
    {
        layers = new List<TowerLayer>();


        for (int i = 0; i < towerShape.hight; i++)
        {
            curLayerNum = i;
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

    protected GameObject GetNewBrick(TowerLayer host)
    {
        int index = Random.Range(1, buildParams.colorBind.Length);
        Material material = Object.Instantiate(buildParams.colorBind[index].material);
        var brick = GameObject.Instantiate(buildParams.brick);
        brick.GetComponent<MeshRenderer>().material = material;
        brick.GetComponent<Brick>().Host = host;
        brick.GetComponent<Brick>().color = buildParams.colorBind[index].color;
        return brick;
        //return GameObject.Instantiate(buildParams.bricks[0]);
    }

    protected GameObject BuildBrickOn(float x, float z, Transform parent, TowerLayer host, string itemMark)
    {
        GameObject brick = GetNewBrick(host);
        Vector3 ps = new Vector3(x, 0, z);
        brick.transform.parent = parent;
        brick.transform.localPosition = ps;
        float mass = buildParams.startMass - curLayerNum * buildParams.increaseMass;
        //float mass = buildParams.startMass / Mathf.Pow(buildParams.increaseMass, curLayerNum);
        brick.GetComponent<Rigidbody>().mass = mass;
        brick.GetComponent<Brick>().Mark = "layer_" + curLayerNum + "_row_" + itemMark;
        return brick;
    }

    protected abstract TowerLayer BuildLayer(Transform parent, int length);


}

public class BuildParams
{
    public GameObject brick;
    public ColorBind[] colorBind;
    public TowerShaper towerLayer;
    public float startY;
    public float brickHight;
    public float startMass;
    public float increaseMass;
    public Transform host;
    public float startMark;
    public int seek;

    public BuildParams(TowerShaper _towerLayer, float _startY, float _brickHight, float _startMass, float _increaseMass, float _startMark,
        Transform _host, GameObject _bricks, ColorBind[] _colorBind, int _seek)
    {
        towerLayer = _towerLayer;
        startY = _startY;
        brickHight = _brickHight;
        startMass = _startMass;
        increaseMass = _increaseMass;
        startMark = _startMark;
        host = _host;
        brick = _bricks;
        colorBind = _colorBind;
        seek = _seek;
    }
}
