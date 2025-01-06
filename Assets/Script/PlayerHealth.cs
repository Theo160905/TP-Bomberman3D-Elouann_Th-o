using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int Health = 3;

    public void AddHealth()
    {
        Health = Mathf.Clamp(Health + 1, 0, 3);

    }

    public void RemoveHealth()
    {
        Health = Mathf.Clamp(Health -1 , 0, 3);
    }

    public void CheckHealth()
    {
        if (Health <= 0)
        {
            Debug.Log(gameObject.name + " a perdu");
        }
    }
}
