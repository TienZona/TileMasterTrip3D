using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameObject queueA;
    public int id;
    public string type;
    void Start()
    {
        queueA = GameObject.Find("Queue");
     
    }

    private void Update()
    {
        float x = transform.rotation.x;
        float y = transform.rotation.y;
        float z = transform.rotation.z;
        if ((x >= 90 && x <= 270) || (x >= -270 && x <= -90)) {
            transform.rotation = Quaternion.Euler(0, y, z);
            
        }
    }


    private void OnMouseDown()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();    
        rb.useGravity = false;
        rb.isKinematic = true;
        gameObject.transform.position = new Vector3 (gameObject.transform.position.x, 1.0f, gameObject.transform.position.z);
        gameObject.transform.rotation = Quaternion.identity;


        queueA.GetComponent<Queue>().addItem(this);
        SoundManager.instance.playButtonClick();

    }

    private void OnMouseEnter()
    {
       
        transform.rotation  = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, 0));

        
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Outline>().outlineWidth = 0f;
    }

    private void removeItem()
    {
        Destroy(gameObject);
    }
  
    void OnDestroy()
    {
        /*Game_Manager.instance.checkWinGame();*/
    }

}
