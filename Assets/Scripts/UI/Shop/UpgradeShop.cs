using Newtonsoft.Json.Linq;
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
            SellUpgradeView(upgradeView, index, name, value, increaseValue);
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
                SellUpgradeView(upgradeView, index, name, value, increaseValue);
            }
        }
    }
    
    private void ShowPlayerUpgrades()
    {
        int count = _player.GetAttributesCount();

        for (int i = -1; i < count; i++)
        {
            var template = Instantiate(_upgradeView, _playerContainer.transform);
            template.SellButtonClicked += SellUpgradeButtonClicked;
            _playerUpgrades.Add(template);

            if (i == -1)
            {
                RenderUpgradeView(0, "Лечение", -1, _healValue);
            }
            else
            {
                _player.ShowAttributes(i, out string name, out float value, out float increaseValue);
                RenderUpgradeView(i + 1, name, i, value, increaseValue);
            }
        }
    }

    private void Sell(UpgradeView upgradeView)
    {
        _player.RemoveMoney(_currentPrice);
        _currentPrice += _priceIncrease;
        upgradeView.Render(_currentPrice);
    }

    private void SellUpgradeView(UpgradeView upgradeView, int index, string name, float value, float increaseValue)
    {
        upgradeView.Init(index, name, value, increaseValue);
        Sell(upgradeView);
    }

    private void RenderUpgradeView(int listIndex, string name, int index, float value, float increaseValue = 0)
    {
        _playerUpgrades[listIndex].Init(index, name, value, increaseValue);
        _playerUpgrades[listIndex].Render(_currentPrice);
    }
}
