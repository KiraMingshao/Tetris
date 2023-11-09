using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float fallTime;
    public float lastFallTime;

    public GameObject tetris;
    
    private float step;

    private bool moveLeft;
    private bool moveRight;

    private int childCount;
    List<GameObject> Child = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        fallTime = 1.5f;
        lastFallTime = 0.0f;

        step = 0.48f;

        tetris = this.transform.gameObject;

        moveLeft = true;
        moveRight = true;

        foreach (Transform child in tetris.transform)
        {
            Child.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        canMove();
        Move();
        
        lastFallTime += Time.deltaTime;
        if(lastFallTime >= fallTime)
        {
            Fall();
            lastFallTime = 0.0f;
        }
    }

    public void Move()
    {
        if(Input.GetKeyDown(KeyCode.A) && moveLeft == true) 
            tetris.transform.position = Vector2.MoveTowards(tetris.transform.position, new Vector2(tetris.transform.position.x - 1, tetris.transform.position.y), step);
        if (Input.GetKeyDown(KeyCode.D) && moveRight == true)
            tetris.transform.position = Vector2.MoveTowards(tetris.transform.position, new Vector2(tetris.transform.position.x + 1, tetris.transform.position.y), step);
    }
    public void Fall()
    {
        tetris.transform.position = Vector2.MoveTowards(tetris.transform.position, new Vector2(tetris.transform.position.x, tetris.transform.position.y - 1), step);
    }

    private void canMove()
    {
        
        foreach(GameObject square in Child)
        {
            if (!SquareManager.canMoveLeft)
                moveLeft = false;
            else
                moveLeft = true;
        }
        foreach (GameObject square in Child)
        {
            if (!SquareManager.canMoveRight)
                moveRight = false;
            else 
                moveRight = true;
        }
    }
    
}
