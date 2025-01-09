using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Experimental.GlobalIllumination;

public class ChangeColorPlayer : MonoBehaviour
{
    MeshRenderer meshrend;

    [SerializeField]
    Light light;

    public Color color;

    private void Start()
    {
        light.color = color;
        meshrend = GetComponent<MeshRenderer>();
        meshrend.material.SetColor("_EmissionColor", color);
        meshrend.material.EnableKeyword("_EMISSION");
    }
}
