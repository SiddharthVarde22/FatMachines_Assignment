using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Imovable
{
    public void MovementToggle(bool a_canMove);
    public void MoveToPosition(Vector3 a_newPosition);
}
