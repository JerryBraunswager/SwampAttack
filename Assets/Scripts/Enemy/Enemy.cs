using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected List<Attribute> _startAttributes;

    private List<Attribute> _attributes;
    private float _lastAttackTime;
    private Player _target;
    private Animator _animator;
    private Menu _menu;
    private int _healthIndex = 0;
    private int _damageIndex = 2;
    private int _attackSpeedIndex = 3;

    public int Reward => (int)_attributes[1].Value;
    public event UnityAction<Enemy> Dying;
    public Player Target => _target;

    private bool _isStop;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        SetStartValue();
        _isStop = false;
    }

    public void Init(Player target, Menu menu)
    {
        _target = target;
        _menu = menu;
        _menu.TimeStopped += StopTime;
    }

    public void TakeDamage(int damage)
    {
        _attributes[_healthIndex].Value -= damage;

        if(_attributes[_healthIndex].Value <= 0)
        {
            _menu.TimeStopped -= StopTime;
            Dying?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        if (_isStop == false & _lastAttackTime <= 0)
        {
            _animator.Play("Attack");
            _target.ApplyDamage((int)_attributes[_damageIndex].Value);
            _lastAttackTime = _attributes[_attackSpeedIndex].Value;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    public void SetStartValue()
    {
        List<Attribute> result = new List<Attribute>();

        foreach (Attribute attribute in _startAttributes)
        {
            result.Add(new Attribute(attribute.Name, attribute.NameEnum, attribute.Value, attribute.ValueIncrease));
        }

        _attributes = result;
    }

    public void Move()
    {
        if (_isStop == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _attributes[4].Value * Time.deltaTime);
        }
    }

    private void StopTime(bool stop)
    {
        _isStop = stop;
    }
}
