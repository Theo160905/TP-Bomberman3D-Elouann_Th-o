using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseBomb : MonoBehaviour
{
    public BombUI _bombUI;

    private Queue<GameObject> bombList = new();
    private bool usebomb = false;

    public void UseBomb(InputAction.CallbackContext context)
    {
        if (bombList.Count > 0)
        {
            if (usebomb) return;
            SpawnerBomb.Instance.spawnCount--;
            StartCoroutine(Wait());
            GameObject bomb = bombList.Dequeue();
            bomb.SetActive(true);
            _bombUI.OnDropJuice(bombList.Count);
            //bombList[0].transform.position = gameObject.transform.position;
            bomb.transform.position = new Vector3(Mathf.RoundToInt(gameObject.transform.position.x), gameObject.transform.position.y - 0.5f, Mathf.RoundToInt(gameObject.transform.position.z));
            bomb.gameObject.GetComponent<Bomb>().ExplodeBomb();
            return;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if (other.TryGetComponent(out Bomb bomb))
            {
                if (!bomb.CanBeRecup | bombList.Count >= 3) return;           
                bombList.Enqueue(other.gameObject);
                other.gameObject.SetActive(false);
                bomb.IsOnMap = false;
                bomb.CanBeRecup = false;
                bomb.Collider.isTrigger = true;
                _bombUI.OnPickUpJuice(bombList.Count - 1);
                return;
            }
        }
    }

    private IEnumerator Wait()
    {
        usebomb = true;
        yield return new WaitForSeconds(0.25f);
        usebomb = false;
    }
}
