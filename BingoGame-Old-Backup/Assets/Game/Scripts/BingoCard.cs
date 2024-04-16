using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BingoCard : MonoBehaviour {
    [SerializeField] Transform bingoGrid = null;

    List<BingoCell> _bingoCells;

    void Start() {

    }

    public void Init(GameObject bingoCellPrefab, List<int> bingoNumberList, Action<BingoCell> onBingoCellClickedCallback) {
        Debug.Log("BingoCard.Init");
        name = "Bingo Card";
        InitBingoCells(bingoCellPrefab, bingoNumberList, onBingoCellClickedCallback);
    }

    void InitBingoCells(GameObject bingoCellPrefab, List<int> bingoNumberList, Action<BingoCell> onBingoCellClickedCallback) {
        Assert.IsTrue(bingoNumberList.Count == BingoGame.NUM_BINGO_CELLS, "BingoCard.InitBingoCells - bingoNumberList != NUM_BINGO_CELLS");
        _bingoCells = new List<BingoCell>();

        for(int bingoCellIndex = 0; bingoCellIndex < BingoGame.NUM_BINGO_CELLS; ++bingoCellIndex) {
            BingoCell bingoCell = Instantiate(bingoCellPrefab, bingoGrid).GetComponent<BingoCell>();
            bingoCell.Init(bingoCellIndex, bingoNumberList[bingoCellIndex], onBingoCellClickedCallback);
            bingoCell.name = "BingoCell " + bingoCellIndex;
            _bingoCells.Add(bingoCell);
        }

        Debug.Log("Num bingo cells created: " + _bingoCells.Count);
    }

    public int GetFreeSpaceBingoNumber() {
        return _bingoCells[BingoGame.FREE_SPACE_INDEX].BingoNumber;
    }
}
