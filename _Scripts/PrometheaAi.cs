using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrometheaAi : MonoBehaviour
{
    public Transform target;
    public GameObject arloWaypoint1;
    public GameObject arloWaypoint2;
    public GameObject arloWaypoint3;
    public GameObject arloWaypoint4;
    public WaveManager wManager;
    public Rigidbody rigid;
    public float speed;
    float distanceToTarget;
    Vector3 direction;
    Vector3 heading;
    public GameObject currentWaypoint;
  
    
    

    // Start is called before the first frame update
    void Start()
    {
        
        rigid = this.gameObject.GetComponent<Rigidbody>();

        wManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<WaveManager>();
        
        speed = 7;


    }

    public GameObject GetCurrentWaypoint(GameObject randArloWaypoint)
    {
        currentWaypoint = randArloWaypoint;
        ChooseNextWaypoint();
        return currentWaypoint;
    }

    public void ChooseNextWaypoint()
    {
       
        switch (currentWaypoint.name)
        {
            case "ArloWaypoint 1":
                target = arloWaypoint2.transform;


                break;
            case "ArloWaypoint 2":
                target = arloWaypoint3.transform;

                break;
            case "ArloWaypoint 3":
                target = arloWaypoint4.transform;
                break;
            case "ArloWaypoint 4":
                target = arloWaypoint1.transform;

                break;
            default:
                target = arloWaypoint2.transform;
                break;
        }
        currentWaypoint = target.gameObject;

    }


   
    



    // Update is called once per frame
    void Update()
    {

       if(target != null)
        {
            GetDirection();
            FlyTowards();
        }
       
        if(distanceToTarget <= 1)
        {
            ChooseNextWaypoint();
        }

    }

    float GetDistanceToTarget() //Finds Android's distance to target 
    {
        // Gets a vector that points from the player's position to the target's.
        heading = target.position - this.transform.position;

        //calculates distance to target into a float
        distanceToTarget = heading.magnitude;

        return distanceToTarget;
    }

    Vector3 GetDirection() //Finds the direction of the target
    {
        

        GetDistanceToTarget();

        direction = heading / distanceToTarget; // This is now the normalized direction.
        transform.rotation = Quaternion.LookRotation(direction);

        return direction;

    }

    public void FlyTowards() //Makes our android fly towards the target.
    {
        rigid.velocity = direction * speed;

    }

}
