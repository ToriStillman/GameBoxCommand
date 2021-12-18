using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusController : MonoBehaviour
{
    [SerializeField] private Text bonus;
    [SerializeField] private Text bonus1;
    //[SerializeField] private Text bonusScr;
    [SerializeField] private int score;

    private float bonusPresent;
    private float bonusCoal;
    private int bonusAll;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bonus"))
        {
            AddBonus();
            StartCoroutine(DestroyBonus(other.gameObject));
        }

        if (other.gameObject.CompareTag("Coal"))
        {
            AddCoal();
            StartCoroutine(DestroyBonus(other.gameObject));
        }
    }


    IEnumerator DestroyBonus(GameObject other)
    {
        //Animator anim = other.GetComponentInChildren<Animator>();
        //anim.SetBool("isDie", true);

        MusicController audio = GetComponent<MusicController>();
        audio.BonusMusic();

        yield return new WaitForSeconds(0.001f);
        Destroy(other);
    }

    public void AddBonus()
    {
        bonusPresent += 1;
        //bonusAll = score * bonusPresent;
        bonus.text = bonusPresent.ToString();
        //bonus1.text = bonusAll.ToString();
        //bonusScr.text = ("Bonus " + bonusPresent + " * " + score).ToString();
    }

    public void AddCoal()
    {
        bonusCoal += 1;
        bonus1.text = bonusCoal.ToString();
    }

    public int ScoreBonus()
    {
        return bonusAll;
    }

    public float maxCoal()
    {
        return bonusCoal;
    }

    public float maxPresent()
    {
        return bonusPresent;
    }

    public void CrashBonus()
    {
        int a = Random.Range(1, 3);

        switch (a)
        {
            case 1:
                if (bonusPresent > 0)
                {
                    bonusPresent -= Mathf.Round(bonusPresent * 0.20f);
                }
                else if (bonusCoal > 0)
                {
                    bonusCoal -= Mathf.Round(bonusCoal * 0.20f);
                }
                break;

            case 2:
                if (bonusCoal > 0)
                {
                    bonusCoal -= Mathf.Round(bonusCoal * 0.40f);
                }
                else if (bonusPresent > 0)
                {
                    bonusPresent -= Mathf.Round(bonusPresent * 0.40f);
                }
                break;
        }

        bonus.text = bonusPresent.ToString();
        bonus1.text = bonusCoal.ToString();

    }
}
