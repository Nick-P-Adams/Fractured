using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ITargeter
{
    private List<GameObject> registeredObjects = new List<GameObject>();
    public Camera playerCam;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        interact();
    }

    public GameObject curTarget()
    {
        GameObject target = null;
        var ray = playerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            target = hit.transform.gameObject;

            if (target.GetComponent<ObjectController>() != null && target.GetComponent<ObjectController>().isTargetable() && registeredObjects.Contains(target))
            {
                return target;
            }
        }

        return null;
    }

    private void interact()
    {
        if (curTarget() != null)
        {
            if (curTarget().GetComponent<ObjectController>() != null && curTarget().GetComponent<WhiteBoard>() != null && Input.GetKey("e"))
            {
                playerCam.GetComponent<FirstPersonScript>().enabled = false;
                gameObject.GetComponent<MovementScript>().enabled = false;
                playerCam.GetComponent<LookAt>().setTarget(curTarget());
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                playerCam.GetComponent<LookAt>().enabled = true;
                curTarget().GetComponent<ObjectController>().setActive(true);
            }
        }
    }

    public void Register(GameObject target)
    {
        if (target.GetComponent<ObjectController>() != null && target.GetComponent<ObjectController>().isTargetable() && !(registeredObjects.Contains(target)))
        {
            Debug.Log(target.gameObject.name + " Was Registered With Player");
            registeredObjects.Add(target);
        }
    }

    public void Unregister(GameObject target)
    {
        if (target.GetComponent<ObjectController>() != null && target.GetComponent<ObjectController>().isTargetable() && registeredObjects.Contains(target))
        {
            Debug.Log(target.gameObject.name + " Was Unregistered With Player");
            registeredObjects.Remove(target);
        }
    }
}
