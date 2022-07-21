using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class testraycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.left));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, transform.TransformDirection(Vector3.left) * hit.distance, Color.blue);


            hit.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f);
        }
        else
        {
            Debug.DrawRay(ray.origin, transform.TransformDirection(Vector3.left) * hit.distance, Color.red);
        }
    }
}
