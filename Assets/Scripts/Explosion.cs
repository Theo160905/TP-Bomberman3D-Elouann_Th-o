using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TimeExplosion());
    }

    private IEnumerator TimeExplosion()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
