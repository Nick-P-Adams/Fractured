using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private GameObject target;
    private GameObject targetCameraPos;
    private float offset = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
        {
            targetCameraPos = target.transform.Find("CameraPos").gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            lookAtTarget();
        }
    }

    private void lookAtTarget()
    {
        if (transform.position.z < (targetCameraPos.transform.position.z - offset) || transform.position.z > (targetCameraPos.transform.position.z + offset))
        {
            transform.position = Vector3.Slerp(transform.position, targetCameraPos.transform.position, 1.0f * Time.deltaTime);
        } 

        transform.LookAt(target.transform);
    }

    public void setTarget(GameObject target)
    {
        this.target = target;  
    }
}
