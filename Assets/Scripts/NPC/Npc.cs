using System;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
public class Npc : MonoBehaviour
{
    public NpcState walkerState, followerState;
    public LayerMask layerMask;

    public NpcState _currentState;
    private Leader _leader;
    private NavMeshAgent _agent;
    private Vector3 _destination;
    private Renderer _renderer;
    private GameObject[] enemies;
    private Transform targetedEnemy;
    #region Properties

    public bool IsWalker => _currentState == walkerState;
    bool lootState;
    GameObject g;


    private void Start()
    {
        g = GameObject.Find("mall");
    }
    public Leader Leader
    {
        get => _leader;
        set
        {
            _leader = value;
            /*_renderer.material.color = _leader.CurrentSkin;*/
        }
    }

    public Vector3 Destination
    {
        get => _destination;
        set
        {
            _destination = value;
            if (_destination != null)
            _agent.SetDestination(_destination);
        }
    }

    public bool ReachedDestination => _agent.velocity == Vector3.zero;

    public NavMeshAgent Agent => _agent;

    #endregion

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        Destination = transform.position;

        _renderer = GetComponent<Renderer>();

        _currentState = walkerState;
        _currentState.OnStateStart(this);
    }

    private void Update()
    {
        _currentState.Execute(this);
        //Agent.speed = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>().value;
        if (FindObjectOfType<Mall>() != null)
        {
            if (lootState && !FindObjectOfType<Mall>().played)
            {
                transform.position = Vector3.MoveTowards(transform.position, g.transform.GetChild(0).transform.position, 10 * Time.deltaTime);
                this.GetComponent<NavMeshAgent>().enabled = false;
            }
            if (FindObjectOfType<Mall>().played)
                this.GetComponent<NavMeshAgent>().enabled = true;

        }

    }
    private void FixedUpdate()
    {

        if (GameObject.Find("Police(Clone)") != null)
        {
            if (_currentState == followerState)
            {
                enemies = GameObject.FindGameObjectsWithTag("Police");
                print(enemies);
                targetedEnemy = enemies[Random.Range(0, enemies.Length)].transform;

                float dist = Vector3.Distance(targetedEnemy.position, transform.position);
                if (dist < 10)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetedEnemy.position, 8 * Time.deltaTime);

                }



            }
        }
    }

    public void SetLeader(Leader lead)
    {
        //TODO: shit
        if (_currentState != followerState)
        {
            _currentState.OnStateEnd(this);
            _currentState = followerState;
            _currentState.OnStateStart(this);
        }
        else
        {
            _currentState.OnNewLeader(this);
        }

        Leader = lead;
    }

    public void CheckCollider()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, 1.2f, layerMask))
        {
            if (col.TryGetComponent(out Npc npc))
            {
                if(npc.IsWalker)
                {
                    Leader.AddFollower(npc);
                }
                else
                {
                    if(Leader == npc.Leader || npc.Leader.Followers == Leader.Followers) continue;
                    
                    if(npc.Leader.Followers < Leader.Followers)
                        Leader.AddFollower(npc);
                    else
                        npc.Leader.AddFollower(this);
                }
            }
                
            if (col.TryGetComponent(out Leader leader))
            {
                if(leader.Followers == 1)
                    Leader.KillPlayer(leader);
                else
                    leader.AddFollower(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Police"))
        {
            if (_currentState == followerState)
            {
                FindObjectOfType<Player.Leader>()._followers--;
                Destroy(other.gameObject);
                Destroy(this.gameObject);

            }


        }
        if (other.gameObject.CompareTag("Mall"))
        {
            if (_currentState == followerState)
            {
                lootState = true;
            }
       

        }
    }


 

}