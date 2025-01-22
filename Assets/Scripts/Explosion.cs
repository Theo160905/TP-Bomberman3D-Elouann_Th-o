using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TimeExplosion());
    }

    private IEnumerator TimeExplosion()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
        
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            other.gameObject.GetComponent<PlayerHealth>().RemoveHealth();
        }
    }
}
