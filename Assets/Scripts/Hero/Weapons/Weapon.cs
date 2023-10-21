using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed;

    [SerializeField] protected List<Attribute> _attributes;
    [SerializeField] protected Bullet Bullet;

    protected Animator Animator;

    public Sprite Icon => _icon;
    public int Price => _price;
    public bool IsBuyed => _isBuyed;
    public string Label => _label;
    public float AttackDelay => _attributes[0].Value;
    public int Damage => (int)_attributes[1].Value;

    private void Start()
    {
        Animator = FindObjectOfType<Player>().GetComponent<Animator>();
    }

    public void AddLevelAttribute(int index)
    {
        float currrentValue = _attributes[index].Value;
        float increaseValue = _attributes[index].ValueIncrease;
        currrentValue += increaseValue;
        _attributes[index].Value = currrentValue;
    }

    public abstract void Shoot(Transform shootPoint);

    public abstract int DealDamage();

    public void ShowAttributes(int index, out string name, out float value)
    {
        name = _attributes[index].Name;
        value = _attributes[index].Value;
    }

    public int GetAttributesCount()
    {
        return _attributes.Count;
    }

    public void Buy()
    {
        _isBuyed = true;
    }
}

[System.Serializable]
public class Attribute
{
    public string Name;
    public string NameEnum;
    public float Value;
    public float ValueIncrease;
}
