using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidAI : MonoBehaviour
{
    public Transform target;
    public Transform arenaCenter;
    public Rigidbody rigid;
    public float speed;
    float distanceToTarget;
    float distArenaCenter;
    Vector3 direction;
    Vector3 heading;
    Vector3 headingArenaCenter;
    int randRange;
    Animator anim;
    bool landed;

    public Transform[] androidHands;
    public GameObject androidBeam;

    public GameObject androidHealth;
    public Material androidHealthMaterial;
    public Color healthy;
    public Color fine;
    public Color poor;

    public int healthNum;

    WaveManager waveManager;

    // Start is called before the first frame update
    void Start()
    {
        randRange = Random.Range(-5, 5);
        rigid = this.gameObject.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        landed = false;
        waveManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<WaveManager>();
        InvokeRepeating("Shoot", 5f, 5);
        androidHealthMaterial = androidHealth.GetComponent<Renderer>().material;
        androidHealthMaterial.color = healthy;
        healthNum = 3;
    }

    // Update is called once per frame
    void Update()
    {
       
        GetDistanceToArenaCenter();
        GetDirection();
        if(healthNum <= 0)
        {
            waveManager.killCount++;
            Destroy(this.gameObject);
        }

        if (waveManager.prometheaVisible == true)
        {
            speed = 3;
            if (landed == true)
            {
                landed = false;
                anim.SetInteger("AndroidAction", 1);
                rigid.useGravity = false;
            }
            FlyTowards();
        }
        else
        {
            if (distArenaCenter <= 20 && distArenaCenter > 8 + randRange)
            {
                if (landed != true)
                {
                    Land();
                }

                FlyTowards();
            }
            else if (distArenaCenter <= 8 + randRange)
            {
                Stop();
            }
            else
            {
                FlyTowards();
            }
        }

        HealthColor();

        
        
    }

    void HealthColor()
    {
        if(healthNum == 3)
        {

            androidHealthMaterial.color = healthy;
            androidHealthMaterial.SetColor("_EmissionColor", healthy);
        }
        else if (healthNum == 2)
        {
            androidHealthMaterial.color = fine;
            androidHealthMaterial.SetColor("_EmissionColor", fine);
        }
        else
        {
            androidHealthMaterial.color = poor;
            androidHealthMaterial.SetColor("_EmissionColor", poor);
        }
    }

    //void RotateHealthGauge()
    //{
        
    //    var head = GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position;

    //    var dist = head.magnitude;

    //    var dir = head / dist;

    //    androidHealth.transform.rotation = Quaternion.LookRotation(dir);
    //}

    Transform FindTarget() //Finds the target we need to track
    {
        if (waveManager.prometheaVisible == true)
        {
            target = GameObject.FindGameObjectWithTag("Promethea").transform;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        return target;
    }

    float GetDistanceToTarget() //Finds Android's distance to target 
    {
        // Gets a vector that points from the Android's position to the target's.
        heading = target.position - this.transform.position;

        //calculates distance to target into a float
        distanceToTarget = heading.magnitude;

        return distanceToTarget;
    }

    float GetDistanceToArenaCenter() //Finds Android's distance to the Arena's center
    {
        arenaCenter = GameObject.FindGameObjectWithTag("Center").transform;
        // Gets a vector that points from the Android's position to the target's.
        headingArenaCenter = arenaCenter.position - this.transform.position;

        //calculates distance to arenaCenter into a float
        distArenaCenter = headingArenaCenter.magnitude;

        return distArenaCenter;
    }

    Vector3 GetDirection() //Finds the direction of the target
    {
        FindTarget();

        GetDistanceToTarget();

        direction = heading / distanceToTarget; // This is now the normalized direction.
        transform.rotation = Quaternion.LookRotation(direction);

        return direction;

    }

    public void FlyTowards() //Makes our android fly towards the target.
    {
        rigid.velocity = direction * speed;
        
    } 

    public void Land()
    {
        landed = true;
        rigid.useGravity = true;
    
    }
    public void Stop()
    {
        anim.SetInteger("AndroidAction", 3);
        speed = 0;
        rigid.velocity = Vector3.zero;
    }

    public void Shoot()
    {
        foreach(Transform armLoc in androidHands)
        {
            
            GameObject tempObj = Instantiate(androidBeam, armLoc.position, Quaternion.identity);
            tempObj.AddComponent<Rigidbody>();
            tempObj.GetComponent<Rigidbody>().useGravity = false;
            tempObj.GetComponent<Rigidbody>().velocity = direction * 10;
            tempObj.transform.rotation = Quaternion.LookRotation(direction);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            GameObject hitObj = collision.gameObject;

            if(hitObj.tag == "FireBeam")
            {
                healthNum--;
            }
        }
    }
}
