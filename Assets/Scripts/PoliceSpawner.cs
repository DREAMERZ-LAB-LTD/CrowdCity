using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
public class PoliceSpawner : MonoBehaviour
{
    public Transform[] pos;
    public GameObject police;
    public GameObject[] g;

    public Player.Leader leader;
    int a;


    bool wave1, wave2, wave3;
    void Start()
    {
      
           


    }

    // Update is called once per frame
    void Update()
    {
        if(leader._followers > 15 && leader._followers <= 17)
        {
            if (!wave1)

            {
                int j = Random.Range(0, pos.Length);
                for (int i = 0; i < leader._followers - 10; i++)
                {
                    GameObject clone = Instantiate(police, pos[j].transform.position, Quaternion.identity) as GameObject;


                }
     
                wave1 = true;

            }
        }
        if (leader._followers > 28 && leader._followers <= 30)
        {
            if (!wave2)

            {
                int j = Random.Range(0, pos.Length);
                for (int i = 0; i < leader._followers - 10; i++)
                {
                    GameObject clone = Instantiate(police, pos[j].transform.position, Quaternion.identity) as GameObject;


                }

                wave2 = true;

            }
        }
        if (leader._followers > 35 && leader._followers <= 37)
        {
            if (!wave3)

            {
                int j = Random.Range(0, pos.Length);
                for (int i = 0; i < leader._followers - 10; i++)
                {
                    GameObject clone = Instantiate(police, pos[j].transform.position, Quaternion.identity) as GameObject;


                }

                wave3 = true;

            }
        }
    }
}
