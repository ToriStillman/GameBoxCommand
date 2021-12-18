using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private GameObject buttonMenu;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            gameObject.GetComponent<PlayerController>().GetSpeed(-1f);
            gameObject.GetComponent<BonusController>().CrashBonus();
            var rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.back * 20, ForceMode.VelocityChange);
            Destroy(other.gameObject);

            MusicController audio = GetComponent<MusicController>();
            audio.HitMusic();


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
