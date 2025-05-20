using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float startSpeed = 0.5f;
    [SerializeField] private float acceleration = 0.1f;

    [Header("Blinker Stuff")]
    [SerializeField] private Image leftBlinker;
    [SerializeField] private Image rightBlinker;
    [SerializeField] private Color startingBlinkerColor;
    [SerializeField] private Color activeBlinkerColor;
    [SerializeField] private AudioSource leftBlinkerSFX;
    [SerializeField] private AudioSource rightBlinkerSFX;

    private float currentSpeed;
    private int numberOfLanes;
    private int currentLane; 
    private float laneWidth;
    private bool needToResetBlinker = false;
    private int blinker = 0; // -1 = left 0 = none +1 = right

    private void Start()
    {
        currentSpeed = startSpeed;
    }

    private void Update()
    {
        UpdateSpeed();
        UpdateBlinker();
        UpdateUI();
        ChangeLanes();
        Move();
    }

    public void MoveToSpawn(Vector3 startLocation)
    {
        gameObject.transform.position = startLocation;
    }

    public void LoadLaneInfo(int lanes, float width)
    {
        numberOfLanes = lanes;
        laneWidth = width;
        currentLane = 0;
    }

    private void UpdateSpeed()
    {
        if (Input.GetKey("w"))
        {
            currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.deltaTime, minSpeed, maxSpeed);
        }
        if (Input.GetKey("s"))
        {
            currentSpeed = Mathf.Clamp(currentSpeed - acceleration * Time.deltaTime, minSpeed, maxSpeed);
        }
    }

    private void Move()
    {
        Vector3 moveAmount = new Vector3(0, currentSpeed, 0);
        gameObject.transform.Translate(moveAmount);
    }

    private void ChangeLanes()
    {
        if (Input.GetKeyDown("d") && blinker >= 1 && !needToResetBlinker)
        {
            if (numberOfLanes > currentLane + 1)
            {
                transform.Translate(new Vector3(laneWidth, 0, 0));
                currentLane++;
                blinker = 0;
            }
        }
        if (Input.GetKeyDown("a") && blinker <= -1 && !needToResetBlinker)
        {
            if (currentLane > 0)
            {
                transform.Translate(new Vector3(-laneWidth, 0, 0));
                currentLane--;
                blinker = 0;
            }
        }
    }

    private void UpdateBlinker()
    {
        if (Input.GetMouseButtonDown(0))
        {
            blinker = -1;
        }
        if (Input.GetMouseButtonDown(1))
        {
            blinker = 1;
        }
        if (Input.GetKeyDown("space"))
        {
            blinker = 0;
        }
    }

    private void UpdateUI()
    {
        switch (blinker)
        {
            case 0:
                {
                    leftBlinker.GetComponent<Image>().color = startingBlinkerColor;
                    rightBlinker.GetComponent<Image>().color = startingBlinkerColor;
                    leftBlinkerSFX.Stop();
                    rightBlinkerSFX.Stop();
                    break; 
                }
            case 1: 
                {
                    leftBlinker.GetComponent<Image>().color = startingBlinkerColor;
                    rightBlinker.GetComponent<Image>().color = activeBlinkerColor;
                    leftBlinkerSFX.Stop();
                    if (!rightBlinkerSFX.isPlaying)
                    {
                        rightBlinkerSFX.Play();
                    }
                    break; 
                }
            case -1: 
                {
                    leftBlinker.GetComponent<Image>().color = activeBlinkerColor;
                    rightBlinker.GetComponent<Image>().color = startingBlinkerColor;
                    rightBlinkerSFX.Stop();
                    if (!leftBlinkerSFX.isPlaying)
                    {
                        leftBlinkerSFX.Play();
                    }
                    break; 
                }
            default: 
                {
                    leftBlinker.GetComponent<Image>().color = startingBlinkerColor;
                    rightBlinker.GetComponent<Image>().color = startingBlinkerColor;
                    leftBlinkerSFX.Stop();
                    rightBlinkerSFX.Stop();
                    Debug.LogWarning("Blinker is not a valid value rn");
                    break; 
                }
        }
    }
}
