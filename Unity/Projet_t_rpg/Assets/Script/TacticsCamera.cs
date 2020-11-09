using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour
{// script qui permet de changer l'angle de la caméra de 90° à gauche ou à droite
public void RotateLeft()
    {
        transform.Rotate(Vector3.up, 90, Space.Self);
    }
    public void RotateRight()
    {
        transform.Rotate(Vector3.up, -90, Space.Self);

    }
}