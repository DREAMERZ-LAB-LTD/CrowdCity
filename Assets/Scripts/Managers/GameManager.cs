using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using DefaultNamespace;
using Player;
using Scriptables;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text countdowns;
    public int timerText;



    public GameSettings settings;
    public PlayerController playerController;
    public Player.Leader leader;
    public bool start;
    public void Awake()
    {
        GameResources.gm = this;
       

   
    }
    private void Start()
    {
        StartCoroutine(Countdown());
    }
    void Update()
    {

    }

    public IEnumerator Countdown()
    {
        countdowns.text = "3";
        yield return new WaitForSeconds(1);
        countdowns.text = "2";
        yield return new WaitForSeconds(1); 
        countdowns.text = "1";
        yield return new WaitForSeconds(1); 
        countdowns.text = "GO";
        yield return new WaitForSeconds(1);
        countdowns.gameObject.SetActive(false);
        start = true;
    }


}
