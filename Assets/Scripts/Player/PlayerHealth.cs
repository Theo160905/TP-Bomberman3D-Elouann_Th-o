using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int Health { get; private set; }

    private bool IsInvulnerable = false;

    [SerializeField] private List<LifeHeartUI> _lifeHeartJuice;

    MeshDestroy MeshDestroy;

    private void Awake()
    {
        Health = 3;
        MeshDestroy = GetComponent<MeshDestroy>();
    }

    public void AddHealth()
    {
        Health = Mathf.Clamp(Health + 1, 0, 3);
    }

    public void RemoveHealth()
    {
        if (IsInvulnerable) return;
        Health--;
        StartCoroutine(TimeInvulnerable());

        _lifeHeartJuice[Health].DamageJuice();
        if (Health == 1) StartCoroutine(_lifeHeartJuice[0].CriticalJuice());
        _lifeHeartJuice[Health].Juice();

        if(Health <= 0)
        {
            StartCoroutine(Death());
        }
    }

    public IEnumerator TimeInvulnerable()
    {
        IsInvulnerable = true;
        yield return new WaitForSeconds(3f);
        IsInvulnerable = false;
    }

    public IEnumerator Death()
    {
        Time.timeScale = 0.25f;
        MeshDestroy.DestroyMesh();
        yield return new WaitForSeconds(3f);
        Time.timeScale = 1f;
    }
}
