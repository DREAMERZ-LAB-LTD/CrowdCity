using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiControls : MonoBehaviour
{
    public Slider speed;
    // Start is called before the first frame update

    private void Update()
    {
        speed.value = FindObjectOfType<PlayerController>().speed;
    }


  
    
}
