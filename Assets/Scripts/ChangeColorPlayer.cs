using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Experimental.GlobalIllumination;

public class ChangeColorPlayer : MonoBehaviour
{
    MeshRenderer meshrend;

    [SerializeField]
    Light _light;

    public Color color;

    private void Start()
    {
        _light.color = color;
        meshrend = GetComponent<MeshRenderer>();
        meshrend.material.SetColor("_Color", color);
        meshrend.material.EnableKeyword("_EMISSION");
    }
}
