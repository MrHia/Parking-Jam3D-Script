using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarInParking : MonoBehaviour
{
    CarController Car;
    Collider Collider;
    public AudioSource m_AudioBox;
    Vector3 direct_vector  ;
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
                //direct_vector = Car.GetDrirection_vector();
                
                if (Car.GetDrirection_vector() != Vector3.zero)
                {
                    direct_vector = Car.GetDrirection_vector();
                    m_AudioBox.Play();
                    Car.SetDrirection_vector(Vector3.zero);
                    Car.SetIsMoving(false);
                    Car.SetIsMoveTowards(false);
                }
                
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
                if(Car.GetDrirection_vector() != Vector3.zero)
                {
                    Car.SetDrirection_vector(Vector3.zero);
                    Car.SetIsMoving(false);
                    Car.SetIsMoveTowards(false);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
       
        
        if (other.tag == "Box")
        {
            CarInParking carCollided = other.GetComponent<CarInParking>();
            if (carCollided.Car.tag == "Finish" && Car.gameObject.tag != "Finish")
            {
                //Debug.Log(direct_vector + " Vs " +Car.gameObject.tag);
                Car.SetDrirection_vector(direct_vector);
                Car.SetIsMoving(true);
                Car.SetIsMoveTowards(true);

                Car.SetisDrag(true);
            }
        }
    }

}
