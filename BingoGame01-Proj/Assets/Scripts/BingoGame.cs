using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

// -----------------
// Bingo Numbers
//
// B:  0 - 15
// I: 16 - 30
// N: 31 - 45
// G: 46 - 60
// O: 61 - 75
// -----------------

// ------------------------
// Bingo Card Cell Indexes
//
//  B   I   N   G   O
//  0   5   10  15  20 
//  1   6   11  16  21
//  2   7   12  17  22
//  3   8   13  18  23
//  4   9   14  19  24
// ------------------------

public class BingoGame : MonoBehaviour
{
    public const int NUM_BINGO_COLS = 5;
    public const int NUM_BINGO_ROWS = 5;
    public const int NUM_BINGO_CELLS = 25;
    public const int FREE_SPACE_INDEX = 12;

    // The Bingo Number picks for the game
    List<int> _bingoNumberPicks;

    void Start()
    {
        InitGame();
    }

    void Update()
    {
        CheckBingoCellMouseClick();
    }

    void CheckBingoCellMouseClick()
    {
        // Handle mouse click on Bingo Cell
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);

            RaycastHit2D raycastHit2D = Physics2D.Raycast(mouseWorldPos, Vector3.forward);
            if(raycastHit2D.collider != null)
            {
                Debug.Log("collider hit " + raycastHit2D.collider.gameObject.name);
            }
        }
    }

    void InitGame()
    {
        // WIP
        //DestroyCurrentBingoCard();
        InitBingoNumbers();
        //InitBingoCard();
        ShuffleBingoNumberPicks();
        //RemoveFreeSpaceNumberFromPicks();
        //DrawPick();
        //StartGameTimer();

        // TODO: 
        // * Handle card complete
        // * Add Timer
        //   * Maybe: Subtract time for incorrect daub
        // * IDEA: Marathon Mode - Blackout as many cards as you can in the given time
    }

    void InitBingoNumbers()
    {
        // Create the Bingo Numbers for each Bingo Letter using the proper ranges
        List<int> bingoNumbersB = InitBingoNumberList(1, 15);
        List<int> bingoNumbersI = InitBingoNumberList(16, 30);
        List<int> bingoNumbersN = InitBingoNumberList(31, 45);
        List<int> bingoNumbersG = InitBingoNumberList(46, 60);
        List<int> bingoNumbersO = InitBingoNumberList(61, 75);

        // Shuffle each Bingo Number List
        ShuffleBingoNumberList(ref bingoNumbersB);
        ShuffleBingoNumberList(ref bingoNumbersI);
        ShuffleBingoNumberList(ref bingoNumbersN);
        ShuffleBingoNumberList(ref bingoNumbersG);
        ShuffleBingoNumberList(ref bingoNumbersO);

        // Only use the first 5 numbers from each shuffled bingo number list (remove 10 elements starting with index 5)
        bingoNumbersB.RemoveRange(5, 10);
        bingoNumbersI.RemoveRange(5, 10);
        bingoNumbersN.RemoveRange(5, 10);
        bingoNumbersG.RemoveRange(5, 10);
        bingoNumbersO.RemoveRange(5, 10);

        //Debug.Log("_bingoNumbersB: " + bingoNumbersB.Count);
        //Debug.Log("_bingoNumbersI: " + bingoNumbersI.Count);
        //Debug.Log("_bingoNumbersN: " + bingoNumbersN.Count);
        //Debug.Log("_bingoNumbersG: " + bingoNumbersG.Count);
        //Debug.Log("_bingoNumbersO: " + bingoNumbersO.Count);

        // Add the numbers to the Bingo Number Picks in order of letters, so each column is filled out with proper values
        _bingoNumberPicks = new List<int>();
        _bingoNumberPicks.AddRange(bingoNumbersB);
        _bingoNumberPicks.AddRange(bingoNumbersI);
        _bingoNumberPicks.AddRange(bingoNumbersN);
        _bingoNumberPicks.AddRange(bingoNumbersG);
        _bingoNumberPicks.AddRange(bingoNumbersO);
    }

    List<int> InitBingoNumberList(int start, int end)
    {
        List<int> bingoNumberList = new List<int>();
        for(int i = start; i <= end; ++i)
        {
            bingoNumberList.Add(i);
        }
        return bingoNumberList;
    }

    void ShuffleBingoNumberList(ref List<int> bingoNumberList)
    {
        bingoNumberList = bingoNumberList.OrderBy(elem => Random.Range(0, int.MaxValue)).ToList();
    }

    void ShuffleBingoNumberPicks()
    {
        ShuffleBingoNumberList(ref _bingoNumberPicks);

        string bingoNumbers = "Shuffled Picks: ";
        foreach(int bingoPick in _bingoNumberPicks)
        {
            bingoNumbers += bingoPick + " ";
        }
        Debug.Log(bingoNumbers);
    }

    void RemoveFreeSpaceNumberFromPicks()
    {
        //_bingoNumberPicks.Remove(_bingoCard.GetFreeSpaceBingoNumber());
    }

    void OnRestartButtonClicked()
    {
        InitGame();
    }

    void OnGameTimeElapsed()
    {
        Debug.Log("GAME TIME ELAPSED!");
    }
}
