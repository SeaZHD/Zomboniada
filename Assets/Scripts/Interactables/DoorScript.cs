using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator anim;
    private bool isOpen = false;


    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void PlayAnimation()
    {
        if (!isOpen)
        {
            anim.Play("OpenDoor");
            isOpen = true;
        }
        else
        { anim.Play("CloseDoor");
            isOpen = false;
        }
    }


    void Update()
    {
        
    }
}
