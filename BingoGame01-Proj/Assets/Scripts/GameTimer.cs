using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] Text timerText = null;

    bool _timerStarted = false;
    UnityAction _onGameTimeElapsedCallback;
    float _startTime = 0.0f;
    float _gameTime = 0.0f;

    void Awake()
    {
        _timerStarted = false;
        _startTime = 0.0f;
        InitTimeText();
    }

    void InitTimeText()
    {
        timerText.text = "";
    }

    void StopTimer()
    {
        _timerStarted = false;
    }

    public void StartTimer(float gameTime, UnityAction onGameTimeElapsedCallback)
    {
        _onGameTimeElapsedCallback = onGameTimeElapsedCallback;
        _timerStarted = true;
        _startTime = Time.time;
        _gameTime = gameTime;
    }

    void Update()
    {
        UpdateTime();
    }

    void UpdateTime()
    {
        if(!_timerStarted)
        {
            return;
        }

        float timeElapsed = Time.time - _startTime;
        float timeRemaining = Mathf.Max(0.0f, _gameTime - timeElapsed);
        UpdateTimeText(timeRemaining);

        if(timeRemaining <= 0.0f)
        {
            StopTimer();
            _onGameTimeElapsedCallback();
        }
    }

    void UpdateTimeText(float time)
    {
        time = Mathf.Ceil(time);
        //Debug.Log("time: " + time);
        timerText.text = ((int)time).ToString();
    }
}
