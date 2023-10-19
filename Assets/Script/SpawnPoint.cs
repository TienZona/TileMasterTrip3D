using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField, Range(0, 360f)]
    float rotationSpeed;

    float force;

    [SerializeField] Transform Point;
    GameObject obj;
    public void Spawn()
    {
        StartCoroutine(SpawnTile());
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.fixedDeltaTime);
    }

    IEnumerator SpawnTile()
    {
        var tiles = Game_Manager.instance.levels[0].ListTile;

        int number = Game_Manager.instance.number;
        for(int i = 0; i < number; i++)
        {
            int index = i % tiles.Count;
            for (int j = 0; j < 3; j++)
            {
                
                obj = Instantiate(tiles[index], Point.position, Point.rotation);
                Game_Manager.instance.numberTile++;

                force = UnityEngine.Random.Range(5f, 10f);

                obj.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.VelocityChange);

                obj.transform.rotation = Quaternion.Euler(90, 0f, 0f);
                yield return new WaitForSeconds(0.15f);
            }
        }
    
        gameObject.SetActive(false);
    }

}