using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int Health { get; private set;}

    private bool IsInvulnerable = false;

    [SerializeField] private List<LifeHeartUI> _lifeHeartJuice;

    private void Awake()
    {
        Health = 3;
    }

    public void AddHealth()
    {
        Health = Mathf.Clamp(Health + 1, 0, 3);
    }

    public void RemoveHealth()
    {
        if (IsInvulnerable) return;
        Health = Mathf.Clamp(Health -1 , 0, 3);
        StartCoroutine(TimeInvulnerable());
        CheckHealth();
        _lifeHeartJuice[Health].Juice();
    }

    public void CheckHealth()
    {
        if (Health <= 0)
        {
            Debug.Log(gameObject.name + " a perdu");
        }
    }

    public IEnumerator TimeInvulnerable()
    {
        IsInvulnerable = true;
        yield return new WaitForSeconds(3f);
        IsInvulnerable = false;
    }
}
