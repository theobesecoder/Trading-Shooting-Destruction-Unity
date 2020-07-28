using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
   [SerializeField]
    private AudioClip audioSource;

   

    private void OnTriggerStay(Collider other)
    {
       
        if(other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();

                if (player != null)
                {
                    player._hasCoin = true;

                    AudioSource.PlayClipAtPoint(audioSource, transform.position, 1f);

                    UIManager uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

                    uIManager.showCoin();

                    Destroy(this.gameObject);
                }
                
            }
        }
    }
}
