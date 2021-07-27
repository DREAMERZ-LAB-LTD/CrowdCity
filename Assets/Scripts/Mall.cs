using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine.UI;
public class Mall : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
      float count;

    public Text cointText;
    public  bool played;
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("npc"))
        {
         
            if (other.gameObject.GetComponent<NavMeshAgent>().enabled == false)
            {
                count += .5f * Time.deltaTime;
                anim.Play("Mall");
                cointText.text = count.ToString("0");
                StartCoroutine(startTime());
            }
        }
    }


    public IEnumerator startTime()
    {
        yield return new WaitForSeconds(4);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        anim.enabled = false;
        played = true;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
