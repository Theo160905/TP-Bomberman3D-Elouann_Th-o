using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BombUI : MonoBehaviour
{
    [SerializeField] private List<Image> _imageList;
    [SerializeField] private Color _ifNoBomb;

    public void OnPickUpJuice(int index)
    {
        _imageList[index].DOColor(Color.white, 0.1f);
    }

    public void OnDropJuice(int index)
    {
        _imageList[index].DOColor(_ifNoBomb, 0.1f);
    }
}
