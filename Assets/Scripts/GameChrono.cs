using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameChrono : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _inGameChrono;
    public float Timer { get; private set; } = 0;
    private bool _isPlaying;
    public event Action OnResume;

    //Singleton
    #region Singleton
    private static GameChrono _instance;

    public static GameChrono Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("SpawnerBomb");
                _instance = go.AddComponent<GameChrono>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 0f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#eb624d>destroyed</color></b>");
        }
        else
        {
            _instance = this;
            Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#58ed7d>created</color></b>");
        }
    }
    #endregion

    private void Start()
    {
        ResumeTimer();
    }

    public void ResumeTimer()
    {
        _isPlaying = true;
        StartCoroutine(CoroutineResumeTimer());
        OnResume?.Invoke();
    }

    public void PauseTimer()
    {
        _isPlaying = false;
        StopCoroutine(CoroutineResumeTimer());
    }

    public IEnumerator CoroutineResumeTimer()
    {
        while (_isPlaying)
        {
            Timer += 0.01f;
            if(_inGameChrono != null) _inGameChrono.text = String.Format("{0:0.00}", Timer);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
