using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IPull
{
    Vector3 calculateDirectionVector(GameObject targetedObject);
    void pull(GameObject targetedObject);
}