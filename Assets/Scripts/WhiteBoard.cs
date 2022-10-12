using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WhiteBoard : MonoBehaviour
{
    private int textureSize = 2048;
    private int penSize = 10;
    private Texture2D texture;
    private Color[] color;

    private bool touching, touchingLast;
    private float posX, posY, lastX, lastY, mouseX, mouseY;
    private Vector3 mouseCntrl;
    public GameObject pen;

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        this.texture = new Texture2D(textureSize, textureSize);
        renderer.material.mainTexture = this.texture;
    }

    // Update is called once per frame
    void Update()
    {
        draw();

        if (gameObject.GetComponent<ObjectController>().getActive() == true)
        {
            setPenPosition();
        }
    }

    private void draw()
    {
        int x = (int)(posX * textureSize - (penSize / 2));
        int y = (int)(posY * textureSize - (penSize / 2));

        if (touchingLast)
        {
            //Debug.Log("coords X: " + x + " Y: " + y);
            texture.SetPixels(x, y, penSize, penSize, color);

            for (float i = 0.01f; i < 1.00f; i += 0.01f)
            {
                int lerpX = (int)Mathf.Lerp(lastX, (float)x, i);
                int lerpY = (int)Mathf.Lerp(lastY, (float)y, i);
                texture.SetPixels(lerpX, lerpY, penSize, penSize, color);
            }

            texture.Apply();
        }

        this.lastX = (float)x;
        this.lastY = (float)y;

        this.touchingLast = this.touching;
    }

    private void setPenPosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        this.mouseX += Input.GetAxis("Mouse X");
        this.mouseY += Input.GetAxis("Mouse Y");

        if (Physics.Raycast(ray, out hit))
        {
            mouseCntrl = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.7f));
            pen.transform.position = mouseCntrl;
        }

        //pen.transform.position = new Vector3(-mouseY, mouseX, transform.position.z);
    }

    public void SetTouch(bool touching)
    {
        this.touching = touching;
    }

    public void SetTouchPosition(float x, float y)
    {
        this.posX = x;
        this.posY = y;
    }

    public void SetColor(Color color)
    {
        this.color = Enumerable.Repeat<Color>(color, penSize * penSize).ToArray<Color>();
    }
}
