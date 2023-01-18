using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed = 00f;

    LayerMask targetMask;
    Vector3 dir = Vector3.zero;

    bool usePhysical = false;

    BrickColor color;
    public BrickColor Color
    {
        get { return color; }
        set { color = value; }
    }

    private void Start()
    {
        targetMask = LayerMask.GetMask("Brick");
    }

    // Update is called once per frame
    void Update()
    {
        if (dir == Vector3.zero || usePhysical)
        {
            return;
        }

        transform.position += dir * Time.deltaTime * speed;
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 0.5f, targetMask))
        {
            Brick brick = hitInfo.collider.GetComponent<Brick>();
            if (brick.color == color)
            {
                brick.BeShot();
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(Rebounce(brick));
            }

        }

    }

    public void Shoot(Vector3 targetPoint)
    {
        dir = (targetPoint - transform.position).normalized;
    }

    IEnumerator Rebounce(Brick brick)
    {
        usePhysical = true;
        Debug.Log("do rebounce");
        //   brick.GetComponent<MeshRenderer>().material = GameManagers.Instance.GetByColor(BrickColor.Defaualt);
        Rigidbody brickRB = brick.GetComponent<Rigidbody>();
        brickRB.isKinematic = true;
        gameObject.AddComponent<Rigidbody>();
        float startTime = Time.time;
        float arriveTime = startTime + 2f;

        var fistChange = true;

        while (Time.time < arriveTime)
        {
            float deltaTime = Time.time - startTime;
            //Debug.Log("detalTime=" + deltaTime);

            if (deltaTime > 1 && fistChange)
            {
                fistChange = false;
                Debug.Log("resume.");
                brickRB.isKinematic = false;
            }

            yield return null;
        }


        yield return null;
        //   Destroy(gameObject);
    }
}
