using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class way : MonoBehaviour
{
    UIManager m_UI;
    GameController m_GC;
    int CarInGame = 0;

    private void Start()
    {
        m_GC = FindObjectOfType<GameController>();
    }

    void CountCarInGame()
    {
        CarInGame += GameObject.FindGameObjectsWithTag("CarHorizontal-left").Length;

        CarInGame += GameObject.FindGameObjectsWithTag("CarVertical-top").Length;


        CarInGame += GameObject.FindGameObjectsWithTag("CarVertical-bottom").Length;

        CarInGame += GameObject.FindGameObjectsWithTag("CarHorizontal-right").Length;
    }

    private void OnTriggerEnter(Collider other)

    {
        if (other.CompareTag("CarHorizontal-left") || other.CompareTag("CarHorizontal-right") || other.CompareTag("CarVertical-top") || other.CompareTag("CarVertical-bottom"))
        {



            if (this.CompareTag("Way1") && other.CompareTag("CarVertical-top"))
            {
                other.transform.DOMove(new Vector3(other.transform.position.x, this.transform.position.y, this.transform.position.z), 0.1f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.1f);
                other.transform.GetComponent<CarController>().SetNextPosIndex(1);
            }
            if (this.CompareTag("Way1") && other.CompareTag("CarVertical-bottom"))
            {
                other.transform.DOMove(new Vector3(other.transform.position.x, this.transform.position.y, this.transform.position.z), 0.1f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.1f);
                other.transform.GetComponent<CarController>().SetNextPosIndex(1);
            }
            if (this.CompareTag("Way2") && other.CompareTag("CarHorizontal-left"))
            {
                other.transform.DOMove(new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z), 0.1f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.1f);
                other.transform.GetComponent<CarController>().SetNextPosIndex(2);
            }
            if (this.CompareTag("Way2") && other.CompareTag("CarHorizontal-right"))
            {
                other.transform.DOMove(new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z), 0.1f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.1f);
                other.transform.GetComponent<CarController>().SetNextPosIndex(2);
            }

            if (this.CompareTag("Way3") && other.CompareTag("CarVertical-top"))
            {
                other.transform.DOMove(new Vector3(other.transform.position.x, this.transform.position.y, this.transform.position.z), 0.1f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.1f);
                other.transform.GetComponent<CarController>().SetNextPosIndex(3);
            }
            if (this.CompareTag("Way3") && other.CompareTag("CarVertical-bottom"))
            {
                other.transform.DOMove(new Vector3(other.transform.position.x, this.transform.position.y, this.transform.position.z), 0.1f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.1f);
                other.transform.GetComponent<CarController>().SetNextPosIndex(3);
            }

            if (this.CompareTag("Way4") && other.CompareTag("CarHorizontal-left"))
            {
                other.transform.DOMove(new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z), 0.1f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.1f);
                other.transform.GetComponent<CarController>().SetNextPosIndex(4);
            }
            if (this.CompareTag("Way4") && other.CompareTag("CarHorizontal-right"))
            {
                other.transform.DOMove(new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z), 0.1f);
                other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y - 90f, 0f), 0.1f);
                other.transform.GetComponent<CarController>().SetNextPosIndex(4);
            }

            other.tag = "Finish";
            m_GC.incrementCoint();

            other.transform.GetComponent<CarController>().SetisDestroyCar(true);
            other.transform.GetComponent<CarController>().m_DrifAudio.Play();
            //transform.GetComponent<GameController>().m_MoveAudio.loop = true;
            m_GC.m_MoveAudio.Play();
            
        }
    }
    
}
