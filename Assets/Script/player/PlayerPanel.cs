using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPanel : BasePanel, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    LayerMask brickMask;

    Vector2 pointterEnterPs;
    Vector2 lastDragPs;
    GameObject ratateCenter;
    float curRotateAngle = 0;

    Spawn spawn;

    void Start()
    {
        spawn = FindObjectOfType<Spawn>();
        Hide();
        base.Start();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("OnDrag");
        //Debug.Log(" last=" + lastDragPs + " cur=" + eventData.position);
        float offsetX = eventData.position.x - lastDragPs.x;
        lastDragPs = eventData.position;
        float maxDis = GameManagers.Instance.conifg.rotateCircleDis;
        float angle = offsetX / maxDis * 360;
        curRotateAngle += angle;
        curRotateAngle %= 360;
        var cameraObj = Camera.main.gameObject;

        //    Debug.Log("offset=" + offsetX + "  angle=" + angle);

        Quaternion rotateion = Quaternion.Euler(0, angle, 0);
        CameraMananger.Instance.RotateAround(ratateCenter.transform.position, Vector3.up, curRotateAngle * Mathf.Deg2Rad);
        cameraObj.transform.RotateAround(ratateCenter.transform.position, Vector3.up, curRotateAngle * Mathf.Deg2Rad);

        //spawn.gameObject.transform.RotateAround(ratateCenter.transform.position, Vector3.up, curRotateAngle * Mathf.Deg2Rad);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //   Debug.Log("OnPointerDown");
        pointterEnterPs = eventData.position;
        lastDragPs = pointterEnterPs;
        ratateCenter = new GameObject();
        ratateCenter.transform.position = new Vector3(0, Camera.main.gameObject.transform.position.y, 0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //   Debug.Log("OnPointerUp");
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100, brickMask))
        {
            Bullet bullet = Spawn.Instance.GetBulletToShoot();
            //Vector3 cameraPs = Camera.main.gameObject.transform.position;
            //Vector3 startPosition = new Vector3(cameraPs.x, cameraPs.y - 5, cameraPs.z);
            //  bullet.gameObject.transform.position = startPosition;
            bullet?.Shoot(hitInfo.collider.transform.position);

        }
        ratateCenter = null;
    }



    private void Awake()
    {
        brickMask = LayerMask.GetMask("Brick");
    }

    public override Panel Name()
    {
        return Panel.Player;
    }
}
