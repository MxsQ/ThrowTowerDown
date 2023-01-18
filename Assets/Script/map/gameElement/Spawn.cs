using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour, PinObject
{
    [SerializeField] GameObject curBullectParent;
    [SerializeField] GameObject nextBullectParent;

    public static Spawn Instance;

    bool inStuff = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CameraMananger.Instance.SetPinObjecct(this);
    }


    public void Reset()
    {
        GeneratBullet(curBullectParent.transform);
        GeneratBullet();
    }

    public Bullet GetBulletToShoot()
    {
        if (inStuff)
        {
            return null;
        }
        var bullect = curBullectParent.GetComponentInChildren<Bullet>();
        bullect.gameObject.transform.parent = null;
        StartCoroutine(stuffBullet());
        return bullect;
    }

    IEnumerator stuffBullet()
    {
        inStuff = true;

        var bullect = nextBullectParent.GetComponentInChildren<Bullet>();
        Vector3 startPs = bullect.gameObject.transform.position;
        Vector3 endPs = curBullectParent.gameObject.transform.position;

        float moveTime = .2f;
        float startTime = Time.time;
        float targetTime = startTime + moveTime;
        while (Time.time < targetTime)
        {
            float factor = (Time.time - startTime) / moveTime;
            //   Debug.Log("now factor=" + factor);
            var nowPs = Vector3.Lerp(startPs, endPs, factor);
            bullect.gameObject.transform.position = nowPs;

            yield return null;
        }

        bullect.transform.position = endPs;
        bullect.transform.parent = curBullectParent.gameObject.transform;

        GeneratBullet();

        inStuff = false;
    }

    void GeneratBullet(Transform parent = null)
    {
        BrickColor color = LevelManager.Instance.GetValidBrickColor();
        var bullect = Instantiate(GameManagers.Instance.bullect);
        bullect.GetComponent<Bullet>().Color = color;
        Material material = GameManagers.Instance.GetByColor(color);
        bullect.gameObject.GetComponent<Renderer>().material = material;
        if (parent == null)
        {
            bullect.transform.parent = nextBullectParent.gameObject.transform;
        }
        else
        {
            bullect.transform.parent = parent;
        }
        bullect.transform.localPosition = Vector3.zero;

        // Debug.Log("new bullet is " + color.ToString());
    }

    public void OnCameraYChange(float curY)
    {
        var ps = gameObject.transform.position;
        gameObject.transform.position = new Vector3(ps.x, curY - .8f, ps.z);
    }

    public void OnCameraRotateAround(Vector3 center, Vector3 axis, float angle)
    {
        gameObject.transform.RotateAround(center, axis, angle);
    }
}
