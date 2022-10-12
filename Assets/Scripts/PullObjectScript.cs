using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObjectScript : MonoBehaviour, IPull
{
    public float pullForceScalar, downForce;

    void Update()
    {
        if (this.GetComponent<PlayerController>() != null && this.GetComponent<PlayerController>().curTarget() != null 
            && this.GetComponent<PlayerController>().curTarget().GetComponent<Rigidbody>() != null && Input.GetKey("e"))
        {
            this.pull(this.GetComponent<PlayerController>().curTarget());
        }
    }
    public Vector3 calculateDirectionVector(GameObject targetedObject)
    {
        Vector3 curPosition, objPosition;
       
        curPosition = transform.position;
        objPosition = targetedObject.transform.position;

        Vector3 direction = curPosition - objPosition;

        return direction;
    }

    public void pull(GameObject targetedObject)
    {
        if (targetedObject != null)
        {
            Debug.Log("pull");
            targetedObject.GetComponent<Rigidbody>().AddForce(this.calculateDirectionVector(targetedObject) * pullForceScalar, ForceMode.Force);
            targetedObject.GetComponent<Rigidbody>().AddForce((-targetedObject.transform.up) * downForce, ForceMode.Force);
        }
    }
}
