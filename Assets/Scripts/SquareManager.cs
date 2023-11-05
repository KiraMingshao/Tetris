using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareManager : MonoBehaviour
{

    public static bool canMoveLeft;
    public static bool canMoveRight;

    private void RangeLimitation()
    {
        this.transform.position = new Vector2(Mathf.Clamp(transform.position.x, -5.36f, -1.06f), Mathf.Clamp(transform.position.y, -4.56f, 6.0f));

        if(transform.position.x == -5.36f)
            canMoveLeft = false;
        else
            canMoveLeft = true;

        if(transform.position.x == -1.06f)
            canMoveRight = false;
        else
            canMoveRight = true;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        canMoveLeft = true;
        canMoveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        RangeLimitation();
    }
}
