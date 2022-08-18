using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarInParking : MonoBehaviour
{
    CarController Car;
    Collider Collider; 

    private void Awake()
    {
        Car = this.GetComponentInParent<CarController>();
        Collider = GetComponent<BoxCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Box")
        {
            CarInParking carCollided = other.GetComponent<CarInParking>();
            if (carCollided.Car.tag == "Finish" && Car.gameObject.tag != "Finish")
            {

                Car.SetDrirection_vector(Vector3.zero);
                Car.SetIsMoving(false);
                Car.SetIsMoveTowards(false);
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.tag);
        if(other.tag == "Box")
        {
            CarInParking carCollided = other.GetComponent<CarInParking>();
            if (carCollided.Car.tag == "Finish" && Car.gameObject.tag != "Finish")
            {

                Car.SetDrirection_vector(Vector3.zero);
                Car.SetIsMoving(false);
                Car.SetIsMoveTowards(false);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        Car.SetIsMoveTowards(true);
    }

}
