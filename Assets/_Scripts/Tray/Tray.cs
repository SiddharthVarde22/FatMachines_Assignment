using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tray : MonoBehaviour, Imovable
{
    [SerializeField]
    Rigidbody m_rigidBody;
    [SerializeField]
    float m_moveSpeed = 5;
    [SerializeField]
    Vector3 m_bottomRightCorner;

    bool m_isMoving = false;
    Vector3 m_positionToMove;

    private void Start()
    {
        SnapPositionOnGrid();
    }

    public void MovementToggle(bool a_canMove)
    {
        m_isMoving = a_canMove;
        m_rigidBody.isKinematic = !a_canMove;
        if(!a_canMove)
        {
            SnapPositionOnGrid();
        }
    }

    public void MoveToPosition(Vector3 a_newPosition)
    {
        m_positionToMove = a_newPosition;
    }

    Vector3 m_currvelocity;
    private void FixedUpdate()
    {
        if(m_isMoving)
        {
            Vector3 l_direction = m_positionToMove - transform.position;
            if (l_direction.sqrMagnitude >= 0.05f)
            {
                //m_rigidBody.AddForce((l_direction).normalized, ForceMode.Impulse);
                //m_rigidBody.MovePosition(transform.position + (l_direction.normalized * Time.deltaTime * m_moveSpeed));
                m_rigidBody.MovePosition(
                    Vector3.SmoothDamp(transform.position, transform.position + (l_direction.normalized * m_moveSpeed * Time.deltaTime),
                    ref m_currvelocity, 0.05f));
            }
            //m_rigidBody.MovePosition(Vector3.Lerp(transform.position, m_positionToMove, Time.deltaTime * m_moveSpeed));
            //m_rigidBody.Move(m_positionToMove, Quaternion.identity);
        }
    }
    private void SnapPositionOnGrid()
    {
        m_positionToMove = Grid.FindNearestPositionOnGrid(transform.position - m_bottomRightCorner) + m_bottomRightCorner;
        m_rigidBody.MovePosition(m_positionToMove);
    }
}
