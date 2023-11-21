using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _count;
    [SerializeField] private TMP_Text _increaseLabel;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _sellButton;

    private string _name;
    private float _value;
    private float _increaseValue;
    private int _index;
    private Weapon _weapon;

    public string Name => _name;
    public event UnityAction<int,Weapon, UpgradeView> SellButtonClicked;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(int price)
    {
        _label.text = _name;
        _count.text = _value.ToString();

        if (_increaseValue == 0)
        {
            _increaseLabel.text = "";
        }
        else
        {
            _increaseLabel.text = _increaseValue.ToString();
        }

        _price.text = price.ToString();
    }

    public void OnButtonClick()
    {
        SellButtonClicked?.Invoke(_index, _weapon, this);
    }

    public void Init(int index, string name, float value, float increaseValue = 0, Weapon weapon = null)
    {
        _index = index;
        _name = name;
        _value = value;
        _increaseValue = increaseValue;
        _weapon = weapon;
    }
}
