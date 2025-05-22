using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ColisionEn " + other.gameObject.name);
        GameObject.Find("GameManager").GetComponent<UIManagerJuego>().gemaEncontrada();
        Destroy(other.gameObject);

    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("ColisionEx " + other.gameObject.name);
    }

}
