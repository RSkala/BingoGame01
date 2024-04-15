using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class BingoCard : MonoBehaviour
{
    // List of Bingo Cells. These are expected to be in order, from B1 through O5
    [SerializeField] BingoCell[] _bingoCells;

    void Start()
    {
        if(_bingoCells.Count() != BingoGame.NUM_BINGO_CELLS)
        {
            Debug.LogError("Incorrect number of Bingo Cells: " + _bingoCells.Count());
        }    
    }

    public void Init(List<int> bingoNumberList, UnityAction<BingoCell> onBingoCellClickedCallback)
    {
        InitBingoCells(bingoNumberList, onBingoCellClickedCallback);
    }

    void InitBingoCells(List<int> bingoNumberList, UnityAction<BingoCell> onBingoCellClickedCallback)
    {
        Assert.IsTrue(bingoNumberList.Count == BingoGame.NUM_BINGO_CELLS, "BingoCard.InitBingoCells - bingoNumberList != NUM_BINGO_CELLS");

        // Iterate through the list of BingoCells and initialize with its index and bingo number
        for(int bingoCellIndex = 0; bingoCellIndex < BingoGame.NUM_BINGO_CELLS; ++bingoCellIndex)
        {
            _bingoCells[bingoCellIndex].Init(bingoCellIndex, bingoNumberList[bingoCellIndex], onBingoCellClickedCallback);
        }
    }

    public int GetFreeSpaceBingoNumber() => _bingoCells[BingoGame.FREE_SPACE_INDEX].BingoNumber;
}
