using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ControlScreen : MonoBehaviour, IDragHandler
{
    public static UnityAction<float> OnDragAction;

    public void OnDrag(PointerEventData eventData)
    {
        OnDragAction?.Invoke(eventData.delta.x);
    }
}
