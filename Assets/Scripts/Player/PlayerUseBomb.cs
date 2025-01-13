using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerUseBomb : MonoBehaviour
{
    private GameObject[] bombList = new GameObject[3];
    private bool usebomb = false;

    public void UseBomb(InputAction.CallbackContext context)
    {
        if (bombList[0] != null)
        {
            if (usebomb) return;
            StartCoroutine(Wait());
            bombList[0].gameObject.SetActive(true);
            //bombList[0].transform.position = gameObject.transform.position;
            bombList[0].transform.position = new Vector3(Mathf.RoundToInt(gameObject.transform.position.x), gameObject.transform.position.y - 0.5f, Mathf.RoundToInt(gameObject.transform.position.z));
            bombList[0].gameObject.GetComponent<Bomb>().ExplodeBomb();
            bombList[0] = bombList[1];
            bombList[1] = bombList[2];
            bombList[2] = null;
            
            return;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            if (other.GetComponent<Bomb>().CanBeRecup)
            {
                other.GetComponent<Bomb>().CanBeRecup = false;
                for(int i = 0; i < bombList.Length; i++)
                {
                    if (bombList[i] == null)
                    {
                        bombList[i] = other.gameObject;
                        other.gameObject.SetActive(false);
                        Debug.Log(bombList[i]);
                        return;
                    }
                }             
            }
        }
    }

    private IEnumerator Wait()
    {
        usebomb = true;
        yield return new WaitForSeconds(3f);
        usebomb = false;
    }
}
