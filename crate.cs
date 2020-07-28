using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crate : MonoBehaviour
{
    [SerializeField]
    private GameObject cracked;

    public void Swap()
    {
        Instantiate(cracked, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }
}
