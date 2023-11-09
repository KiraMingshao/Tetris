using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int row = 24;
    public const int col = 10;
    //public float step = 0.48f;

    public float minX = -5.36f;
    public float maxX = -1.04f;
    public float minY = -4.56f;

    Transform[,] mapArray = new Transform[row, col];
    // Start is called before the first frame update
    private void Start()
    {
        //for (int r = 0;  r < row; r++)
        //    for (int c = 0; c < col; c++)
        //    {
        //        float xPos = minX + r * step;
        //        float yPos = minY + c * step;

        //        mapArray[c, r].position = new Vector2(xPos, yPos);
        //    }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public bool IsValidPosition(Transform shape)
    {
        foreach(Transform block in shape.transform)
        {
            if (block.GetComponent<SpriteRenderer>() == null) continue;

            Vector2 blockPos = block.transform.position;

            if (IsBorder(blockPos))
                return false;
        }

        return true;
    }

    private bool IsBorder(Vector2 blockPos) 
    {
        if(blockPos.y <= minY || blockPos.x >= maxX || blockPos.x <= minX)
            return true;

        return false;
    }
}
