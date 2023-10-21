using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private List<Attribute> _attributes;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private int _currentWeaponIndex;
    private int _currentHealth;
    private float _timeBeforeAttack;

    public int Money { get; private set; }
    public int CurrentWeaponIndex => _currentWeaponIndex;
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction MoneyChanged;
    public event UnityAction WeaponChanged;

    private void Start()
    {
        _currentWeapon = _weapons[0];
        _weapons[0].Buy();
        _currentHealth = (int)_attributes[0].Value;
    }

    private void Update()
    {
        _timeBeforeAttack += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) & _currentWeapon.AttackDelay <= _timeBeforeAttack)
        {
            _currentWeapon.Shoot(_shootPoint);
            _timeBeforeAttack = 0;
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, (int)_attributes[0].Value);

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int money)
    {
        Money += money;
        MoneyChanged?.Invoke();
    }

    public void RemoveMoney(int money)
    {
        Money -= money;
        MoneyChanged?.Invoke();
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        _weapons.Add(weapon);
        MoneyChanged?.Invoke();
        WeaponChanged?.Invoke();
    }

    public void Heal(int count)
    {
        _currentHealth += count;

        if(_currentHealth > _attributes[0].Value)
        {
            _currentHealth = (int)_attributes[0].Value;
        }

        HealthChanged?.Invoke(_currentHealth, (int)_attributes[0].Value);
    }

    public void AddLevelAttribute(int index)
    {
        float cur = _attributes[index].Value;
        float increase = _attributes[index].ValueIncrease;
        cur += increase;
        _attributes[index].Value = cur;
    }

    public void ShowAttributes(int index, out string name, out float value)
    {
        value = _attributes[index].Value;
        name = _attributes[index].Name;
    }

    public int GetAttributesCount()
    {
        return _attributes.Count;
    }

    public bool IsFullHealth()
    {
        return _currentHealth == _attributes[0].Value;
    }

    public List<Sprite> GetWeaponsIcons()
    {
        List<Sprite> weaponsIcons = new List<Sprite>();

        for(int i = 0; i < _weapons.Count; i++)
        {
            weaponsIcons.Add(_weapons[i].Icon);
        }

        return weaponsIcons;
    }

    public void NextWeapon()
    {
        _currentWeaponIndex++;
        ChangeWeapon();
    }

    public void PreviousWeapon()
    {
        _currentWeaponIndex--;
        ChangeWeapon();
    }

    private void ChangeWeapon()
    {
        NormalizeNumber();
        _currentWeapon = _weapons[_currentWeaponIndex];
        WeaponChanged?.Invoke();
    }

    private void NormalizeNumber()
    {
        if (_currentWeaponIndex < 0)
        {
            _currentWeaponIndex = _weapons.Count - 1;
        }

        if (_currentWeaponIndex > _weapons.Count - 1)
        {
            _currentWeaponIndex = 0;
        }

    }
}
