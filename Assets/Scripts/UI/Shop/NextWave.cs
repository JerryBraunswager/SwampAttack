using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWave : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _container;
    [SerializeField] private Menu _menu;

    private void OnEnable()
    {
        _spawner.AllEnemyKilled += OnAllEnemyKilled;
        _button.onClick.AddListener(OnNextWaveButtonClick);
    }

    private void OnDisable()
    {
        _spawner.AllEnemyKilled -= OnAllEnemyKilled;
        _button.onClick.RemoveListener(OnNextWaveButtonClick);
    }

    private void OnAllEnemyKilled()
    {
        _menu.OpenPanel(_container);
    }

    public void OnNextWaveButtonClick()
    {
        _spawner.NextWave();
        _menu.ClosePanel(_container);
    }
}
