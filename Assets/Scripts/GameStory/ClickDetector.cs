using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickDetector : MonoBehaviour
{
    public UnityEvent onStoryClickEvent;
    public UnityEvent onRoleClickEvent;
    
    private GraphicRaycaster _raycaster;

    private void Awake()
    {
        _raycaster = GetComponent<GraphicRaycaster>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pointerData = new PointerEventData(EventSystem.current);
            var results = new List<RaycastResult>();
            
            pointerData.position = Input.mousePosition;

            _raycaster.Raycast(pointerData, results);

            foreach (RaycastResult result in results)
            {
                string name = result.gameObject.name;
                
                if (name.Equals("Panel - Story"))
                {
                    onStoryClickEvent.Invoke();
                }
                else if (name.Equals("Panel - Role"))
                {
                    onRoleClickEvent.Invoke();
                }
            }
        }
    }
}

