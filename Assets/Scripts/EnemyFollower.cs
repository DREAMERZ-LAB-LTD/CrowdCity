using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DefaultNamespace;
public class EnemyFollower : MonoBehaviour
{
    NavMeshAgent enemy;
   Transform player;
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        player = GameResources.gm.playerController.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        if(dist < 10)
        enemy.SetDestination(player.position);
    }
  

}
