using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField, Range(0, 360f)]
    float rotationSpeed;

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
        int level = Game_Manager.instance.levelObj.level;
        var tiles = Game_Manager.instance.levels[level-1].ListTile;
        int number = Game_Manager.instance.number;

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < number; i++)
        {
            int index = i % tiles.Count;
            for (int j = 0; j < 3; j++)
            {
                list.Add(tiles[index]);
            }
        }

        float force = 8f;
        float step = 4f / (float)(number * 3);

        for (int i = 0; i < number; i++)
        {
          
            for (int j = 0; j < 3; j++)
            {
                int randomNumber = UnityEngine.Random.Range(0, list.Count - 1);
                obj = Instantiate(list[randomNumber], Point.position, Point.rotation);
                list.Remove(list[randomNumber]);
                obj.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.VelocityChange);

                obj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                force -= step;
                yield return new WaitForSeconds(0.05f);
            }
        }
    
        gameObject.SetActive(false);
    }

}