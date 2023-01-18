using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;

[RequireComponent(typeof(Rigidbody))]
public class Brick : MonoBehaviour
{
    [SerializeField] public BrickColor color;
    [SerializeField] public ParticleSystem eliminateEffect;

    public TowerLayer Host { get; set; }
    public string Mark;

    Vector3 original;

    BrickColor defaultColor = BrickColor.Defaualt;
    bool isPin = false;
    float awayFormThredhold = 1 * 1;

    public void Eliminate()
    {
        Host.Remove(gameObject);
        Destroy(gameObject);
        var effect = Instantiate<ParticleSystem>(eliminateEffect);
        effect.gameObject.transform.position = gameObject.transform.position;

        Destroy(effect.gameObject, effect.main.startLifetime.constantMax);
    }

    public void BeShot()
    {
        Debug.Log("be shot on: " + gameObject.transform.position);


        Dictionary<string, Brick> markDic = new Dictionary<string, Brick>();
        Stack<Brick> tmpStack = new Stack<Brick>();
        tmpStack.Push(this);
        markDic.Add(Mark, this);

        while (tmpStack.Count > 0)
        {
            var curBrick = tmpStack.Pop();
            findNeighourToStack(curBrick, markDic, tmpStack);
        }

        LevelManager.Instance.BrickOut(markDic.Count);

        foreach (var entry in markDic)
        {
            entry.Value.Eliminate();
        }
    }

    public void Pin()
    {
        var colorMaterial = GameManagers.Instance.GetByColor(defaultColor);
        gameObject.GetComponent<MeshRenderer>().material = colorMaterial;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        isPin = true;

    }

    public void Loosen()
    {
        var colorMaterial = GameManagers.Instance.GetByColor(color);
        gameObject.GetComponent<MeshRenderer>().material = colorMaterial;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        isPin = false;
        original = gameObject.transform.position;
    }

    public bool AwayFrom()
    {
        var disV = original - gameObject.transform.position;
        var dis = disV.x * disV.x + disV.y * disV.y + disV.z * disV.z;
        return !isPin && dis > awayFormThredhold;
    }

    public void printD()
    {
        /* var disV = original - gameObject.transform.position;
         float dis = disV.x * disV.x + disV.y * disV.y + disV.z * disV.z;
         Debug.Log("origin=" + original + "  now=" + gameObject.transform.position + " div=" + disV + "  dis=" + (disV.x * disV.x));*/
    }

    public bool SameColor(Brick brick)
    {
        if (brick.isPin || brick.isPin)
        {
            return false;
        }

        return color == brick.color;
    }

    void findNeighourToStack(Brick centerBrick, Dictionary<string, Brick> dic, Stack<Brick> stack)
    {
        Collider[] cs = Physics.OverlapSphere(centerBrick.gameObject.transform.position, 1.01f, LayerMask.GetMask("Brick"));

        foreach (Collider c in cs)
        {
            var _brick = c.gameObject.GetComponent<Brick>();
            if (SameColor(_brick) && !dic.ContainsKey(_brick.Mark))
            {
                //Debug.Log("get on : " + _brick.gameObject.transform.position + "  mark=" + _brick.Mark);
                //targets.Add(_brick);
                dic.Add(_brick.Mark, _brick);
                stack.Push(_brick);
            }
        }
    }
}

public enum BrickColor
{
    Defaualt,
    Color1,
    Color2,
    Color3,
}
