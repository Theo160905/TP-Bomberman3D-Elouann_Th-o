using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleAnimationImage : MonoBehaviour
{
    private Vector3 _basePosition;
    private Vector3 _position;
    [SerializeField] private Vector3 _scaleRise;
    [Space(5)]
    [SerializeField] private Vector2 _wobbleStrength;
    [SerializeField] private Vector2 _wobbleSpeed;

    [SerializeField] private float _timer;
    private float timer;

    void Start()
    {
        _basePosition = transform.localPosition;
        _position = transform.localPosition;
        timer = _timer;
    }

    private void Update()
    {
        timer -= 0.01f;
        this.transform.localPosition = new Vector3(_position.x + Mathf.Sin(Time.time * _wobbleSpeed.x) * _wobbleStrength.x, _position.y + Mathf.Sin(Time.time * _wobbleSpeed.y) * _wobbleStrength.y, _position.z);
        if (timer <= 0)
        {
            this.transform.DOPunchScale(_scaleRise, 0.5f, 1, 1);
            timer = _timer;
        }
    }
}
