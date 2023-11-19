using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _shopButton;

    private bool _isGameStarted = false;

    private void OnEnable()
    {
        _continueButton.gameObject.SetActive(_isGameStarted);
        _startButton.onClick.AddListener(OnStartButtonClick);
        _shopButton.onClick.AddListener(OnShopButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClick);
        _shopButton.onClick.RemoveListener(OnShopButtonClick);
    }

    public void OnStartButtonClick()
    {
        _menu.ClosePanel(gameObject);
        _isGameStarted = true;
    }

    public void OnShopButtonClick()
    {

    }
}
