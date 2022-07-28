using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class test : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{

    public float PositionxBegin;

    public float PositionyBegin;

    public float PositionxEnd;

    public float PositionyEnd;


    public Rigidbody m_Rigidbody;


    public Rigidbody c_Rigidbody;

    public float speed = 10;

    public string direction = "";


    public Vector3 drirection_vector;

    [SerializeField] Transform[] Positions;
    [SerializeField] Transform nextPos;

    float objectSpeed = 10;

    int nextPosIndex;

    public bool isDestroy = false;

    bool isMoving = false;

    bool isMoveTowards = true;

    //public bool checkOnTriggerEnter = false;
    void Start()
    {
        m_Rigidbody = this.GetComponent<Rigidbody>();
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
            if (this.tag == "Finish" && collision.gameObject.tag == "Finish")
            {
                float distThis = Vector3.Distance(nextPos.position, transform.position);
                float distCollision = Vector3.Distance(nextPos.position, collision.transform.position);
                if (distCollision < distThis)
                {
                    StartCoroutine(EnterobjectSpeed());
                }
            }

        }
    }

    public bool checkOnTriggerStay = false;
    private void OnTriggerStay(Collider other)
    {
        checkOnTriggerStay = true;
    }
    public bool checkOnCollisionStay = false;
    void OnCollisionExit(Collision collision)
    {
       if (collision.gameObject.tag == "CarHorizontal-left" || collision.gameObject.tag == "CarHorizontal-right" || collision.gameObject.tag == "CarVertical-top" || collision.gameObject.tag == "CarVertical-bottom" || collision.gameObject.tag == "wall" || collision.gameObject.tag == "Finish")
       {
          drirection_vector = Vector3.zero;

       }
       checkOnCollisionStay = false;     
    }

        

    IEnumerator EnterobjectSpeed()
    {
        objectSpeed = 0f;
        yield return new WaitForSeconds(0.5f);
        objectSpeed = 10;
    }

    public void SetNextPosIndex(int SetNextPosIndex)
    {
        nextPosIndex = SetNextPosIndex;
        nextPos = Positions[SetNextPosIndex];
    }


    public void SetisDestroyCar(bool isDestroyCar)
    {
        isDestroy = isDestroyCar;
    }



    public void SetIsMoving(bool isMoving_)
    {
        isMoving = isMoving_;
    }

    public void SetDrirection_vector(Vector3 SetDrirection)
    {
        drirection_vector = SetDrirection;
    }

    void MoveGameObject()
    {
        if (transform.position == nextPos.position)
        {

            isMoveTowards = false;
            nextPosIndex++;

            if (nextPosIndex >= Positions.Length)
            {
                Destroy(gameObject);
            }
            nextPos = Positions[nextPosIndex];
            transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f).OnComplete(() => { isMoveTowards = true; });
        }
        else
        {
            if (isMoveTowards)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos.position, objectSpeed * Time.deltaTime);
            }
        }
    }

    void Update()
    {
        if (isMoving)
        {

            transform.Translate(drirection_vector * speed * Time.deltaTime);
        }
        if (isDestroy)
        {
            drirection_vector = Vector3.zero;
            gameObject.tag = "Finish";
            MoveGameObject();
        }
    }
}