using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Tile tile;
    public GameObject slot;
    public int slotIndex;
    public Vector3 position;

    public Slot(GameObject slot, int index)
    {
        this.slot = slot;
        position = slot.transform.position;
        slotIndex = index;
    }

    public void SetTile(Tile tile)
    {
        this.tile = tile;
    }
}
