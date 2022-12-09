using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPointerClickHandler
{
    LayerMask brickMask;

    private void Awake()
    {
        brickMask = LayerMask.GetMask("Brick");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("work");
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, brickMask))
        {
            hitInfo.collider.GetComponent<Brick>().Eliminate();
        }
    }
}
