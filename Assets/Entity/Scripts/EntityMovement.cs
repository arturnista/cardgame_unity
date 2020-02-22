using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour, IMovement
{

    public virtual void MoveTo(Vector3 position)
    {
        transform.position = position;
    }

}
