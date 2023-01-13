using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMananger : MonoBehaviour
{
    public static CameraMananger Instance;

    Camera camera;
    PinObject pinObj;

    float targetDownY;

    private void Awake()
    {
        Instance = this;
        camera = Camera.main;
    }

    public void UpTo(float y)
    {
        StartCoroutine(UpCameraAnim(y));
    }

    IEnumerator UpCameraAnim(float y)
    {
        var config = GameManagers.Instance.conifg;
        var cameraOriginPs = camera.transform.position;
        float startY = cameraOriginPs.y;
        float endY = y;

        if (endY < startY)
        {
            yield return null;
        }

        float spanTime = config.CameraChangeTimeOnStart;
        float deltaTime = 0;
        float startTime = Time.time;
        float ariiveTime = startTime + spanTime;
        float angelSpeed = config.CameraChangeRotateSpeedOnStart * Mathf.Deg2Rad;
        Transform cameraT = Camera.main.transform;

        //    Debug.Log("start " + startY + "  endY " + endY);

        while (Time.time < ariiveTime)
        {
            deltaTime = Time.time - startTime;
            float curY = Mathf.Lerp(startY, endY, deltaTime / spanTime);
            Debug.Log("CurTime=" + deltaTime + " next Y " + curY);
            cameraT.position = new Vector3(cameraT.position.x, curY, cameraT.position.z);
            pinObj.OnCameraYChange(curY);

            var rotateCenter = new Vector3(0, curY, 0);
            var asix = Vector3.up;
            var angle = angelSpeed * deltaTime;
            cameraT.RotateAround(rotateCenter, asix, angle);
            pinObj.OnCameraRotateAround(rotateCenter, asix, angle);
            yield return null;
        }
        cameraT.position = new Vector3(cameraT.position.x, endY, cameraT.position.z);
        pinObj.OnCameraYChange(endY);
    }

    public void DownTo(float y)
    {
        float startY = camera.transform.position.y;
        targetDownY = y;
        if (startY < targetDownY)
        {
            return;
        }

        StopCoroutine("DownCameraAnim");
        StartCoroutine("DownCameraAnim");
    }

    public void SetPinObjecct(PinObject obj)
    {
        pinObj = obj;
    }


    IEnumerator DownCameraAnim()
    {
        var config = GameManagers.Instance.conifg;
        float startY = camera.transform.position.y;

        float spanTime = config.CameraChangeTimeOnDown;
        float deltaTime = 0;
        float startTime = Time.time;
        float ariiveTime = startTime + spanTime;
        float angelSpeed = config.CameraChangeRotateSpeedOnStart * Mathf.Deg2Rad;
        Transform cameraT = Camera.main.transform;

        while (Time.time < ariiveTime)
        {
            deltaTime = Time.time - startTime;
            float curY = Mathf.Lerp(startY, targetDownY, deltaTime / spanTime);
            cameraT.position = new Vector3(cameraT.position.x, curY, cameraT.position.z);
            pinObj.OnCameraYChange(curY);

            yield return null;
        }
        cameraT.position = new Vector3(cameraT.position.x, targetDownY, cameraT.position.z);
        pinObj.OnCameraYChange(targetDownY);
    }
}

public interface PinObject
{
    public void OnCameraYChange(float curY);
    public void OnCameraRotateAround(Vector3 center, Vector3 axis, float angle);
}
