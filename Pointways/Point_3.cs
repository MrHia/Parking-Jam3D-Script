using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_3 : MonoBehaviour
{
    // Start is called before the first frame update
    bool isTurned = false;

    void Start()
    {
    }
    IEnumerator checkIsturnAfter(RaycastHit hit, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (isTurned)
        {
            hit.transform.GetComponent<CarController>().SetPosCollision(transform);
            hit.transform.GetComponent<CarController>().SetisDestroyCar(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.left));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.tag == "CarHorizontal-left" || hit.collider.tag == "CarHorizontal-right" || hit.collider.tag == "CarVertical-top" || hit.collider.tag == "CarVertical-bottom")
            {
                //Debug.Log("2: " + isTurned);
                if (hit.transform.position.z == transform.position.z)
                {
                    hit.transform.GetComponent<CarController>().SetDrirection_vector(Vector3.zero);
                }
                if (hit.collider.tag == "CarVertical-top")
                {

                    //hit.transform.GetComponent<CarController>().SetDrirection_vector(Vector3.zero);
                    hit.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.5f).OnComplete(() => {
                        isTurned = true;
                        //Debug.Log("is turn CarVertical-top: " + isTurned);
                    });
                }
                if (hit.collider.tag == "CarVertical-bottom")
                {
                    //hit.transform.GetComponent<CarController>().SetDrirection_vector(Vector3.zero);
                    hit.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f).OnComplete(() => {
                        isTurned = true;
                        //Debug.Log("is turn CarVertical-bottom: " + isTurned);
                    });
                }
                //Debug.Log("1: "+isTurned);
                StartCoroutine(checkIsturnAfter(hit, 0.4f));


            }
        }
    }
}
