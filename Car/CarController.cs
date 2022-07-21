using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class CarController : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{

    public float PositionxBegin;
    public float PositionyBegin;

    public float PositionxEnd;

    public float PositionyEnd;

    public Rigidbody m_Rigidbody;
    public Rigidbody c_Rigidbody;

    BoxCollider m_Collider;
    
    public float speed = 10;

    public string direction = "";
    public Vector3 drirection_vector;

    public Transform PosCollision;

    public List<Vector3> Positions = new List<Vector3>();

    [SerializeField] Vector3[] Directions;
    [SerializeField] Vector3 NextPos;

    Vector3[] points;
    public int NextPosIndex;
    

    public bool isDestroy = false;

    bool isMoving = false;

    bool isMoveTowards = true;
    bool Rotation;


    void Start()
    {
        m_Rigidbody = this.GetComponent<Rigidbody>();
        m_Collider =  this.GetComponent<BoxCollider>();

        Positions.Add(new Vector3(-6f, 0.5f, 6f));
        Positions.Add(new Vector3(6f, 0.5f, 6f));
        Positions.Add(new Vector3(6f, 0.5f, -6f));
        Positions.Add(new Vector3(-6f, 0.5f, -6f));
        Positions.Add(new Vector3(-6f, 0.5f, 10f));
    }

    //Called by the EventSystem every time the pointer is moved during dragging.
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.tag == "CarHorizontal-left" || eventData.pointerDrag.tag == "CarHorizontal-right" || eventData.pointerDrag.tag == "CarVertical-top" || eventData.pointerDrag.tag == "CarVertical-bottom")
        {

            PositionyEnd = Input.mousePosition.y;

            PositionxEnd = Input.mousePosition.x;
        }

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.tag == "CarHorizontal-left" || eventData.pointerDrag.tag == "CarHorizontal-right")
            {

                if (PositionxBegin > PositionxEnd)
                {

                    drirection_vector = Vector3.left;
                }
                else if (PositionxBegin < PositionxEnd)
                {

                    drirection_vector = Vector3.right;
                }
        }
         if (eventData.pointerDrag.tag == "CarVertical-top" || eventData.pointerDrag.tag == "CarVertical-bottom")
            {

                if (PositionyEnd > PositionyBegin)
                {
                    drirection_vector = Vector3.forward;

                }
                else if (PositionyEnd < PositionyBegin)
                {
                    drirection_vector = Vector3.back;
                }

            }
            if (drirection_vector != Vector3.zero)
            {
                isMoving = true;
            }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

            PositionxBegin = Input.mousePosition.x;
            PositionyBegin = Input.mousePosition.y;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "CarHorizontal-left" || collision.gameObject.tag == "CarHorizontal-right" || collision.gameObject.tag == "CarVertical-top" || collision.gameObject.tag == "CarVertical-bottom" || collision.gameObject.tag == "wall" || collision.gameObject.tag == "Finish")
        {


            if (drirection_vector.sqrMagnitude != 0)
            {

                if (drirection_vector == Vector3.left)
                {
                    transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
                }
                if (drirection_vector == Vector3.right)
                {
                    transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
                }
                if (drirection_vector == Vector3.forward)
                {
                    transform.position = transform.position + Vector3.back * speed * Time.deltaTime;
                }
                if (drirection_vector == Vector3.back)
                {
                    transform.position = transform.position + Vector3.forward * speed * Time.deltaTime;
                }
            }
            drirection_vector = Vector3.zero;
            isMoving = false;
        }
    }

    
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "CarHorizontal-left" || collision.gameObject.tag == "CarHorizontal-right" || collision.gameObject.tag == "CarVertical-top" || collision.gameObject.tag == "CarVertical-bottom"|| collision.gameObject.tag == "wall" || collision.gameObject.tag == "Finish")
        {
            drirection_vector = Vector3.zero;

        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Pointway")
    //    {
    //        transform.DORotate(new Vector3(0f, 90f, 0f), (float)0.5);
    //        Debug.Log(other.tag);
    //    }
    //}

    public void setRotation( bool Rotation_)
    {

        Rotation = Rotation_;
    }
    public void SetPosCollision(Transform c_PosCollision)
    {

        PosCollision = c_PosCollision;
        NextPosIndex = Positions.IndexOf(PosCollision.position);
        
        NextPos = Positions[++NextPosIndex];
        //Debug.Log(c_PosCollision.position,c_PosCollision.gameObject);
        //Debug.Log(NextPosIndex);
        //Debug.Break();


    }
    public void SetisDestroyCar(bool isDestroyCar)
    {
        isDestroy = isDestroyCar;
    }

    public Transform GetPosCollision()
    {
        return PosCollision;
    }
    public void SetIsMoving(bool isMoving_)
    {
        isMoving = isMoving_;
    }

    public void SetDrirection_vector(Vector3 SetDrirection)
    {
        drirection_vector = SetDrirection;
    }
    private void MoveGameObject()
    {

        if (transform.position == NextPos)
        {
            Debug.Log("NextPosIndex: " + NextPosIndex);
            //Debug.LogError("0" + transform.rotation.eulerAngles, transform.gameObject);
            isMoveTowards = false;
            //transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f).OnComplete(() => {
            //    Debug.LogError("1"+transform.rotation, transform.gameObject);
            //});
            
            NextPosIndex++;


            if (NextPosIndex == Positions.Count)
            {

                Destroy(gameObject);
            }
            NextPos = Positions[NextPosIndex];

            transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f).OnComplete(() => { isMoveTowards = true; });
        }
        else 
        {
            if (isMoveTowards)
            {
                NextPos.y = 0;
                transform.position = Vector3.MoveTowards(transform.position, NextPos, speed * Time.deltaTime);
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isMoving)
        {
            
            transform.Translate(drirection_vector * speed * Time.deltaTime);
        }
        if (isDestroy)
        {
            

            //transform.DORotate(new Vector3(0f, -90f, 0f), 0.5f);
            //isMoving = true;

            drirection_vector = Vector3.zero;
            gameObject.tag = "Finish";
            MoveGameObject();
            
        }


    }


}