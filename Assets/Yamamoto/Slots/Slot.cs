using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Vector2 startItemPos;
    public Vector2 itemSpacePos;
    public int chargePoint;

    private const int MaxWidth = 8;
    private Item[] items = new Item[MaxWidth];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CompactItems();
    }

    public void AddItem(Item item)
    {
        int emptyIndex = FindLeftmostEmpty();
        if (emptyIndex == -1)
        {
            RemoveOldestItem();
            emptyIndex = FindLeftmostEmpty();
        }

        items[emptyIndex] = item;
        MoveItemToSlot(item, emptyIndex);
    }

    private int FindLeftmostEmpty()
    {
        for (int i = 0; i < MaxWidth; i++)
        {
            if (items[i] == null) return i;
        }
        return -1;
    }

    private void RemoveOldestItem()
    {
        if (items[0] != null)
        {
            items[0].DestroySelf();
        }

        items[0] = null;

        // ‹ó‚«‚ð‹l‚ß‚é
        CompactItems();
    }


    private void MoveItemToSlot(Item item, int xIndex)
    {
        StartCoroutine(item.MoveToPosition(new Vector3(startItemPos.x + itemSpacePos.x * xIndex, startItemPos.y, 0)));
    }

    public void CompactItems()
    {
        int targetIndex = 0;

        for (int i = 0; i < MaxWidth; i++)
        {
            if (items[i] != null)
            {
                if (i != targetIndex)
                {
                    items[targetIndex] = items[i];
                    items[i] = null;

                    MoveItemToSlot(items[targetIndex], targetIndex);
                }
                targetIndex++;
            }
        }
    }

    public void AddChargePoint()
    {
        chargePoint += 1;
    }
}