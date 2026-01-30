using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class ScrollRectPosition : MonoBehaviour
{
    [SerializeField] public InputActionReference rightHandPrimary2DAxis;
    [SerializeField] public float scrollSpeed = 0.1f;
    private ScrollRect scrollRect;

    public void Start()
    {
        scrollRect = this.gameObject.GetComponent<ScrollRect>();
        rightHandPrimary2DAxis.action.Enable();
    }

    public void Update()
    {
        if (rightHandPrimary2DAxis != null && rightHandPrimary2DAxis.action.enabled)    
        {
            Vector2 joystickValue = rightHandPrimary2DAxis.action.ReadValue<Vector2>();
            print(joystickValue);
            print(scrollRect.verticalScrollbar.value);
            scrollRect.verticalScrollbar.value = Mathf.Clamp(scrollRect.verticalScrollbar.value + (joystickValue.y * scrollSpeed), 0f, 1f);
        }
    }
}
