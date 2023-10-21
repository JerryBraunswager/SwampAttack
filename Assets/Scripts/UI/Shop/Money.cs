using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyLabel;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.MoneyChanged += ChangeLabel;
        ChangeLabel();
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= ChangeLabel;
    }

    private void ChangeLabel()
    {
        _moneyLabel.text = _player.Money.ToString();
    }
}
