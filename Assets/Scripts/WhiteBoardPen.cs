using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBoardPen : MonoBehaviour
{
    private WhiteBoard whiteBoard;
    private RaycastHit touch;
    private float tipHeight;
    

    // Start is called before the first frame update
    void Start()
    {
        tipHeight = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        touchWhiteBoard();
    }

    private void touchWhiteBoard()
    {
        if (Physics.Raycast(transform.position, transform.up, out touch, tipHeight))
        {
            if (touch.collider.GetComponent<WhiteBoard>() != null)
            {
                this.whiteBoard = touch.collider.GetComponent<WhiteBoard>();

                this.whiteBoard.SetColor(Color.blue);
                //Debug.Log("coords X: " + touch.textureCoord.x + " Y: " + touch.textureCoord.y);
                this.whiteBoard.SetTouchPosition(touch.textureCoord.x, touch.textureCoord.y);
                this.whiteBoard.SetTouch(true);
            }
        }
        else
        {
            if (this.whiteBoard != null)
            {
                this.whiteBoard.SetTouch(false);
            }
        }
    }
}
