using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private string _workLabel;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed;

    [SerializeField] protected List<Attribute> _attributes;
    [SerializeField] protected Bullet Bullet;

    protected int MinChance = 0;
    protected int MaxChance = 101;

    public Sprite Icon => _icon;
    public int Price => _price;
    public bool IsBuyed => _isBuyed;
    public string Label => _label;
    public float AttackDelay => _attributes[0].Value;
    public int Damage => (int)_attributes[1].Value;
    public float CritChance => _attributes[2].Value;
    public float CritDamage => _attributes[3].Value;
    public string WorkLabel => _workLabel;

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
