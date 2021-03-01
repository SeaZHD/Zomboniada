using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAttackScript : MonoBehaviour
{
    public string opponentLayer;
    GameObject target;
    Animator anim;

    void Start()
    {
        target = GameObject.Find("Player");
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("2"))
        {
            if (other.gameObject.layer != LayerMask.NameToLayer(opponentLayer))
                return;
            Debug.Log("HIT");
            target.GetComponent<HealthStatus>().TakeDamage(10f);
        }



    }
}
