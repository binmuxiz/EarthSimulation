using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickDetector : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pointerData = new PointerEventData(EventSystem.current);
            var results = new List<RaycastResult>();
            
            pointerData.position = Input.mousePosition;
            
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject == gameObject)
                {
                    Debug.Log($"{gameObject.name} 클릭됨!");
                }
            }
            
        }
    }
}

