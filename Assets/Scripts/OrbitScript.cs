using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitScript : MonoBehaviour
{
    public const float ORBIT_ANGLE_MIN = -50.0f, ORBIT_ANGLE_MAX = 50.0f;
    public Transform lookAt;
    public float distance = 0.1f, sensitivityX = 1.0f, sensitivityY = 1.0f;

    private float currentX = 180.0f, currentY = -25.0f;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        this.currentX += Input.GetAxis("Mouse X");
        this.currentY += Input.GetAxis("Mouse Y");
        this.currentY = Mathf.Clamp(currentY, ORBIT_ANGLE_MIN, ORBIT_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 direction = new Vector3(0, 0, distance);
        Quaternion rotation = Quaternion.Euler(currentY * sensitivityY, currentX * sensitivityX, 0);
        Quaternion camYRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        transform.position = lookAt.position + rotation * direction;
        lookAt.rotation = camYRotation;
        transform.LookAt(lookAt.position);
    }
}
