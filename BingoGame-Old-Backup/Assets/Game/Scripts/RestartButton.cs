using System;
using UnityEngine;

public class RestartButton : MonoBehaviour {
    Action _onRestartButtonClickedCallback;

    void Start() {

    }

    public void Init(Action onRestartButtonClickedCallback) {
        _onRestartButtonClickedCallback = onRestartButtonClickedCallback;
    }

    public void OnButtonClicked() {
        _onRestartButtonClickedCallback();
    }
}
