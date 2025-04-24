using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayMover : MonoBehaviour
{
    [SerializeField]
    Camera m_camera;
    [SerializeField]
    LayerMask m_trayLayer;

    private Imovable m_movableTray = null;

    private void Start()
    {
        InputManager.AddTouchStartCallback(OnTouchStart);
        InputManager.AddTouchEndCallback(OnTouchEnd);
    }
    private void OnDestroy()
    {
        InputManager.RemoveTouchStartCallback(OnTouchStart);
        InputManager.RemoveTouchEndCallback(OnTouchEnd);
    }
    private void LateUpdate()
    {
        if(m_movableTray != null)
        {
            SetTrayMovePosition();
        }
    }
    private void OnTouchStart()
    {
        Debug.Log("Touch Started");
        //InputManager.TouchPosition
        if (Physics.Raycast(m_camera.ScreenPointToRay(InputManager.TouchPosition), out RaycastHit l_hitInfo, Mathf.Infinity, m_trayLayer))
        {
            m_movableTray = l_hitInfo.transform.GetComponent<Imovable>();
            if (m_movableTray == null) Debug.Log("Tray is null");
            Debug.Log(((Tray)m_movableTray).gameObject.name);
            m_movableTray.MovementToggle(true);
            // Experimental
            //m_movableTray.MoveToPosition(Vector3.forward * 5);
            SetTrayMovePosition();
        }
    }
    private void OnTouchEnd()
    {
        Debug.Log("Touch ended");
        if (m_movableTray != null)
        {
            m_movableTray.MovementToggle(false);
            m_movableTray.MoveToPosition(Vector3.zero);
            m_movableTray = null;
        }
    }
    private void SetTrayMovePosition()
    {
        Vector3 l_newPos = m_camera.ScreenToWorldPoint(
            new Vector3(InputManager.TouchPosition.x, InputManager.TouchPosition.y, m_camera.transform.position.y));
        m_movableTray.MoveToPosition(l_newPos);
    }
}
