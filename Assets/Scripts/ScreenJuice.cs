using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenJuice : MonoBehaviour
{
    [SerializeField] private Volume _sceneVolume;

    private Bloom _bloom;
    private ChromaticAberration _chromaticAberration;
    private ColorAdjustments _colorAdjustments;

    //Singleton
    #region Singleton
    private static ScreenJuice _instance;

    public static ScreenJuice Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ScreenJuice");
                _instance = go.AddComponent<ScreenJuice>();
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
        _sceneVolume.profile.TryGet(out _bloom);
        _sceneVolume.profile.TryGet(out _chromaticAberration);
        _sceneVolume.profile.TryGet(out _colorAdjustments);
    }

    public void LilScreenShake()
    {
        Camera.main.DOShakePosition(0.4f, 0.5f, 15, 90);
    }

    public void BigScreenShake()
    {
        Camera.main.DOShakePosition(0.5f, 1, 16, 90);
        StartCoroutine(ChromaticApocalypse());
    }

    private IEnumerator ChromaticApocalypse()
    {
        float timer = 0f;

        while (timer < 0.1f)
        {
            timer += 0.01f;
            float t = timer / 0.1f;
            _chromaticAberration.intensity.Override(Mathf.Lerp(0, 1, t));
            yield return new WaitForSeconds(0.01f);
        }
        timer = 0f;
        while (timer < 0.3f)
        {
            timer += 0.01f;
            float t = timer / 0.3f;
            _chromaticAberration.intensity.Override(Mathf.Lerp(1, 0, t));
            yield return new WaitForSeconds(0.01f);
        }
    }
}
