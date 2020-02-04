
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {

    public int selectedWeapon = 0;
    public bool RustSpearTest = false;
    public Player player;

	// Use this for initialization
	void Start () {
        SelectWeapon();
		
	}
	
	// Update is called once per frame
	void Update () {

        //GameObject thePlayer = GameObject.Find("Player");
        //player = thePlayer.GetComponent<Player>();
        RustSpearTest = player.rustSpearUnlock;  


        int previousSelectedWeapon = selectedWeapon;

		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount -1;
            else
                selectedWeapon--;
        }

        if (selectedWeapon == 1)
        {
            if (RustSpearTest == false)
            {
                selectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && RustSpearTest)
        {
            selectedWeapon = 1;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0; 
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++; 
        }
    }
}
