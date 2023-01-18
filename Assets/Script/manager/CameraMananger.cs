using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class CameraMananger : MonoBehaviour
{
    public static CameraMananger Instance;

    [SerializeField] public Transform cameraT;

    PinObject pinObj;

    float targetDownY;

    private void Awake()
    {
        Instance = this;

    }

    public void UpTo(float y)
    {
        StartCoroutine(UpCameraAnim(y));
    }

    IEnumerator UpCameraAnim(float y)
    {

        var config = GameManagers.Instance.conifg;
        var cameraOriginPs = cameraT.position;
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

        Debug.Log("start to up camera.");
        //    Debug.Log("start " + startY + "  endY " + endY);

        while (Time.time < ariiveTime)
        {
            deltaTime = Time.time - startTime;
            float curY = Mathf.Lerp(startY, endY, deltaTime / spanTime);
            //    Debug.Log("CurTime=" + deltaTime + " next Y " + curY);
            cameraT.position = new Vector3(cameraT.position.x, curY, cameraT.position.z);
            pinObj.OnCameraYChange(curY);

            var rotateCenter = new Vector3(0, curY, 0);
            var asix = Vector3.up;
            var angle = angelSpeed * deltaTime;
            RotateAround(rotateCenter, asix, angle);
            yield return null;
        }
        cameraT.position = new Vector3(cameraT.position.x, endY, cameraT.position.z);
        pinObj.OnCameraYChange(endY);
        Debug.Log("ends to up camera.");
    }

    public void DownTo(float y)
    {
        float startY = cameraT.position.y;
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
        float startY = cameraT.position.y;

        float spanTime = config.CameraChangeTimeOnDown;
        float deltaTime = 0;
        float startTime = Time.time;
        float ariiveTime = startTime + spanTime;
        float angelSpeed = config.CameraChangeRotateSpeedOnStart * Mathf.Deg2Rad;


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

    public void RotateAround(Vector3 center, Vector3 axis, float angle)
    {
        cameraT.transform.RotateAround(center, axis, angle);
        pinObj.OnCameraRotateAround(center, axis, angle);
    }


    //public void up()
    //{
    //    inYChange = true;
    //    MMF_RotatePositionAround mmAround = upFeedback.GetFeedbackOfType<MMF_RotatePositionAround>();
    //    mmAround.RemapCurveOne = 540;
    //    upFeedback.Initialization();
    //    upFeedback.PlayFeedbacks();
    //}
}

public interface PinObject
{
    public void OnCameraYChange(float curY);
    public void OnCameraRotateAround(Vector3 center, Vector3 axis, float angle);
}
