using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTrapsController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("e");

        if (collision.gameObject.CompareTag("Coal") || collision.gameObject.CompareTag("Bonus"))
        {
            Destroy(gameObject);
        }
    }

}
