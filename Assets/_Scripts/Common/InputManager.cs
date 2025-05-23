using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    private static InputManager s_instance;

    public static Vector2 TouchPosition { get { return s_instance.m_touchPosition; } }
    public static bool IsDragging { get { return s_instance.m_isDragging; } }

    public Action m_onTouchStart, m_onTouchEnd;

    private bool m_isDragging;
    private Vector2 m_touchPosition;

    private void Awake()
    {
        s_instance = this;
    }
    private void OnDestroy()
    {
        s_instance = null;
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_touchPosition = Input.mousePosition;
            m_isDragging = true;
            m_onTouchStart.Invoke();
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            m_touchPosition = Input.mousePosition;
        }
        
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            m_isDragging = false;
            m_onTouchEnd.Invoke();
        }
#elif UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            Touch l_touchInput = Input.GetTouch(0);
            m_touchPosition = l_touchInput.position;

            if (l_touchInput.phase == TouchPhase.Began)
            {
                m_isDragging = true;
                m_onTouchStart?.Invoke();
            }
            else if(l_touchInput.phase == TouchPhase.Ended || l_touchInput.phase == TouchPhase.Canceled)
            {
                m_isDragging = false;
                m_onTouchEnd?.Invoke();
            }
        }
#endif
    }

    public static void AddTouchStartCallback(Action a_callbackToAdd)
    {
        if (s_instance != null)
        {
            s_instance.m_onTouchStart += a_callbackToAdd;
        }
    }
    public static void RemoveTouchStartCallback(Action a_callbackToRemove)
    {
        if (s_instance != null)
        {
            s_instance.m_onTouchStart -= a_callbackToRemove;
        }
    }

    public static void AddTouchEndCallback(Action a_callbackToAdd)
    {
        if (s_instance != null)
        {
            s_instance.m_onTouchEnd += a_callbackToAdd;
        }
    }
    public static void RemoveTouchEndCallback(Action a_callbackToRemove)
    {
        if (s_instance != null)
        {
            s_instance.m_onTouchEnd -= a_callbackToRemove;
        }
    }
}
