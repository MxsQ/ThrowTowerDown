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
        if (dir == Vector3.zero)
        {
            return;
        }

        transform.position += dir * Time.deltaTime * speed;
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 0.2f, targetMask))
        {
            Brick brick = hitInfo.collider.GetComponent<Brick>();
            if (brick.color == color)
            {
                brick.BeShot();
            }
            Destroy(gameObject);
        }

    }

    public void Shoot(Vector3 targetPoint)
    {
        dir = (targetPoint - transform.position).normalized;
    }
}
