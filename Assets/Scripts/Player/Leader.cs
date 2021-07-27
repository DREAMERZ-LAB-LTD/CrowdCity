using System;
using System.Collections.Generic;
using System.Linq;
using Scriptables;
using UnityEngine;

namespace Player
{
    public class Leader: MonoBehaviour
    {
        public LayerMask layerMask;
        public BoolVariable checkCollider;

        
        public int _followers = 1;

        private bool _isInit;

        public int Followers
        {
            get => _followers;
            set => _followers = value;
        }


        public int LeaderboardPlace { get; set; }
  

  

        public void KillPlayer(Leader leader)
        {
          //  FindObjectOfType<GameManager>().DestroyLeader(leader);
            FindObjectOfType<NpcSpawner>().SpawnFollowers(this);
        }

        public void AddFollower(Npc npc)
        {
            npc.SetLeader(this);
            _followers++;
        }
    
        private void Update()
        {
            CheckCollider();
        }
        
        
        private void CheckCollider()
        {
            if(!checkCollider.value) return;
            
            foreach (Collider col in Physics.OverlapSphere(transform.position, 2, layerMask))
            {
                if (col.TryGetComponent(out Npc npc))
                {
                    if(npc.IsWalker || npc.Leader != this && npc.Leader.Followers < _followers)
                        AddFollower(npc);
                    
                    continue;
                }
                
                if (col.TryGetComponent(out Leader leader))
                {
                    if(leader != this && leader.Followers == 1)
                        KillPlayer(leader);
                }
            }
        }
    }

}