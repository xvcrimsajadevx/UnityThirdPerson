using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int health;

    public event Action OnTakeDamage;

    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int damage)
    {
        if (health == 0) { return; }

        health = Mathf.Max(health - damage, 0);

        OnTakeDamage?.Invoke();

        Debug.Log(health);
    }
}
