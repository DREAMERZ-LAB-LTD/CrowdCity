using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scriptables
{
    [CreateAssetMenu(menuName = "Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public Vector2 xSize, zSize;
        
        public int maxAmountOfPlayers;
        public GameObject playerPrefab, aiPrefab, npcPrefab;
 
        
        public int startAmountOfNpc, maxAmountOfNpc;
        public int followersForCapturing;
        
        [NonSerialized] public List<PlayerConfiguration> players = new List<PlayerConfiguration>();
        [NonSerialized] public List<CustomItem<string>> nicknameItems;
        [NonSerialized] public List<CustomItem<Skin>> skinItems;
        
      
     

        public CustomItem<Skin> RandomSkinTable()
        {
            CustomItem<Skin>[] available = skinItems.Where(x => !x.taken).ToArray();

            if (available.Length == 0)
                return null;
            
            return available[Random.Range(0, available.Length)];
        }
        
        public CustomItem<string> RandomNicknameTable()
        {
            CustomItem<string>[] available = nicknameItems.Where(x => !x.taken).ToArray();

            if (available.Length == 0)
                return null;
            
            return available[Random.Range(0, available.Length)];
        }
        
 

        

        
        public int PrevAvailableIndexColor(int i)
        {
            return PreviousIndex(skinItems, i);
        }
   

        private int PreviousIndex<T>(List<CustomItem<T>> list, int i)
        {
            int index = i;
            
            while (true)
            {
                if (index < 0)
                    index = list.Count - 1;
                
                if (list[index].taken)
                {
                    index--;
                    continue;
                }

                return index;
            }
        }
    }

    [System.Serializable]
    public class CustomItem<T>
    {
        public T value;
        public bool taken;

        public CustomItem(T v)
        {
            value = v;
            taken = false;
        }
    }
}