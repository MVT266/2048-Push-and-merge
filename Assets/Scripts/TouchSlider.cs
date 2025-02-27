﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction OnPointerDownEvent, OnPointerUpEvent;
    public UnityAction<float> OnPointerDragEvent;

    private Slider uiSlider;

    private void Awake ()
    {
        uiSlider = GetComponent<Slider>();
        uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnPointerDownEvent != null) OnPointerDownEvent.Invoke();
        if (OnPointerDragEvent != null) OnPointerDragEvent.Invoke (uiSlider.value);
    }

    public void OnPointerUp (PointerEventData eventData)
    {
        if (OnPointerUpEvent != null) OnPointerUpEvent.Invoke();
        uiSlider.value = 0f ;
    }

    private void OnSliderValueChanged(float value)
    {
        if (OnPointerDragEvent != null) OnPointerDragEvent.Invoke(value);
    }

    private void OnDestroy ()
    {
        uiSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
