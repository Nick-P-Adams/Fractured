using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonScript : MonoBehaviour
{
    public const float YANGLE_MIN = -60.0f, YANGLE_MAX = 60.0f;
    public GameObject body;
    public float distance = 1.2f, sensitivityX = 1.0f, sensitivityY = 1.0f;

    private float currentX = 0.0f, currentY = -25.0f, timeCount = 0.0f;
    private Quaternion rotation, bodyXZRotation, camXZRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        this.currentX += Input.GetAxis("Mouse X");
        this.currentY += Input.GetAxis("Mouse Y");
        
        this.currentY = Mathf.Clamp(currentY, YANGLE_MIN, YANGLE_MAX);
    }

    private void LateUpdate()
    {
        transform.position = body.transform.position + new Vector3(0.0f, distance, 0.0f);

        rotation = Quaternion.Euler((-currentY) * sensitivityY, currentX * sensitivityX, 0.0f);
        camXZRotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);
        bodyXZRotation = Quaternion.Euler(0.0f, body.transform.rotation.eulerAngles.y, 0.0f);

        transform.rotation = rotation;

        body.transform.rotation = Quaternion.Slerp(bodyXZRotation, camXZRotation, timeCount);
        timeCount = timeCount + Time.deltaTime;
    }
}
