using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class way : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarHorizontal-left") || other.CompareTag("CarHorizontal-right") || other.CompareTag("CarVertical-top") || other.CompareTag("CarVertical-bottom"))
        {
            //Debug.Log(other.tag);
            other.transform.DOMove(this.transform.position,0.5f);
            //other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f).OnComplete(() => { });
            if(this.CompareTag("Way1") && other.CompareTag("CarVertical-top"))
            {

                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f).OnComplete(() => { });
                //other.transform.GetComponent<>().SetPosCollision();
                other.transform.GetComponent<test>().SetNextPosIndex(1);
            }
            if (this.CompareTag("Way1") && other.CompareTag("CarVertical-bottom"))
            {
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.5f).OnComplete(() => { });
                other.transform.GetComponent<test>().SetNextPosIndex(1);
            }
            if (this.CompareTag("Way2") && other.CompareTag("CarHorizontal-left"))
            {
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.5f).OnComplete(() => { });
                other.transform.GetComponent<test>().SetNextPosIndex(2);
            }
            if (this.CompareTag("Way2") && other.CompareTag("CarHorizontal-right"))
            {
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f).OnComplete(() => { });
                other.transform.GetComponent<test>().SetNextPosIndex(2);
            }

            if (this.CompareTag("Way3") && other.CompareTag("CarVertical-top"))
            {
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.5f).OnComplete(() => { });
                other.transform.GetComponent<test>().SetNextPosIndex(3);
            }
            if (this.CompareTag("Way3") && other.CompareTag("CarVertical-bottom"))
            {
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f).OnComplete(() => { });
                other.transform.GetComponent<test>().SetNextPosIndex(3);
            }
            
            if (this.CompareTag("Way4") && other.CompareTag("CarHorizontal-left"))
            {
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f).OnComplete(() => { });
                other.transform.GetComponent<test>().SetNextPosIndex(4);
            }
            if (this.CompareTag("Way4") && other.CompareTag("CarHorizontal-right"))
            {
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.5f).OnComplete(() => { });
                other.transform.GetComponent<test>().SetNextPosIndex(4);
            }
            other.transform.GetComponent<test>().SetisDestroyCar(true);
            other.tag = "Finish";
            
            Debug.Log(other.tag);
        }
    }

}
