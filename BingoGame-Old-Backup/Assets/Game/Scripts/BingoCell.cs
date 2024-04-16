using System;
using UnityEngine;
using UnityEngine.UI;

public class BingoCell : MonoBehaviour {
    [SerializeField] Text bingoNumberText = null;
    [SerializeField] Button button = null;

    public int BingoNumber { get; private set; }
    bool IsFreeSpace { get { return _bingoCellIndex == BingoGame.FREE_SPACE_INDEX; } }

    int _bingoCellIndex;
    Action<BingoCell> _onBingoCellClickedCallback;

    void Start() {

    }

    public void Init(int bingoCellIndex, int bingoNumber, Action<BingoCell> onBingoCellClickedCallback) {
        BingoNumber = bingoNumber;
        _bingoCellIndex = bingoCellIndex;
        name = "BingoCell " + _bingoCellIndex;
        bingoNumberText.text = BingoNumber.ToString();
        _onBingoCellClickedCallback = onBingoCellClickedCallback;

        if(IsFreeSpace) {
            DaubCell();
        }
    }

    public void OnButtonClicked() {
        Debug.Log("BingoCell clicked - " + name + " = " + BingoNumber);
        _onBingoCellClickedCallback(this);
    }

    public void DaubCell() {
        bingoNumberText.text = "X";
        bingoNumberText.color = Color.red;
        button.enabled = false;
    }
}
