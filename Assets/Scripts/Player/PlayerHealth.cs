using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int Health = 3;

    private bool IsInvulnerable = false;

    [SerializeField] private List<LifeHeartUI> _lifeHeartJuice;

    public void AddHealth()
    {
        Health = Mathf.Clamp(Health + 1, 0, 3);
    }

    public void RemoveHealth()
    {
        if (IsInvulnerable) return;
        Health--;
        StartCoroutine(TimeInvulnerable());
        CheckHealth();

        _lifeHeartJuice[Health].Juice();
    }

    public void CheckHealth()
    {
        if (Health <= 0)
        {
            Debug.Log(gameObject.name + " a perdu");
            Application.Quit();
            EditorApplication.isPlaying = false;
        }
    }

    public IEnumerator TimeInvulnerable()
    {
        IsInvulnerable = true;
        yield return new WaitForSeconds(3f);
        IsInvulnerable = false;
    }
}
