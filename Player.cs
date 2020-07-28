using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private float _gravity = 9.81f;

    private CharacterController characterController;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private GameObject hitmarkerPrefab;

    [SerializeField]
    private AudioSource weaponSound;

    [SerializeField]
    private int currentAmmo;
    [SerializeField]
    private int MaxAmmo = 50;

    [SerializeField]
    private bool canShoot = true;

    private UIManager uIManager;

    [SerializeField]
    public bool _hasCoin = false;

    [SerializeField]
    private GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        characterController = GetComponent<CharacterController>();
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(characterController == null)
        {
            Debug.LogError("Character Controller");
        }

        currentAmmo = MaxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        uIManager.UpdateAmmo(currentAmmo);

        if (Input.GetMouseButton(0) && currentAmmo > 0)
        {
            if (canShoot == true)
            {
                Shoot();
            }
        }
        
        else
        {
            muzzleFlash.SetActive(false);
            //stop         
            weaponSound.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            canShoot = false;
            StartCoroutine(reloadLogic());
            
        }

        CalculateMovement();
    }

    void CalculateMovement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 velocity = direction * _speed;

        //Gravity acts on the vertical axis (Y), so we will take the y axis of the velocity

        velocity.y = velocity.y - _gravity; // Calculation of Gravity.

        velocity = transform.transform.TransformDirection(velocity); // convert the local to global co-ordinate

        characterController.Move(velocity * Time.deltaTime);
    }

    void Shoot()
    {

        if (weaponSound.isPlaying == false)
        {
            weaponSound.Play();
        }
        //Play Muzzle Flash
        muzzleFlash.SetActive(true);

        currentAmmo--;

        //Fire a Ray from the centre of the screen.
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Instantiate(hitmarkerPrefab, hit.point, Quaternion.LookRotation(hit.normal));  //hit.normal cuz of particle effect perpendicular to the surface.

            //check crate hit?
            //destroy crate
            crate crate = hit.transform.GetComponent<crate>();
            if(crate != null)
            {
                crate.Swap();
            }
        

        }


       
    }

    IEnumerator reloadLogic()
    {
        yield return new WaitForSeconds(2.5f);
        currentAmmo = MaxAmmo;

        canShoot = true;
    }

    public void EnableWeapon()
    {
        weapon.SetActive(true);
    }
}
