using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();

                if(player != null)
                {
                    if (player._hasCoin == true)
                    {
                        player._hasCoin = false;
                        UIManager manager = GameObject.Find("Canvas").GetComponent<UIManager>();
                        if(manager != null)
                        {
                            manager.removeCoin();
                        }

                        AudioSource audio = GetComponent<AudioSource>();
                        audio.Play();

                        player.EnableWeapon();
                    }

                    else
                    {
                        Debug.Log("No COIN");
                    }


                }

            }
        }
    }
}
