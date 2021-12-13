using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private GameObject buttonMenu;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            gameObject.GetComponent<PlayerController>().GetSpeed(-1f);
            gameObject.GetComponent<BonusController>().CrashBonus();
            var rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.back * 20, ForceMode.VelocityChange);
            Destroy(collision.gameObject);

            //gameObject.GetComponent<PlayerController>().StartDeath();
            //MusicController audio = GetComponent<MusicController>();
            //audio.DieMusic();

            //StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        buttonMenu.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<DistanceController>().LoadAllDis();
        gameObject.GetComponent<DieController>().DeathScreen();
    }
}
