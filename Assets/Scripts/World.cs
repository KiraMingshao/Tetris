using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject worldCell;

    public Transform[,] cell;
    public int cellCountX;
    public int cellCountY;

    public float tickInterval;

    private float minX = -5.36f;
    private float maxX = -1.04f;
    private float minY = -4.56f;
    private float maxY =  4.56f;
    private float step =  0.48f;

    public GameObject[] tetrisSprite;

    public Tetris tetris { get; set; }

    public World()
    {
        tickInterval = 1.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        tickInterval = 1.5f;
        GenerateWorld();

        cellCountX = (int)((maxX - minX) / step + 1);
        cellCountY = (int)((maxY - minY) / step + 1);
        InitiateCell();
    }

    // Update is called once per frame
    void Update()
    {
        if (tetris == null)
            GenerateTetris();

    }

    public void GenerateWorld()
    {
        for(float x = minX; x <= maxX; x += step)
            for(float y = minY; y <= maxY; y += step)
            {
                Vector2 vector = new Vector2(x, y); 
                Instantiate(worldCell, vector, Quaternion.identity);
            }
    }

    public void InitiateCell()
    {
        cell = new Transform[cellCountX, cellCountY];
    }

    public void MoveTetrisIntoCells()
    {
        foreach(Transform square in tetris.transform)
        {
            if (square.GetComponent<SpriteRenderer>() != null)
                continue;

            int cellX = (int)((square.transform.position.x + 5.36f) / step);
            int cellY = (int)((square.transform.position.y + 4.56f) / step);

            cell[cellX, cellY] = square.transform;
        }

        RowElimitation();
    }


    public void GenerateTetris()
    {
        int randomType = Random.Range(0, 7);
        GameObject currentTetrisSprite = Instantiate(tetrisSprite[randomType]);
        tetris = currentTetrisSprite.GetComponent<Tetris>();
        tetris.transform.SetParent(this.transform);
        tetris.Initiate(new Vector2(-3.2f, 5.04f));
    }

    public bool IsInEmptyCells(Transform tetris)
    {
        foreach (Transform square in tetris.transform)
        {
            if (square.GetComponent<SpriteRenderer>() != null)
                continue;

            Vector2 squarePosition = square.transform.position;
            int cellX = (int)((square.transform.position.x + 5.36f) / step);
            int cellY = (int)((square.transform.position.y + 4.56f) / step);

            if (!IsBorder(squarePosition) || cell[cellX, cellY] == null || !IsBottom(squarePosition))
                return true;
        }
        return false;
    }


    public bool IsAtBottom(Transform tetris)
    {
        foreach (Transform square in tetris.transform)
        {
            if (square.GetComponent<SpriteRenderer>() != null)
                continue;

            Vector2 squarePosition = square.transform.position;
            int cellX = (int)((square.transform.position.x + 5.36f) / step);
            int cellY = (int)((square.transform.position.y + 4.56f) / step);

            Debug.Log(cellX);
            Debug.Log(cellY);

            if (cell[cellX, cellY] != null || IsBottom(squarePosition))
                return true;
        }
        return false;
    }

    public bool IsBorder(Vector2 squarePosition)
    {
        if(squarePosition.x >= maxX || squarePosition.x <= minX)
            return true;

        return false;
    }

    public bool IsBottom(Vector2 squarePosition)
    {
        if (squarePosition.y <= minY)
            return true;

        return false;
    }


    public void RowElimitation()
    {
        bool IsFullRow;
        for (float y = minY, j = 0; y <= maxY && j <= cellCountY; y += step, j++)
        {
            IsFullRow = true;
            for (float x = minX, i = 0; x <= maxX && i <= cellCountX; x += step, i++)
            {
                if (cell[(int)i, (int)j] == null)
                {
                    IsFullRow = false;
                    break;
                }
            }

            if (IsFullRow)
            {
                for(int i =  0; i < cellCountX; i++)
                {
                    Destroy(cell[i, (int)j].gameObject);
                    cell[i, (int)j] = null;
                }

                MoveRowDown((int)j + 1);
                j--;
            }
        }
    }

    public void MoveRowDown(int row)
    {
        for (int j = row; j < cellCountY; j++)
        {
            for (int i = 0; i < cellCountX; i++)
            {
                if (cell[i, j].gameObject == null)
                {
                    cell[i, row - 1] = cell[i, row];
                    cell[i, row] = null;
                    cell[i, row - 1].position -= Vector3.down * step;
                }
            }
        }
    }

}
