using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingoCardGrid : MonoBehaviour
{
    // Size of a Bingo Card Cell
    [SerializeField] Vector2 _cellSize = new Vector2(1.0f, 1.0f);

    // Gap between bingo card cells in the X direction
    [SerializeField] float _gapX;

    // Gap between bingo card cells in the Y direction
    [SerializeField] float _gapY;

    const float NUM_ROWS = 5;
    const float NUM_COLS = 5;

    void Start()
    {
        GenerateGrid();
    }

    void Update()
    {
        
    }

    void GenerateGrid()
    {
        for(int row = 0; row < NUM_ROWS; ++row)
        {
            for(int col = 0; col < NUM_COLS; ++col)
            {
                GameObject bingoCellGO = new GameObject(string.Format("BingoCell_{0}-{1}", row, col));
            }
        }
    }
}
