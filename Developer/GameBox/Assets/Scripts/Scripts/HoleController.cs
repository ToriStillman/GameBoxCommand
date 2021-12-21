using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField] private GameObject boom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boom.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            var a = other.GetComponent<PlayerController>();
            StartCoroutine(Wait(a, other));
        }
    }

    IEnumerator Wait(PlayerController p, Collider other)
    {
        boom.SetActive(true);
       
        p.StopSpeed();
        yield return new WaitForSeconds(0.6f);
        boom.SetActive(false);
        other.transform.position = new Vector3(other.transform.position.x, 0, other.transform.position.z + 8);

        yield return new WaitForSeconds(1f);
        p.GetSpeed(-2);
        p.NormSpeed();
    }
}
