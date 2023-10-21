using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilledBar : Bar
{
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.EnemyKilled += OnValueChanged;
        Slider.value = 0;
    }

    private void OnDisable()
    {
        _spawner.EnemyKilled -= OnValueChanged;
    }
}
