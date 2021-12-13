using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject dieEffect;
    [SerializeField] private GameObject startTextMenu;
    [SerializeField] private float speedMove;
    [SerializeField] private float speedJump;
    [SerializeField] private int maxRoads;
    [SerializeField] private Text startText;
    [SerializeField] private Text speedText;

    private Rigidbody rb;
    private Animator anim;

    private float speedZero = 0;
    private float speedNorm;
    private float verticalMove = 0;
    private float targetPos = 0;

    private int roads = 0;

    private bool isGrounded;
    private bool leftPos;
    private bool rightPos;
    private bool isDie;
    private bool onSpeed = false;
    private bool isTrampoline;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        var res = GetComponent<DistanceController>();
        res.LoadMAxDis();
    }

    void Start()
    {
        StartCoroutine(Wait());
        targetPos = 0;
        transform.position = new Vector3(targetPos, 0, 0);
        isGrounded = false;
    }

    void LateUpdate()
    {
        if (transform.position.y > 1 && isTrampoline)
        {
            rb.AddForce(Vector3.forward * 2, ForceMode.Impulse);
            rb.useGravity = true;
        }

        PlayerMove();

        speedText.text = speedNorm.ToString("0.00");

        if (transform.position.x >= targetPos && rightPos)
        {
            verticalMove = 0;
            rightPos = false;
            transform.position = new Vector3(targetPos, transform.position.y, transform.position.z);
        }

        if (transform.position.x <= targetPos && leftPos)
        {
            verticalMove = 0;
            leftPos = false;
            transform.position = new Vector3(targetPos, transform.position.y, transform.position.z);
        }

        if (speedNorm < 15 && onSpeed)
        {
            GetSpeed(0.003f);
        }

    }

    public void PlayerMove()
    {
        transform.Translate(verticalMove, 0, speedNorm * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            LeftMove();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            RightMove();
        }
    }

    public void LeftMove()
    {
        if (roads > -((maxRoads - 1) / 2))
        {
            verticalMove = -speedNorm * Time.fixedDeltaTime * 1.1f;
            targetPos -= 2;
            leftPos = true;
            roads -= 1;
        }
    }

    public void RightMove()
    {
        if (roads < (maxRoads - 1) / 2)
        {
            verticalMove = speedNorm * Time.fixedDeltaTime * 1.1f;
            targetPos += 2;
            rightPos = true;
            roads += 1;
        }
    }

    public void Jump()
    {
        if (isGrounded && !isDie)
        {
            rb.AddForce(Vector3.up * (speedJump * 100));
            MusicController audio = GetComponent<MusicController>();
            audio.JumpMusic();
        }
    }

    public void Trampoline()
    {
        if (!isDie)
        {
            isGrounded = true;
            rb.useGravity = false;
            isTrampoline = true;

            rb.AddForce(Vector3.up * (speedJump * 130));
            rb.AddForce(Vector3.forward * (speedJump * 2), ForceMode.Impulse);
            MusicController audio = GetComponent<MusicController>();
            audio.JumpMusic();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isTrampoline = false;
        rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trampoline"))
        {
            Trampoline();
        }
        if (collision.gameObject.CompareTag("Road"))
        {
            isTrampoline = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road") && !isDie)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Road") && !isDie)
        {
            isGrounded = false;
        }
    }

    public void GetSpeed(float speedNew)
    {
        if (speedNorm < 5)
        {
            speedNorm += 0.002f;
        }
        else
        {
            speedNorm += speedNew;
            speedMove = speedNorm;
        }

        onSpeed = true;
    }

    public void NormSpeed()
    {
        speedNorm = speedMove;
        onSpeed = true;
    }

    public void StopSpeed()
    {
        speedNorm = speedZero;
        onSpeed = false;
    }

    public void StartDeath()
    {
        isDie = true;
        isGrounded = false;
        dieEffect.SetActive(true);
        onSpeed = false;
    }

    public int MaxRoads()
    {
        return maxRoads;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        startTextMenu.SetActive(true);
        startText.text = "3";
        yield return new WaitForSeconds(0.7f);
        startText.text = "2";
        yield return new WaitForSeconds(0.7f);
        startText.text = "1";
        yield return new WaitForSeconds(0.7f);
        startText.text = "Вперед!";
        yield return new WaitForSeconds(0.4f);
        startTextMenu.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        NormSpeed();
        onSpeed = true;
    }

}
