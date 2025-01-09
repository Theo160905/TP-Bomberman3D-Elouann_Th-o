using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool CanBeRecup = true;

    private void Update()
    {
        
    }

    public void ExplodeBomb()
    {
        CanBeRecup = false;
    }

    public IEnumerator TimeToExplode()
    {
        yield return new WaitForSeconds(3);
    }
}
