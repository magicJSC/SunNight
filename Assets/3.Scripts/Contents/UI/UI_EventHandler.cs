using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour,IPointerClickHandler,IDragHandler,IPointerDownHandler
{
    public Action<PointerEventData> _OnClick;
    public Action<PointerEventData> _OnDrag;
    public Action<PointerEventData> _OnDown;

    public void OnDrag(PointerEventData eventData)
    {
        if (_OnDrag != null)
            _OnDrag?.Invoke(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_OnClick != null)
            _OnClick?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_OnDown != null)
            _OnDown?.Invoke(eventData);
    }
}
