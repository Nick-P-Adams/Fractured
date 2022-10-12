using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private bool targetable = true, active = false;
    private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.updateOutline();
    }

    private void OnTriggerEnter(Collider Dobj)
    {
        if (Dobj.GetComponent<PlayerController>() != null)
        {
            player = Dobj.gameObject;
            Dobj.GetComponent<PlayerController>().Register(gameObject);

            /*if (gameObject.GetComponent<Outline>() != null)
            {
                gameObject.GetComponent<Outline>().enabled = true;
                gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAll;
                gameObject.GetComponent<Outline>().OutlineColor = Color.black;
            }*/
        }
    }
    private void OnTriggerExit(Collider Dobj)
    {
        if (Dobj.GetComponent<PlayerController>() != null)
        {
            player = null;
            Dobj.GetComponent<PlayerController>().Unregister(gameObject);

            if (gameObject.GetComponent<Outline>() != null)
            {
                gameObject.GetComponent<Outline>().enabled = false;
            }
        }
    }

    private void updateOutline()
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerController>().curTarget() != null && player.GetComponent<PlayerController>().curTarget() == gameObject)
            {
                gameObject.GetComponent<Outline>().enabled = true;
                gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAndSilhouette;
                gameObject.GetComponent<Outline>().OutlineColor = Color.cyan;
            }
            else if(player.GetComponent<PlayerController>().curTarget() != gameObject)
            {
                gameObject.GetComponent<Outline>().enabled = false;
            }
        }
    }

    public void setTargetable(bool set) { this.targetable = set; }
    public bool isTargetable() { return targetable; }
    public void setActive(bool set){ this.active = set; }
    public bool getActive() { return this.active; }
}
