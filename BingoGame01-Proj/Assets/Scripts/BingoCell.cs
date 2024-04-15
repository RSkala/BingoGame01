using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class BingoCell : MonoBehaviour
{
    [SerializeField] GameObject _bingoNumber;
    [SerializeField] GameObject _daub;

    public int BingoNumber { get; private set; }
    bool IsFreeSpace { get { return _bingoCellIndex == BingoGame.FREE_SPACE_INDEX; } }

    int _bingoCellIndex;
    UnityAction<BingoCell> _onBingoCellClickedCallback;

    // The collider is used for clicking a BingoCell
    Collider2D _collider2D;

    public void Init(int bingoCellIndex, int bingoNumber, UnityAction<BingoCell> onBingoCellClickedCallback)
    {
        // Get the collider here, as Start may not have been called yet
        _collider2D = GetComponent<Collider2D>();
        Assert.IsTrue(_collider2D != null);
        
        _bingoCellIndex = bingoCellIndex;
        BingoNumber = bingoNumber;
        _onBingoCellClickedCallback = onBingoCellClickedCallback;
        
        // Get the image according to the bingo number
        string bingoNumberSpritePath = string.Format("BingoNumbers/{0}", BingoNumber);
        Sprite bingoNumberSprite = Resources.Load<Sprite>(bingoNumberSpritePath);

        // Set the Bingo Number sprite
        SpriteRenderer bingoNumberSpriteRenderer = _bingoNumber.GetComponent<SpriteRenderer>();
        bingoNumberSpriteRenderer.sprite = bingoNumberSprite;

        // Show the BingoNumber, hide the Daub marker, and enable input
        _bingoNumber.SetActive(true);
        _daub.SetActive(false);
        _collider2D.enabled = true;

        // For the Free Space, mark both the BingoNumber and the Daub hidden (this will show the center star) and disable input
        if(IsFreeSpace)
        {
            _bingoNumber.SetActive(false);
            _collider2D.enabled = false;
        }
    }

    public void OnBingoCellClicked()
    {
        _onBingoCellClickedCallback(this);
    }

    public void DaubCell()
    {
        // Hide the Bingo Number in this cell
        _bingoNumber.SetActive(false);

        // Show the Daub marker in this cell
        _daub.SetActive(true);

        // Disable the collider so it takes no more input
        _collider2D.enabled = false;
    }
}
