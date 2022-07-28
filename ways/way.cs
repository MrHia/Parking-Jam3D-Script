using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class way : MonoBehaviour
{
    IEnumerator WaitingToTurnTheCar(Collider other)
    {
        other.tag = "WaitingToTurnTheCar";
        yield return new WaitForSeconds(0.5f);
        other.tag = "Finish";
        other.transform.GetComponent<test>().SetisDestroyCar(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarHorizontal-left") || other.CompareTag("CarHorizontal-right") || other.CompareTag("CarVertical-top") || other.CompareTag("CarVertical-bottom"))
        {



            if (this.CompareTag("Way1") && other.CompareTag("CarVertical-top"))
            {
                other.transform.DOMove(new Vector3(other.transform.position.x, this.transform.position.y, this.transform.position.z), 0.5f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f);

                other.transform.GetComponent<test>().SetNextPosIndex(1);


            }
            if (this.CompareTag("Way1") && other.CompareTag("CarVertical-bottom"))
            {
                other.transform.DOMove(new Vector3(other.transform.position.x, this.transform.position.y, this.transform.position.z), 0.5f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.5f);
                other.transform.GetComponent<test>().SetNextPosIndex(1);
            }
            if (this.CompareTag("Way2") && other.CompareTag("CarHorizontal-left"))
            {
                other.transform.DOMove(new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z), 0.5f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.5f);
                other.transform.GetComponent<test>().SetNextPosIndex(2);
            }
            if (this.CompareTag("Way2") && other.CompareTag("CarHorizontal-right"))
            {
                other.transform.DOMove(new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z), 0.5f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f);
                other.transform.GetComponent<test>().SetNextPosIndex(2);
            }

            if (this.CompareTag("Way3") && other.CompareTag("CarVertical-top"))
            {
                other.transform.DOMove(new Vector3(other.transform.position.x, this.transform.position.y, this.transform.position.z), 0.5f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.5f);
                other.transform.GetComponent<test>().SetNextPosIndex(3);
            }
            if (this.CompareTag("Way3") && other.CompareTag("CarVertical-bottom"))
            {
                other.transform.DOMove(new Vector3(other.transform.position.x, this.transform.position.y, this.transform.position.z), 0.5f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f);
                other.transform.GetComponent<test>().SetNextPosIndex(3);
            }
            
            if (this.CompareTag("Way4") && other.CompareTag("CarHorizontal-left"))
            {
                other.transform.DOMove(new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z), 0.5f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f);
                other.transform.GetComponent<test>().SetNextPosIndex(4);
            }
            if (this.CompareTag("Way4") && other.CompareTag("CarHorizontal-right"))
            {
                other.transform.DOMove(new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z), 0.5f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.5f);
                other.transform.GetComponent<test>().SetNextPosIndex(4);
            }
            
            //StartCoroutine(WaitingToTurnTheCar(other));
            other.tag = "Finish";
            other.transform.GetComponent<test>().SetisDestroyCar(true);

            //Debug.Log(other.tag);
        }
    }

}
