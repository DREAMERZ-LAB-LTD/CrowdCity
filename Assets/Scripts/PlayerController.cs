using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    private PlayerController player;
    public float speed = 6f;
    [Range(0f, 1f)]
    public float movemenetSmoothing = .5f;
    [Range(0f, 1f)]
    public float rotationSmoothing = .25f;
    private Vector3 mouseCurrentPos;
    private Vector3 mouseStartPos;
    private Vector3 moveDirection;
    private Vector3 targetDirection;
    private Vector3 deviation;
    private float currentDragDistance;
    public float maxDragDistance = 10f;
    private bool move;
    Rigidbody rb;
    public Slider slider ;
    public Color color;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        this.player = GetComponent<PlayerController>();
        color = this.GetComponent<Renderer>().material.color;
    }
    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            deviation = targetDirection * speed * Time.deltaTime;
            player.rb.MovePosition(player.rb.position + deviation);

            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                if (transform.rotation != targetRotation)
                {
                    //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing);
                    //player.rb.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing);
                    player.rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothing));
                }
            }

        }
        else
            player.rb.velocity = Vector3.zero;


        // slider controls test
        speed = slider.value;
  

        if(GameResources.gm.start)
        HandlePlayerInput();
    }
    void FixedUpdate()
    {

    }

    private void HandlePlayerInput()
    {
        mouseCurrentPos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = mouseCurrentPos;
    
        }
        else if (Input.GetMouseButton(0) )
        {
            currentDragDistance = (mouseCurrentPos - mouseStartPos).magnitude;

            if (currentDragDistance > maxDragDistance)
            {
                //mouseStartPos = mouseCurrentPos - moveDirection * maxDragDistance;
            }
            move = true;
            moveDirection = (mouseCurrentPos - mouseStartPos).normalized;
            targetDirection = new Vector3(moveDirection.x, 0, moveDirection.y);
            //transform.position += deviation;          
   
        }
        else if (Input.GetMouseButtonUp(0))
        {
            move = false;
           
        }
    }
}

