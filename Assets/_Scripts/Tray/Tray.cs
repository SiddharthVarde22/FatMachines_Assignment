using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tray : MonoBehaviour, Imovable/*IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler,*/
{
    [SerializeField]
    Rigidbody m_rigidBody;

    bool m_isMoving = false;
    Vector3 m_positionToMove;

    public void MovementToggle(bool a_canMove)
    {
        m_isMoving = a_canMove;
    }

    public void MoveToPosition(Vector3 a_newPosition)
    {
        m_positionToMove = a_newPosition;
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    m_isMoving = true;
    //    Debug.Log("Pointer down called");
    //}

    //public void OnPointerMove(PointerEventData eventData)
    //{
    //    m_positionToMove = eventData.position;
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    m_isMoving = false;
    //}

    private void FixedUpdate()
    {
        if(m_isMoving)
        {
            m_rigidBody.MovePosition(m_positionToMove);
        }
    }

    //private void OnMouseDown()
    //{
    //    m_isMoving = true;
    //}
    //private void OnMouseUp()
    //{
    //    m_isMoving = false;
    //}
}
