using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPosition;
    public GameObject[] rooms;

    private int direction;
    public float moveAmount = 10;

    [SerializeField] int totalRoom = 20;


    private float timeBetweenRoom;
    public float startTimeBetweenRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY = -Mathf.Infinity;
    private bool stopGeneration;

    private bool comeFromLeft;
    private bool comeFromRight;
    private bool justGotDown;

    private bool finalRoom;
    private bool isAtBound;
    private bool gonnaBump;
    private int downCounter;
    private bool toCheckCreateFinalRoom=true;

    public int generatedCount;


    void Start()
    {
        generatedCount = 0;
        timeBetweenRoom = startTimeBetweenRoom;




        int randStartingPos = Random.Range(0, startingPosition.Length);
        transform.position = startingPosition[randStartingPos].position;




        Instantiate(rooms[4], transform.position, Quaternion.identity);



        direction = 1;

    }
    void Update()
    {


        if (timeBetweenRoom <= 0 && stopGeneration == false && generatedCount <= totalRoom)
        {

            Move();



            timeBetweenRoom = startTimeBetweenRoom;
        }


        if (generatedCount >= totalRoom)
        {
            StartCoroutine(StopCountTime());//todo dont cut it down harshly
        }
        else
        {
            timeBetweenRoom -= Time.deltaTime;
        }


        if ((transform.position.x <= minX) || (transform.position.x >= maxX))
        {
            isAtBound = true;
        }
        else
        {
            isAtBound = false;
        }
        if ((transform.position.x - moveAmount == minX) || (transform.position.y - moveAmount == minY) || (transform.position.x + moveAmount == maxX))
        {
            gonnaBump = true;
        }
        else
        {
            gonnaBump = false;
        }
        if (generatedCount == totalRoom)
        {
            finalRoom = true;
        }
        if (finalRoom == true&&toCheckCreateFinalRoom==true)
        {
            Invoke("CreateFinalRoom", startTimeBetweenRoom - 0.05f);
            
            toCheckCreateFinalRoom = false;
        }



        //Debug.Log(direction);


    }
    IEnumerator StopCountTime()
    {
        yield return new WaitForSeconds(2);
        timeBetweenRoom = startTimeBetweenRoom;

    }
    private void Move()
    {


        if (direction == 1 || direction == 2)//move right
        {
            if (transform.position.x < maxX)//can move right
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                comeFromLeft = true;

                direction = Random.Range(1, 6);

                if (direction == 3)
                {
                    direction = 1;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else if (transform.position.x >= maxX)
            {

                direction = 5;



            }

        }
        else if (direction == 3 || direction == 4)//move left
        {
            if (transform.position.x > minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                comeFromRight = true;

                direction = Random.Range(3, 6);

            }
            else if (transform.position.x <= minX)
            {

                direction = 5;


            }

        }
        else if (direction == 5) //move down
        {
            GoDown();
        }


        Invoke("CreateRoom", startTimeBetweenRoom - 0.05f);

    }

    private void GoDown()
    {
        if (transform.position.y > minY)
        {
            downCounter++;
            justGotDown = true;
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
            transform.position = newPos;

            if (downCounter >= 1 && transform.position.x >= maxX)
            {
                direction = 3;
            }
            else if (downCounter >= 1 && transform.position.x <= minX)
            {
                direction = 1;

            }
            else
            {
                direction = Random.Range(1, 5);
            }
        }

        /*if(transform.position.y<=minY&&direction==5)
        {
            
            stopGeneration = true;
        }*/
    }

    private void CreateRoom()
    {


        int rand = Random.Range(0, rooms.Length);
        int justDownWhatNext = Random.Range(1, 3);
        if ((direction == 1 || direction == 2 || direction == 3 || direction == 4) && (comeFromLeft == true || comeFromRight == true) && justGotDown == false && isAtBound == false)
        {

            KillBool();

            Instantiate(rooms[4], transform.position, Quaternion.identity);
            generatedCount++;
        }
        else if ((direction == 5) && (comeFromLeft == true))
        {

            KillBool();

            Instantiate(rooms[0], transform.position, Quaternion.identity);
            generatedCount++;

        }
        else if ((direction == 5) && (comeFromRight == true))
        {

            KillBool();

            Instantiate(rooms[3], transform.position, Quaternion.identity);
            generatedCount++;

        }
        /*else if((direction == 5) && (comeFromRight == true)&&isAtBound==true)
        {
            KillBool();

            Instantiate(rooms[3], transform.position, Quaternion.identity);
            generatedCount++;
            
        }
        else if ((direction == 5) && (comeFromLeft == true) && isAtBound == true)
        {
            KillBool();

            Instantiate(rooms[0], transform.position, Quaternion.identity);
            generatedCount++;
            Debug.Log("bbbbbbbbbbbbbb");
        }*/
        else if ((direction == 1 || direction == 2) && (justGotDown == true))
        {
            KillBool();

            Instantiate(rooms[2], transform.position, Quaternion.identity);
            generatedCount++;
        }
        else if ((direction == 3 || direction == 4) && (justGotDown == true))
        {
            KillBool();

            Instantiate(rooms[1], transform.position, Quaternion.identity);
            generatedCount++;
        }
        /*else if(justGotDown==true&&direction==5&&isAtBound==true)
        {
            KillBool();

            Instantiate(rooms[6], transform.position, Quaternion.identity);
            generatedCount++;
        }*/
        else if ((direction == 1 || direction == 2 || direction == 3 || direction == 4 || direction == 5) && isAtBound == true && downCounter >= 1 && justGotDown == true)
        {
            KillBool();

            Instantiate(rooms[6], transform.position, Quaternion.identity);
            generatedCount++;

        }

        /*else if(gonnaBump==true&&(direction==1||direction==2))
        {
            KillBool();

            Instantiate(rooms[3], transform.position, Quaternion.identity);
            generatedCount++;
            Debug.Log("bbaaaaaaaaaaaaaaaaaabb");
        }
        else if (gonnaBump == true && (direction == 3 || direction == 4))
        {
            KillBool();

            Instantiate(rooms[0], transform.position, Quaternion.identity);
            generatedCount++;
            Debug.Log("bbbbbbbbbbbbbb");

        }
        else if(isAtBound==true&&gonnaBump==true && (comeFromLeft==true))
        {
            KillBool();

            Instantiate(rooms[3], transform.position, Quaternion.identity);
            generatedCount++;
        }
        else if (isAtBound == true && gonnaBump == true && (comeFromRight == true))
        {
            KillBool();

            Instantiate(rooms[3], transform.position, Quaternion.identity);
            generatedCount++;
        }*/
        

    }
    public void CreateFinalRoom()
    {

        if (direction==1||direction==2)
        {
            downCounter = 0;
            Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
            transform.position = newPos;
            Instantiate(rooms[7], transform.position, Quaternion.identity);
            KillBool();
            Debug.Log("case1");
        }
        else if (direction==3||direction==4)
        {
            downCounter = 0;
            Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
            transform.position = newPos;
            Instantiate(rooms[5], transform.position, Quaternion.identity);
            KillBool();
            Debug.Log("case2");
        }
        else if (direction==5||downCounter <= 1)
        {
            downCounter = 0;
            comeFromLeft = true;
            Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
            transform.position = newPos;

            int randFinish = Random.Range(0, 2);

            if (randFinish == 0)
            {
                Instantiate(rooms[2], transform.position, Quaternion.identity);
                Instantiate(rooms[7], new Vector2(transform.position.x + moveAmount, transform.position.y), Quaternion.identity);

                KillBool();
                Debug.Log("case3.1");
            }
            else if(randFinish==1)
            {
                Instantiate(rooms[1], transform.position, Quaternion.identity);
                Instantiate(rooms[5], new Vector2(transform.position.x - moveAmount, transform.position.y), Quaternion.identity);

                KillBool();
                Debug.Log("case3.2");
            }
        }

    }
    public void KillBool()
    {
        gonnaBump = false;
        comeFromLeft = false;
        comeFromRight = false;
        justGotDown = false;
        
    }

    
    
}
