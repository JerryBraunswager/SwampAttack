using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private List<Attribute> _startAttributes;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Menu _menu;
    [SerializeField] private StartMenu _startMenu;

    private List<Attribute> _attributes;
    private Weapon _currentWeapon;
    private int _currentWeaponIndex;
    private int _currentHealth;
    private float _timeBeforeAttack;
    private Animator _animator;
    private bool _isStop;
    private int _healthIndex = 0;

    public int Money { get; private set; }
    public int CurrentWeaponIndex => _currentWeaponIndex;
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction MoneyChanged;
    public event UnityAction WeaponChanged;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartNewGame();
    }

    private void Update()
    {
        if (_isStop == false)
        {
            _timeBeforeAttack += Time.deltaTime;

            if (Input.GetMouseButtonDown(0) & _currentWeapon.AttackDelay <= _timeBeforeAttack)
            {
                _animator.Play(_currentWeapon.WorkLabel);
                _currentWeapon.Shoot(_shootPoint, _menu);
                _timeBeforeAttack = 0;
            }
        }
    }

    private void OnEnable()
    {
        _menu.TimeStopped += TimeStop;
        _startMenu.GameStarted += StartGame;
    }

    private void OnDisable()
    {
        _menu.TimeStopped -= TimeStop;
        _startMenu.GameStarted -= StartGame;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, (int)_attributes[_healthIndex].Value);

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
        RemoveMoney(weapon.Price);
        _weapons.Add(weapon);
        WeaponChanged?.Invoke();
    }

    public void Heal(int count)
    {
        _currentHealth += count;

        if(_currentHealth > _attributes[_healthIndex].Value)
        {
            _currentHealth = (int)_attributes[_healthIndex].Value;
        }

        HealthChanged?.Invoke(_currentHealth, (int)_attributes[_healthIndex].Value);
    }

    public void AddLevelAttribute(int index)
    {
        float cur = _attributes[index].Value;
        float increase = _attributes[index].ValueIncrease;
        cur += increase;
        _attributes[index].Value = cur;
        HealthChanged?.Invoke(_currentHealth, (int)_attributes[_healthIndex].Value);
    }

    public void ShowAttributes(int index, out string name, out float value, out float increaseValue)
    {
        value = _attributes[index].Value;
        name = _attributes[index].Name;
        increaseValue = _attributes[index].ValueIncrease;
    }

    public int GetAttributesCount()
    {
        return _attributes.Count;
    }

    public bool IsFullHealth()
    {
        return _currentHealth == _attributes[_healthIndex].Value;
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

    private void SetStartValue()
    {
        List<Attribute> result = new List<Attribute>();

        foreach (Attribute attribute in _startAttributes)
        {
            result.Add(new Attribute(attribute.Name, attribute.NameEnum, attribute.Value, attribute.ValueIncrease));
        }

        _attributes = result;
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

    private void TimeStop(bool stop)
    {
        _isStop = stop;
    }

    private void StartNewGame()
    {
        SetStartValue();
        Money = 0;
        _currentWeapon = _weapons[0];
        _weapons[0].Buy();
        Heal((int)_attributes[0].Value);
    }

    private void StartGame()
    {
        _weapons.RemoveRange(1, _weapons.Count - 1);
        StartNewGame();   
    }
}
