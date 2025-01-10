using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FunctionsDebugingUI : MonoBehaviour
{
    [Header("Vertical Containers")]
    [SerializeField] private GameObject _verticalFunctionsParent;
    [SerializeField] private GameObject _verticalTimesParent;

    [Header("Text objects")]
    [SerializeField] private TextMeshProUGUI _functionDisplayerPrefab;
    [SerializeField] private TextMeshProUGUI _timeDisplayerPrefab;

    [Header("Buttons")]
    [SerializeField] private GameObject _hideButtonText;
    [SerializeField] private GameObject _showButtonText;
    
    // System
    private bool _isShown = true;
    private RectTransform _rectTransform;

    // Singleton
    #region Singleton
    [Header("Singleton")]
    [SerializeField] private bool _hasToDebugWhenCreated;
    [SerializeField] private bool _hasToDebugWhenDestroyed;

    private static FunctionsDebugingUI _instance;

    public static FunctionsDebugingUI Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("FunctionsDebugingUI");
                _instance = go.AddComponent<FunctionsDebugingUI>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            if (_hasToDebugWhenDestroyed) Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 0f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#eb624d>destroyed</color></b>");
        }
        else
        {
            _instance = this;
            if (_hasToDebugWhenCreated) Debug.Log($"<b><color=#{UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f).ToHexString()}>{this.GetType()}</color> instance <color=#58ed7d>created</color></b>");
        }
        _rectTransform = TryGetComponent(out RectTransform rectTransform) ? rectTransform : null;
    }
    #endregion

    public void PositiveFunctionAssert(string functionName, Color color, bool hasToBeDestroyed = false, float duration = 0)
    {
        TextMeshProUGUI newText = Instantiate(_functionDisplayerPrefab, _verticalFunctionsParent.transform);
        TextMeshProUGUI newTime = Instantiate(_timeDisplayerPrefab, _verticalTimesParent.transform);
        newText.text = $"<color=#{color.ToHexString()}>{functionName}</color> <color=#58ed7d>finished</color>";
        newTime.text = String.Format("{0:0.00}", Time.time);
        if (hasToBeDestroyed) StartCoroutine(DeleteDisplayedText(newText.gameObject, newTime.gameObject, duration));
    }

    private IEnumerator DeleteDisplayedText(GameObject text, GameObject time, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(text);
        Destroy(time);
    }

    public async void ShowOrHide()
    {
        if (_isShown)
        {
            this._isShown = false;
            _hideButtonText.SetActive(false);
            _showButtonText.SetActive(true);
            while (_rectTransform.offsetMin.y < 955)
            {
                this._rectTransform.offsetMin += new Vector2(0, (955 - 695) / 50);
                await Task.Delay(1);
            }
        }
        else
        {
            this._isShown = true;
            _showButtonText.SetActive(false);
            _hideButtonText.SetActive(true);
            while (_rectTransform.offsetMin.y > 695)
            {
                this._rectTransform.offsetMin -= new Vector2(0, (955 - 695) / 50);
                await Task.Delay(1);
            }
        }
    }
}

