using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;


public class UI_Highlight_Buttom : MonoBehaviour
{
    [Header("Tween Settings")]
    public float hoverScale = 1.1f;
    public float hoverDuration = 0.2f;
    public float clickScale = 0.9f;
    public float clickDuration = 0.1f;

    [Header("Color Settings")]
    public Color hoverColor = Color.yellow;
    public float colorDuration = 0.2f;

    [Header("Button Events")]
    public UnityEvent onClick;
    public UnityEvent onHoverEnter;
    public UnityEvent onHoverExit;

    private Vector3 originalScale;
    private Color originalColor;
    private Tween currentTween;
    private Image _image;
    private bool isHovering = false;
    private Mouse mouse;

    private void Awake()
    {
        _image = GetComponent<Image>();
        originalScale = transform.localScale;
        originalColor = _image.color;
        mouse = Mouse.current;
    }

    private void Update()
    {
        if (mouse == null) return;

        // Check if mouse is over this UI element
        Vector2 mousePos = mouse.position.ReadValue();
        RectTransform rect = GetComponent<RectTransform>();

        if (RectTransformUtility.RectangleContainsScreenPoint(rect, mousePos, Camera.main))
        {
            if (!isHovering)
            {
                isHovering = true;
                HoverEnter();
            }

            if (mouse.leftButton.wasPressedThisFrame)
            {
                Click();
            }
        }
        else if (isHovering)
        {
            isHovering = false;
            HoverExit();
        }
    }

    private void HoverEnter()
    {
        currentTween?.Kill();
        Sequence seq = DOTween.Sequence();
        seq.Join(transform.DOScale(originalScale * hoverScale, hoverDuration).SetEase(Ease.OutBack));
        seq.Join(_image.DOColor(hoverColor, colorDuration));
        currentTween = seq;

        onHoverEnter?.Invoke();
    }

    private void HoverExit()
    {
        currentTween?.Kill();
        Sequence seq = DOTween.Sequence();
        seq.Join(transform.DOScale(originalScale, hoverDuration).SetEase(Ease.InBack));
        seq.Join(_image.DOColor(originalColor, colorDuration));
        currentTween = seq;

        onHoverExit?.Invoke();
    }

    private void Click()
    {
        currentTween?.Kill();
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(originalScale * clickScale, clickDuration).SetEase(Ease.InOutSine));
        seq.Append(transform.DOScale(originalScale * hoverScale, hoverDuration).SetEase(Ease.OutBack));
        currentTween = seq;

        onClick?.Invoke();
    }
}
