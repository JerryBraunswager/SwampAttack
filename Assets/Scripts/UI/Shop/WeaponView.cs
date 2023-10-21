using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _sellButton;

    private string _buyed = "Куплено";
    private Weapon _weapon;

    public event UnityAction<Weapon, WeaponView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Weapon weapon)
    {
        _icon.sprite = weapon.Icon;
        _label.text = weapon.Label;
        _price.text = weapon.Price.ToString();
        Sell(weapon);
    }

    public void OnButtonClick()
    {
        SellButtonClick?.Invoke(_weapon, this);
    }

    public void Sell(Weapon weapon)
    {
        _weapon = weapon;

        if (weapon.IsBuyed == true)
        {
            _price.text = _buyed;
        }
    }
}
