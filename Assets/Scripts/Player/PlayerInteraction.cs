using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private int rayLenght = 8;
    private string interactableTag = "InteractiveObject";
    private DoorScript door;
    private CollectibleScript coll;
    public Image crosshair;
    public Text pickTxt;
    public Text scoreTxt;

    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        crosshair.color = Color.white;

        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLenght))
        {
            if (hit.collider.CompareTag(interactableTag))
            {

                crosshair.color = Color.red;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (door = hit.collider.gameObject.GetComponent<DoorScript>())
                    {
                        door.PlayAnimation();
                    }
                    if (coll = hit.collider.gameObject.GetComponent<CollectibleScript>())
                    {
                        Animator anim = pickTxt.gameObject.GetComponent<Animator>();
                        anim.Play("FadeIn");
                        coll.CollectItem(hit);
                        CalculateScore();
                    }
                }
            }

        }

    }

    void CalculateScore()
    {
        string[] digits = Regex.Split(scoreTxt.text, @"\D+");
       // foreach (string value in digits)
       // {
            int score = int.Parse(digits[0]);
        //}
        score += 1;
        scoreTxt.text = score.ToString() + " / 5 Items Collected";

    }
}
