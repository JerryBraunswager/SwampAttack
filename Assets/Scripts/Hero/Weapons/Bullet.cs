using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Weapon _weapon;

    private Menu _menu;
    private bool _isStop;

    private void Update()
    {
        if(_isStop == false)
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_weapon.DealDamage());
            _menu.TimeStopped -= StopTime;
            Destroy(gameObject);
        }
    }

    public void Init(Menu menu)
    {
        _menu = menu;
        _menu.TimeStopped += StopTime;
    }

    private void StopTime(bool stop)
    {
        _isStop = stop;
    }
}
