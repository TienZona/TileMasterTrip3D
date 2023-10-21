using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Queue : MonoBehaviour
{
    public GameObject bananas;
    public List<GameObject> ListSlot;
    public List<GameObject> items;
    public List<Slot> slots = new List<Slot>();
    private int numberSlot = 0;
    private GameObject firstItem;
    Coroutine coroutine;

    void Start()
    {
        float start = -3.1f;
        foreach (GameObject slot in ListSlot)
        {
            float lenght = ListSlot.Count;
            float step = 1f;
            Vector3 position = gameObject.transform.position;
            slot.transform.position = new Vector3 (position.x + start, position.y + 0.001f, position.z);
            start += step;
            slot.transform.localScale = new Vector3(0.13f, 0.001f, 0.8f);
        }

        foreach(var item in ListSlot.Select((slot, i) => (slot, i)))
        {
       
            Slot slot = new Slot(item.slot, item.i);
            slots.Add(slot);
        }

     
    
    }
    void Update()
    {
       
    
    
    }

   
    public void addItem(Tile item)
    {

    
        if (numberSlot < ListSlot.Count)
        {
            int index = checkTileHasInArray(item);
            if (index == -1){
                
                slotsMoveNext();
                slots[0].SetTile(item);
                LeanTween.moveLocal(item.gameObject, slots[0].position, 0.5f);
            }
            else
            {
                slotMoveStartIndex(index + 1);
                slots[index + 1].SetTile(item);
                LeanTween.moveLocal(item.gameObject, slots[index + 1].position, 0.5f);
            }


             AddNumberSlot();
        }

        if (numberSlot >= 3)
        {
            int posTribble = checkTribbleInArray();
            if (posTribble != -1)
            {
                DestroyTribbleTile(posTribble);
                Game_Manager.instance.handleScore();
            }
        }


        if (numberSlot == ListSlot.Count)
        {
            if (checkLose())
            {
                Debug.Log("You Lose");
                numberSlot = 0;
                Game_Manager.instance.loseGame();
            }
        }
    }

    private bool checkLose()
    {

        return checkTribbleInArray() == -1;
    }

    private void AddNumberSlot()
    {
        numberSlot++;
    }

    private void DestroyTribbleTile(int index)
    {
        numberSlot -= 3;
        Game_Manager.instance.numberTile -= 3;
        destroyItem(index);
        destroyItem(index + 1);
        destroyItem(index + 2);

        for (int i = index + 3; i <= numberSlot + 2; i++)
        {
            moveItemBack(i);
        }
        SoundManager.instance.PlayComboSound();
        Game_Manager.instance.checkWinGame();
    }

    private void destroyItem(int index)
    {
        Destroy(slots[index].tile.gameObject);
        slots[index].tile = null;
    }

    private void moveItemBack(int index)
    {
        Vector3 pos = slots[index - 3].slot.transform.position;
        Tile tile = slots[index].tile;
        LeanTween.move(tile.gameObject, pos, 0.2f);
        slots[index - 3].tile = slots[index].tile;
        slots[index].tile = null;

    }

    private int checkTribbleInArray()
    {
        int index = -1;
        for (int i = 0; i < numberSlot-2; i++)
        {
            if (slots[i].tile != null && slots[i + 1].tile != null && slots[i +2].tile != null)
            {
                
                var first = slots[i].tile.id;
                var second = slots[i + 1].tile.id;
                var third = slots[i + 2].tile.id;


                if (first == second && second == third)
                {
                    index = i;
                }
            }

        }
        return index;
    }

    private int checkTileHasInArray(Tile item)
    {
        int index = -1;
        foreach (var slot in slots.Select((value, i) => (value, i)) )
        {
            if(slot.value.tile != null)
            {
                if (slot.value.tile.id == item.id)
                {
                    index = slot.i;
                }
               
            }

            
        }
        return index;
    }


    private void slotMoveStartIndex(int index)
    {
        for (int i = slots.Count - 2; i >= index; i--)
        {
            moveItemNext(i);

        }
    }

    private void slotsMoveNext()
    {
 
        for (int i = slots.Count - 2; i >= 0; i--)
        {
            moveItemNext(i);
        }
    }

    private void moveItemNext(int index)
    {
        Vector3 pos = slots[index + 1].slot.transform.position;
        Tile tile = slots[index].tile;
        if(tile != null)
        {
            LeanTween.move(tile.gameObject, pos, 0.2f);
            slots[index + 1].tile = slots[index].tile;
            slots[index].tile = null;
        }
    }

}
