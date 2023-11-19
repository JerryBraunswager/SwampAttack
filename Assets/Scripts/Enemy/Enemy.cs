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

    public int Reward => (int)_attributes[1].Value;

    public event UnityAction<Enemy> Dying;

    public Player Target => _target;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        SetStartValue();
    }

    public void Init(Player target)
    {
        _target = target;
    }

    public void TakeDamage(int damage)
    {
        _attributes[0].Value -= damage;

        if(_attributes[0].Value <= 0)
        {
            Dying?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        if (_lastAttackTime <= 0)
        {
            _animator.Play("Attack");
            _target.ApplyDamage((int)_attributes[2].Value);
            _lastAttackTime = _attributes[3].Value;
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
}
