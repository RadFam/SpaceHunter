using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneralSubMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver = false;
    public bool isOverLocked = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isOverLocked)
        {
            isOver = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isOverLocked)
        {
            isOver = false;
        }
    }
    public bool isOnMouse()
    {
        if (!isOverLocked)
        {
            return isOver;
        }
        return true;
    }
}
