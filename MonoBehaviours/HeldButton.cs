using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Obsolete]
public class HeldButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool IsHeld { get; private set; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsHeld = false;
    }
}
