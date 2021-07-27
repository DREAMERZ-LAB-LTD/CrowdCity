using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform[] childArray = GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < childArray.Length; i++)
        {
         

            childArray[i].GetComponent<Rigidbody>().isKinematic = true;
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Transform[] childArray = GetComponentsInChildren<Transform>(true);

            for (int i = 0; i < childArray.Length; i++)
            {


                childArray[i].GetComponent<Rigidbody>().isKinematic = false;

            }
            StartCoroutine(Destroy());
        }
    }


    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3);
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
