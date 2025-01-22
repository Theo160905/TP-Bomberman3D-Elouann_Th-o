using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public AudioClip clip;
    public AudioClip DeathClip;

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
        SoundManager.Instance.PlaySound(clip);
        StartCoroutine(TimeInvulnerable());

        _lifeHeartJuice[Health].DamageJuice();
        if (Health == 1) StartCoroutine(_lifeHeartJuice[0].CriticalJuice());

        if(Health <= 0)
        {
            Death();
        }
    }

    public IEnumerator TimeInvulnerable()
    {
        IsInvulnerable = true;
        yield return new WaitForSeconds(3f);
        IsInvulnerable = false;
    }

    public async void Death()
    {
        Time.timeScale = 0.25f;
        MeshDestroy.DestroyMesh();
        SoundManager.Instance.PlayMusic(DeathClip);
        SoundManager.Instance._musicSource.loop = false;
        await Task.Delay(2000);
        Time.timeScale = 1f;
        
    }
}
