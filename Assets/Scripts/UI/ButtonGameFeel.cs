using System.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonGameFeel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 _position;
    [SerializeField] private FontGradientAnimation _rainbowFont;


    [Header("Gamefeel")]
    [SerializeField] private Vector3 _scaleOnHover;
    private Vector3 _baseScale;
    [SerializeField] private float _scaleOnHoverSpeed;
    [SerializeField] private float _unscaleSpeed;

    [Space(5)]

    [SerializeField] private Vector3 _slideDirection;
    [SerializeField] private float _slideValue;

    [Space(5)]
    [SerializeField] private Vector2 _wobbleStrength;
    [SerializeField] private Vector2 _wobbleSpeed;

    public static bool _canBeSelected { get; set; }

    #region GameFeel
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_rainbowFont != null) _rainbowFont.IsAnimated = true;
        this.transform.SetAsLastSibling();
        this.transform.DOScale(this.transform.localScale + _scaleOnHover, _scaleOnHoverSpeed);
        this.transform.DOLocalMove(_position + _slideDirection , 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_rainbowFont != null) _rainbowFont.ToggleAnimationOff();
        this.transform.DOScale(_baseScale, _unscaleSpeed);
        this.transform.DOLocalMove(_position, 0.3f);
    }

    public void SelectedGameFeel(int direction)
    {
        this.transform.DOLocalMove(new Vector3(0, 6, 0), 0.8f);
        this.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.8f);
    }

    public void ResetAppearance()
    {

    }
    #endregion

    private void Start()
    {
        _position = transform.localPosition;
        _baseScale = transform.localScale;
    }

    private void Update()
    {
        this.transform.localPosition = new Vector3(_position.x + Mathf.Sin(Time.time * _wobbleSpeed.x) * _wobbleStrength.x, _position.y + Mathf.Sin(Time.time * _wobbleSpeed.y) * _wobbleStrength.y, _position.z);
    }
}