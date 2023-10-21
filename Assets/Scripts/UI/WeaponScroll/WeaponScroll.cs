using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScroll : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _currentWeapon;
    [SerializeField] private Image _previousWeapon;
    [SerializeField] private Image _nextWeapon;

    private List<Sprite> _weaponIcons;
    private int _currentWeaponIndex;
    private int _previousWeaponIndex;
    private int _nextWeaponIndex;

    private void Start()
    {
        GetWeapons();
    }

    private void OnEnable()
    {
        _player.WeaponChanged += GetWeapons;
    }

    private void OnDisable()
    {
        _player.WeaponChanged -= GetWeapons;
    }

    private void GetWeapons()
    {
        _weaponIcons = _player.GetWeaponsIcons();
        _currentWeaponIndex = _player.CurrentWeaponIndex;
        _previousWeaponIndex = NormalizeNumber(_currentWeaponIndex - 1, _weaponIcons.Count - 1);
        _nextWeaponIndex = NormalizeNumber(_currentWeaponIndex + 1, _weaponIcons.Count - 1);
        _currentWeapon.sprite = _weaponIcons[_currentWeaponIndex];
        _previousWeapon.sprite = _weaponIcons[_previousWeaponIndex];
        _nextWeapon.sprite = _weaponIcons[_nextWeaponIndex];
    }

    private int NormalizeNumber(int number, int maxNumber)
    {
        int result = number;

        if(result < 0)
        {
            result = maxNumber;
        }

        if(result > maxNumber)
        {
            result = 0;
        }

        return result;
    }
}
