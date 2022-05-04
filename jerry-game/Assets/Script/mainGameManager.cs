using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class mainGameManager : MonoBehaviour
{
    public GameObject apple;
    private GameObject mainCharacter;
    public GameObject bag;
    private bool foodShow = false;
    public Rig eyeOnApple;
    private Touch touch;
    private float foodMoveSpeedRate = 0.003f;
    private Animator animator;

    private float disToFood_x = 0;
    private float disToFood_y = 0;
    private Vector3 prviousCharatcerPosition = new Vector3(0, -1.75f, 5);
    private bool lastWalkinRight = false;
    



    void Start()
    {
        apple = GameObject.Find("Apple");
        bag = GameObject.Find("bag");
        mainCharacter = GameObject.Find("mainCharacter");
        apple.SetActive(false);
        bag.SetActive(false);
        eyeOnApple.weight = 0;
        animator = mainCharacter.GetComponent<Animator>();
    }

    private void Update()
    {
        moveFood();
        traceFood();
    }

    public void feedBtnOnClick()
    {
        apple.SetActive(true);
        foodShow = true;
        eyeOnApple.weight = 1;
        setCharacterFace();

    }

    public void danceBtnOnClick()
    {
        if (animator.GetBool("isDancing"))
        {
            animator.SetBool("isDancing", false);
            mainCharacter.transform.rotation = Quaternion.Euler(0, 360, 0);//face forward while nor dancing
        }
        else
        {
            animator.SetBool("isDancing", true);
        }
    } 

    private void moveFood()
    {
        if (foodShow)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    float new_x = apple.transform.position.x + touch.deltaPosition.x * foodMoveSpeedRate;
                    float new_y = apple.transform.position.y + touch.deltaPosition.y * foodMoveSpeedRate;
                    if (new_x > 1) { new_x = 1;}
                    if (new_x < -1) { new_x = -1; }
                    if (new_y > 0.65f) { new_y = 0.65f; }
                    if (new_y < -1.25f) { new_y = -1.25f; }
                    apple.transform.position = new Vector3(new_x, new_y, apple.transform.position.z);
                }
            }
        }
    }

    private void traceFood()
    {
        if (foodShow)
        {
            animator.SetBool("isWalking", true);
            Vector3 lastPos = prviousCharatcerPosition; //character position of previous frame
            disToFood_x = apple.transform.position.x * (mainCharacter.transform.position.z / apple.transform.position.z) - mainCharacter.transform.position.x;
            disToFood_y = apple.transform.position.y * (mainCharacter.transform.position.z / apple.transform.position.z) - mainCharacter.transform.position.y;

            if (disToFood_x > 0.05 || disToFood_x < -0.05)
            {
                if (lastWalkinRight)//walking right privous
                {
                    if (disToFood_x < 0)//walking left now
                    {
                        lastWalkinRight = false;
                        mainCharacter.transform.Rotate(0f, 180f, 0f);
                    }
                }
                else //walking left privous
                {
                    if (disToFood_x > 0)//walking right now
                    {
                        lastWalkinRight = true;
                        mainCharacter.transform.Rotate(0f, 180f, 0f);
                    }
                }
                prviousCharatcerPosition = mainCharacter.transform.position;//update last frame position
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        
    }

    private void setCharacterFace()
    {
        if (foodShow)
        {
            if (apple.transform.position.x * (mainCharacter.transform.position.z / apple.transform.position.z) - mainCharacter.transform.position.x < 0)
            {
                mainCharacter.transform.Rotate(0f, -90f, 0f); //face rightside
                lastWalkinRight = true;
            }
            else
            {
                mainCharacter.transform.Rotate(0f, 90f, 0f); //face leftside
            }
        }
    }


}
