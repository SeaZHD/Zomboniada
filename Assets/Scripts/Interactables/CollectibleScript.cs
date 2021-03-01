using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleScript : MonoBehaviour
{ 

    public void CollectItem(RaycastHit hit)
    {
        Destroy(hit.collider.gameObject);  

    }

}
