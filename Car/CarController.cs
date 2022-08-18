using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarController : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
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

     public bool isMoving = false;

    public bool isMoveTowards = true;

    GameController m_GameCtrl;

    CarController c_CarCtrl;


    GameObject gameObject1;

    //public bool checkOnTriggerEnter = false;
    void Start()
    {
       

        m_Rigidbody = this.GetComponent<Rigidbody>();
        m_GameCtrl = FindObjectOfType<GameController>();
    }

    public Transform GetNextPos()
    {
        return nextPos;
    }
    public void SetNextPosIndex(int SetNextPosIndex)
    {
        nextPosIndex = SetNextPosIndex;
        nextPos = Positions[SetNextPosIndex];
    }

    public bool GetisMoveTowards()
    {
        return isMoveTowards;
    }

    public Vector3 GetDrirection_vector()
    {
        return drirection_vector;
    }

    public void SetisDestroyCar(bool isDestroyCar)
    {
        isDestroy = isDestroyCar;

    }



    public void SetIsMoving(bool isMoving_)
    {
        isMoving = isMoving_;
    }

    public void SetIsMoveTowards(bool isMoveTowards_)
    {
        isMoveTowards = isMoveTowards_;
    }



    public void SetDrirection_vector(Vector3 SetDrirection)
    {
        drirection_vector = SetDrirection;
    }




    //Called by the EventSystem every time the pointer is moved during dragging.
    //while dragging the mouse to get the coordinates of x,y
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.tag == "CarHorizontal-left" || eventData.pointerDrag.tag == "CarHorizontal-right" || eventData.pointerDrag.tag == "CarVertical-top" || eventData.pointerDrag.tag == "CarVertical-bottom")
        {

            PositionyEnd = Input.mousePosition.y;

            PositionxEnd = Input.mousePosition.x;
        }

    }
    //At the end, drag the mouse to compare the beginning and end positions of x, y, giving the direction for each type of vehicle
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

    //When the mouse is pressed down get the original coordinates of x,y
    public void OnPointerDown(PointerEventData eventData)
    {

        PositionxBegin = Input.mousePosition.x;
        PositionyBegin = Input.mousePosition.y;

    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "CarHorizontal-left" || collision.gameObject.tag == "CarHorizontal-right" || collision.gameObject.tag == "CarVertical-top" || collision.gameObject.tag == "CarVertical-bottom" || collision.gameObject.tag == "wall" || collision.gameObject.tag == "Finish")
        {


            if (drirection_vector != Vector3.zero)
            {

                if (drirection_vector == Vector3.left)
                {
                    transform.position = transform.position + Vector3.right * speed * Time.fixedDeltaTime;
                }
                if (drirection_vector == Vector3.right)
                {
                    transform.position = transform.position + Vector3.left * speed * Time.fixedDeltaTime;
                }
                if (drirection_vector == Vector3.forward)
                {
                    transform.position = transform.position + Vector3.back * speed * Time.fixedDeltaTime;
                }
                if (drirection_vector == Vector3.back)
                {
                    transform.position = transform.position + Vector3.forward * speed * Time.fixedDeltaTime;
                }
            }

            drirection_vector = Vector3.zero;

            isMoving = false;

            if (this.tag == "Finish" && collision.gameObject.tag == "Finish")
            {

                gameObject1 = collision.gameObject;
                c_CarCtrl = gameObject1.GetComponent<CarController>();
                if (Vector3.Distance(this.nextPos.position, c_CarCtrl.GetNextPos().position) == 0)
                {
                    float distThis = Vector3.Distance(nextPos.position, transform.position);
                    float distCollision = Vector3.Distance(nextPos.position, c_CarCtrl.GetNextPos().position);

                    if (distCollision < distThis)
                    {
                        isMoveTowards = false;
                        c_CarCtrl.SetIsMoveTowards(true);
                    }

                }

            }

        }
    }


    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "CarHorizontal-left" || collisionInfo.gameObject.tag == "CarHorizontal-right" || collisionInfo.gameObject.tag == "CarVertical-top" || collisionInfo.gameObject.tag == "CarVertical-bottom" || collisionInfo.gameObject.tag == "wall" || collisionInfo.gameObject.tag == "Finish")
        {
            if (collisionInfo.gameObject.tag == "Finish" && this.tag == "Finish")
            {
                gameObject1 = collisionInfo.gameObject;
                c_CarCtrl = gameObject1.GetComponent<CarController>();
                if (this.nextPos.position == c_CarCtrl.GetNextPos().position)
                {
                    float distThis = Vector3.Distance(nextPos.position, transform.position);
                    float distCollision = Vector3.Distance(nextPos.position, c_CarCtrl.GetNextPos().position);
                    if (distCollision < distThis)
                    {
                        StartCoroutine(EnterobjectSpeed());
                        c_CarCtrl.SetIsMoveTowards(true);
                    }
                }
            }

        }
    }



    //public bool checkOnCollisionStay = false;
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "CarHorizontal-left" || collision.gameObject.tag == "CarHorizontal-right" || collision.gameObject.tag == "CarVertical-top" || collision.gameObject.tag == "CarVertical-bottom" || collision.gameObject.tag == "wall" || collision.gameObject.tag == "Finish")
        {
            if (collision.gameObject.tag == "Finish" && this.tag == "Finish")
            {
                gameObject1 = collision.gameObject;
                c_CarCtrl = gameObject1.GetComponent<CarController>();
                if (this.nextPos.position == c_CarCtrl.GetNextPos().position)
                {

                    float distThis = Vector3.Distance(nextPos.position, transform.position);
                    float distCollision = Vector3.Distance(nextPos.position, c_CarCtrl.GetNextPos().position);
                    if (distCollision < distThis)
                    {
                        StartCoroutine(EnterobjectSpeed());
                        c_CarCtrl.SetIsMoveTowards(true);
                    }
                }
            }
            //c_CarCtrl.SetIsMoveTowards(true);
        }

    }



    IEnumerator EnterobjectSpeed()
    {
        isMoveTowards = false;
        //Debug.Log("==0");
        yield return new WaitForSeconds(0.1f);
        isMoveTowards = true;
        //Debug.Log("==10");
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
                m_GameCtrl.ReduceCar();
            }
            else
            {
                nextPos = Positions[nextPosIndex];
            }
            
            transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.1f).OnComplete(() => { isMoveTowards = true;});

            //other.transform.DOMove(new Vector3(this.transform.position.x, this.transform.position.y, other.transform.position.z), 0.5f);
            //other.transform.DORotate(new Vector3(0f, transform.rotation.eulerAngles.y + 90f, 0f), 0.5f);


        }
        else
        {
            if (isMoveTowards)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos.position, objectSpeed * Time.fixedDeltaTime);
            }
        }
    }


    void FixedUpdate()
    {
        if (isMoving)
        {

            transform.Translate(drirection_vector * speed * Time.fixedDeltaTime);
            //transform.DOMove(transform.position + drirection_vector,2f,isMoving);
        }
        if (isDestroy)
        {   
            drirection_vector = Vector3.zero;
            gameObject.tag = "Finish";
            MoveGameObject();
        }
    }
}