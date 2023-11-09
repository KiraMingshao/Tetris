using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float fallTime;
    public float lastFallTime;

    public GameObject tetris;

    public GameObject gameManager;
    
    private float step;

    private bool canFall;

    // Start is called before the first frame update
    void Start()
    {
        fallTime = 1.5f;
        lastFallTime = 0.0f;

        step = 0.48f;

        tetris = this.transform.gameObject;
        
        canFall = true;
    }

    // Update is called once per frame
    private void Update()
    {   
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
        Vector2 currentPos = transform.position;

        if(Input.GetKeyDown(KeyCode.A)) 
            tetris.transform.position = Vector2.MoveTowards(tetris.transform.position, new Vector2(tetris.transform.position.x - 1, tetris.transform.position.y), step);
        if (Input.GetKeyDown(KeyCode.D))
            tetris.transform.position = Vector2.MoveTowards(tetris.transform.position, new Vector2(tetris.transform.position.x + 1, tetris.transform.position.y), step);

        if(gameManager.GetComponent<GameManager>().IsValidPosition(transform) == false)
        {
            transform.position = currentPos;
        }
    }
    public void Fall()
    {
        if (canFall == false)
            return;

        tetris.transform.position = Vector2.MoveTowards(tetris.transform.position, new Vector2(tetris.transform.position.x, tetris.transform.position.y - 1), step);
    
        if(gameManager.GetComponent<GameManager>().IsValidPosition(transform) == false)
        {
            FallToBottom();
        }

    }

   public void FallToBottom()
    {
        canFall = false;
    }
    
}
