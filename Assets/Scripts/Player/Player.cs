using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public CharacterController controller;
     Animator anim;
    public GameObject enemy;
    public Camera playerCamera;
    public Transform groundCheck;
    public AudioSource mainAudioSource;
    public Handgun handgun;
    public RifleScript rifle;
    public WeaponSwitch weapon;

    public GameObject impactEffect;

    public AudioClip walkSound;
    public AudioClip jumpSound;
    public Text ammoUI;
    public Text death;


    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float groundDistance = 0.4f;

    int currWeapon = 1;
    public LayerMask groundMask;
    Vector3 velocity;

   public bool isGrounded;
    public bool isRunning;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        mainAudioSource.loop = true;
        weapon = GetComponentInChildren<WeaponSwitch>();
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("MainScene");
                Time.timeScale = 1;
            }
            return;
        }


        WeaponSwitched();
        UpdateAnimatorParameters();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        HandleMovement();
        PlayWalkSounds();

        if(currWeapon==1)
        {
            ammoUI.text = rifle.currentAmmo.ToString() + "/" + rifle.maxAmmo.ToString();
        }
        else
        {
            ammoUI.text = handgun.currentAmmo.ToString() + "/" + handgun.maxAmmo.ToString();
        }
        



        if (gameObject.GetComponent<HealthStatus>().currentHP <= 0)
            Death();


        if (Input.GetKeyDown(KeyCode.F))
            Instantiate(enemy);


    }

    void HandleMovement()
    {
        if (isGrounded && velocity.y < 0)
            velocity.y = -3f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift)
                && (Input.GetKey(KeyCode.W)
                || Input.GetKey(KeyCode.A)
                || Input.GetKey(KeyCode.D)
                || Input.GetKey(KeyCode.S)))
        {

            speed = 20f;
            isRunning = true;
        }

        else
        {

            speed = 12f;
            isRunning = false;
        }

        Vector3 move = transform.right * x * speed +
            transform.forward * z * speed + transform.up * velocity.y;

        controller.Move(move * Time.deltaTime);


        if (Input.GetButtonDown("Jump") & isGrounded)
        {
            mainAudioSource.clip = jumpSound;
            mainAudioSource.Play();
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }


    void WeaponSwitched()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currWeapon = 1;
            anim = GetComponentInChildren<Animator>();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currWeapon = 2;
            anim = GetComponentInChildren<Animator>();
        }
    }

    private void PlayWalkSounds()
    {
        if (isGrounded && (Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D)))
        {
            mainAudioSource.clip = walkSound;
            if (!mainAudioSource.isPlaying)
                mainAudioSource.Play();
        }
        else if (mainAudioSource.isPlaying)
            mainAudioSource.Pause();
    }


    void UpdateAnimatorParameters()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)
                                    || Input.GetKey(KeyCode.S)
                                    || Input.GetKey(KeyCode.D))
            anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk", false);


        if (isRunning == true)
            anim.SetBool("Run", true);
        else
            anim.SetBool("Run", false);

    }
    void Death()
    {
        death.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}