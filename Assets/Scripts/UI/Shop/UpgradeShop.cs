using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private WeaponShop _weaponShop;
    [SerializeField] private UpgradeView _upgradeView;
    [SerializeField] private GameObject _weaponContainer;
    [SerializeField] private GameObject _playerContainer;
    [SerializeField] private int _startPrice;
    [SerializeField] private int _priceIncrease;
    [SerializeField] private int _healValue;

    private List<UpgradeView> _weaponUpgrades = new List<UpgradeView>();
    private List<UpgradeView> _playerUpgrades = new List<UpgradeView>();
    private int _currentPrice;
    private int count;

    private void OnEnable()
    {
        _weaponShop.WeaponChoosed += OnWeaponButtonClick;
    }

    private void OnDisable()
    {
        _weaponShop.WeaponChoosed -= OnWeaponButtonClick;
    }

    private void OnApplicationQuit()
    {
        foreach(UpgradeView upgradeView in _weaponUpgrades)
        {
            upgradeView.SellButtonClicked -= SellUpgradeButtonClicked;
        }

        foreach (UpgradeView upgradeView in _playerUpgrades)
        {
            upgradeView.SellButtonClicked -= SellUpgradeButtonClicked;
        }
    }

    private void Start()
    {
        _currentPrice = _startPrice;
        ShowPlayerUpgrades();
    }

    private void OnWeaponButtonClick(Weapon weapon)
    {
        foreach (UpgradeView upgradeView in _weaponUpgrades)
        {
            if (upgradeView != null)
            {
                upgradeView.gameObject.SetActive(false);
            }
        }

        count = weapon.GetAttributesCount();

        for(int i = 0; i < count; i++)
        {
            if (_weaponUpgrades.Count < count)
            {
                var template = Instantiate(_upgradeView, _weaponContainer.transform);
                template.SellButtonClicked += SellUpgradeButtonClicked;
                _weaponUpgrades.Add(template);
            }

            weapon.ShowAttributes(i, out string name, out float value, out float increaseValue);
            _weaponUpgrades[i].Init(i, name, value, increaseValue, weapon);
            _weaponUpgrades[i].Render(_currentPrice);
            _weaponUpgrades[i].gameObject.SetActive(true);
        }
    }

    private void SellUpgradeButtonClicked(int index, Weapon weapon, UpgradeView upgradeView)
    {
        if(_weaponUpgrades.Contains(upgradeView))
        {
            weapon.AddLevelAttribute(index);
            weapon.ShowAttributes(index, out string name, out float value, out float increaseValue);
            upgradeView.Init(index, name, value, increaseValue);
            Sell(upgradeView);
        }
        else
        {
            if (index == -1)
            {
                if (!_player.IsFullHealth())
                {
                    _player.Heal(_healValue);
                    upgradeView.Init(-1, "Лечение", _healValue);
                    Sell(upgradeView);
                }
            }
            else
            {
                _player.AddLevelAttribute(index);
                _player.ShowAttributes(index, out string name, out float value, out float increaseValue);
                upgradeView.Init(index, name, value, increaseValue);
                Sell(upgradeView);
            }
        }
    }
    
    private void ShowPlayerUpgrades()
    {
        int count = _player.GetAttributesCount();
        var healTemplate = Instantiate(_upgradeView, _playerContainer.transform);
        healTemplate.SellButtonClicked += SellUpgradeButtonClicked;
        _playerUpgrades.Add(healTemplate);
        _playerUpgrades[0].Init(-1, "Лечение", _healValue);
        _playerUpgrades[0].Render(_currentPrice);

        for (int i = 0; i < count; i++)
        {
            var template = Instantiate(_upgradeView, _playerContainer.transform);
            template.SellButtonClicked += SellUpgradeButtonClicked;
            _playerUpgrades.Add(template);
            _player.ShowAttributes(i, out string name, out float value, out float increaseValue);
            _playerUpgrades[i + 1].Init(i, name, value, increaseValue);
            _playerUpgrades[i + 1].Render(_currentPrice);
        }
    }

    private void Sell(UpgradeView upgradeView)
    {
        _player.RemoveMoney(_currentPrice);
        _currentPrice += _priceIncrease;
        upgradeView.Render(_currentPrice);
    }
}
