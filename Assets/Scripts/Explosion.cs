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
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
        
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            print(other.gameObject);
            other.gameObject.GetComponent<PlayerHealth>().RemoveHealth();
        }
    }
}
