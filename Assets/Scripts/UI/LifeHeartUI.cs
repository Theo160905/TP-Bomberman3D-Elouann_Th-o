using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LifeHeartUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image _leftHalf;
    [SerializeField] private Image _rightHalf;
    [SerializeField] private TrailRenderer _slash;

    [Header("Vector3")]
    private Vector3 _leftHalfBasePos;
    private Vector3 _rightHalfBasePos;
    private Vector3 _slashBasePos;

    [SerializeField] private Vector3 _leftHalfFinalPos;
    [SerializeField] private Vector3 _rightHalfFinalPos;
    [SerializeField] private Vector3 _slashFinalPos;

    [Header("Other")]
    [SerializeField] private Color _damagedColor;


    private Sequence _leftHalfSequence;
    private Sequence _rightHalfSequence;

    private void Start()
    {
        _leftHalfBasePos = _leftHalf.transform.localPosition;
        _rightHalfBasePos = _rightHalf.transform.localPosition;
        _slash.gameObject.SetActive(false);
    }

    public void Juice()
    {
        _leftHalfSequence = DOTween.Sequence();
        _rightHalfSequence = DOTween.Sequence();
        _slash.gameObject.SetActive(true);
        
        _leftHalf.transform.DOLocalMove(_leftHalfFinalPos, 0.6f).SetEase(Ease.OutQuart);
        _leftHalfSequence.Append(_leftHalf.DOColor(_damagedColor, 0.5f)).Append(_leftHalf.transform.DOLocalRotate(Vector3.forward * 15, 0.7f).SetEase(Ease.InOutCubic)).Insert(0.6f, _leftHalf.transform.DOScale(0.8f, 0.5f).SetEase(Ease.OutBounce));
        
        _rightHalf.transform.DOLocalMove(_rightHalfFinalPos, 0.6f).SetEase(Ease.OutQuart);
        _rightHalfSequence.Append(_rightHalf.DOColor(_damagedColor, 0.5f)).Append(_rightHalf.transform.DOLocalRotate(Vector3.back * 15, 0.7f).SetEase(Ease.InOutCubic)).Insert(0.6f, _rightHalf.transform.DOScale(0.8f, 0.5f).SetEase(Ease.OutBounce));

        _slash.transform.DOLocalMove(_slashFinalPos, 0.3f).SetEase(Ease.InOutCubic);
    }

    private void Reset()
    {

    }
}
