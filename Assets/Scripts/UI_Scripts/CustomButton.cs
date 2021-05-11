using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent myEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        myEvent.Invoke();
    }
}
