using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSword : MonoBehaviour
{
    public GameObject fireBeam;
    public Transform beamLocation;
    bool cooldown;
    
    // Start is called before the first frame update
    void Start()
    {
        Fire();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(cooldown == false)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.5)
            {
                Fire();
                cooldown = true;
                StartCoroutine(Cooldown(0.5f));
            }
        }
        
    }

    IEnumerator Cooldown(float value)
    {
        yield return new WaitForSeconds(value);
        cooldown = false;
    }

    public void Fire()
    {
        
        Vector3 weaponDirection = this.gameObject.transform.up;
        GameObject fireOBJ = Instantiate(fireBeam, beamLocation.position, Quaternion.identity);
        fireOBJ.AddComponent<Rigidbody>();
        fireOBJ.GetComponent<Rigidbody>().useGravity = false;
        fireOBJ.GetComponent<Rigidbody>().velocity = weaponDirection * 15;

        StartCoroutine(PurgeShot(fireOBJ));
    }

    IEnumerator PurgeShot(GameObject shot)
    {
        yield return new WaitForSeconds(10);
        Destroy(shot);
    }
}
