using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

public class BingoGame : MonoBehaviour {
    public const int NUM_BINGO_COLS = 5;
    public const int NUM_BINGO_ROWS = 5;
    public const int NUM_BINGO_CELLS = 25;
    public const int FREE_SPACE_INDEX = 12;

    const float GAME_TIME = 30.0f;

    [SerializeField] Transform canvasTransform = null;
    [SerializeField] GameObject bingoCardPrefab = null;
    [SerializeField] GameObject bingoCellPrefab = null;
    [SerializeField] Text currentNumberPickText = null;
    [SerializeField] RestartButton restartButton = null;
    [SerializeField] GameTimer gameTimer = null;

    // The Bingo Card that is being played
    BingoCard _bingoCard;

    // The Bingo Number picks for the game
    List<int> _bingoNumberPicks;

    // Current Number pick
    int _currentNumberPick;

    void Awake() {
        currentNumberPickText.text = "";
        restartButton.Init(OnRestartButtonClicked);
    }

    void Start() {
        InitGame();
    }

    void Update() {

    }

    void DestroyCurrentBingoCard() {
        if(_bingoCard != null) {
            Destroy(_bingoCard.gameObject);
        }
    }

    void InitGame() {
        DestroyCurrentBingoCard();
        InitBingoNumbers();
        InitBingoCard();
        ShuffleBingoNumberPicks();
        RemoveFreeSpaceNumberFromPicks();
        DrawPick();
        StartGameTimer();

        // TODO: 
        // * Handle card complete
        // * Add Timer
        // * Maybe: Subtract time for incorrect daub
        //
        // * TRY! Marathon Mode! Blackout as many cards as you can!
    }

    void InitBingoNumbers() {
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

        // Shuffle the bingo number lists then only use the first 5 from each (remove 10 elements starting with index 5)
        bingoNumbersB.RemoveRange(5, 10);
        bingoNumbersI.RemoveRange(5, 10);
        bingoNumbersN.RemoveRange(5, 10);
        bingoNumbersG.RemoveRange(5, 10);
        bingoNumbersO.RemoveRange(5, 10);

        Debug.Log("_bingoNumbersB: " + bingoNumbersB.Count);
        Debug.Log("_bingoNumbersI: " + bingoNumbersI.Count);
        Debug.Log("_bingoNumbersN: " + bingoNumbersN.Count);
        Debug.Log("_bingoNumbersG: " + bingoNumbersG.Count);
        Debug.Log("_bingoNumbersO: " + bingoNumbersO.Count);

        // Add the numbers to the Bingo Number Picks in order of letters, so each column is filled out with proper values
        _bingoNumberPicks = new List<int>();
        _bingoNumberPicks.AddRange(bingoNumbersB);
        _bingoNumberPicks.AddRange(bingoNumbersI);
        _bingoNumberPicks.AddRange(bingoNumbersN);
        _bingoNumberPicks.AddRange(bingoNumbersG);
        _bingoNumberPicks.AddRange(bingoNumbersO);
    }

    List<int> InitBingoNumberList(int start, int end) {
        List<int> bingoNumberList = new List<int>();
        for(int i = start; i <= end; ++i) {
            bingoNumberList.Add(i);
        }
        return bingoNumberList;
    }

    void ShuffleBingoNumberList(ref List<int> bingoNumberList) { // Note: Why do I need a "ref" here? Doesnt work otherwise
        bingoNumberList = bingoNumberList.OrderBy(elem => Random.Range(0, int.MaxValue)).ToList();
    }

    void InitBingoCard() {
        _bingoCard = Instantiate(bingoCardPrefab, canvasTransform).GetComponent<BingoCard>();
        _bingoCard.Init(bingoCellPrefab, _bingoNumberPicks, OnBingoCellClicked);
    }

    void ShuffleBingoNumberPicks() {
        ShuffleBingoNumberList(ref _bingoNumberPicks);

        string bingoNumbers = "Shuffled Picks: ";
        foreach(int bingoPick in _bingoNumberPicks) {
            bingoNumbers += bingoPick + " ";
        }
        Debug.Log(bingoNumbers);
    }

    void DrawPick() {
        if(_bingoNumberPicks.Count == 0) {
            Debug.Log("Out of Picks!");
            return;
        }

        // Pull from the front
        _currentNumberPick = _bingoNumberPicks[0];
        _bingoNumberPicks.RemoveAt(0);

        currentNumberPickText.text = _currentNumberPick.ToString();
    }

    void StartGameTimer() {
        gameTimer.StartTimer(GAME_TIME, OnGameTimeElapsed);
    }

    void OnBingoCellClicked(BingoCell clickedBingoCell) {
        Debug.Log("OnBingoCellClicked - clickedBingoCell: " + clickedBingoCell.BingoNumber);
        if(_currentNumberPick == clickedBingoCell.BingoNumber) {
            clickedBingoCell.DaubCell();
            DrawPick();
        }
    }

    void RemoveFreeSpaceNumberFromPicks() {
        _bingoNumberPicks.Remove(_bingoCard.GetFreeSpaceBingoNumber());
    }

    void OnRestartButtonClicked() {
        InitGame();
    }

    void OnGameTimeElapsed() {
        Debug.Log("GAME TIME ELAPSED!");
    }
}
