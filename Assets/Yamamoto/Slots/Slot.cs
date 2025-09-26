using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Vector2 startItemPos;
    public Vector2 itemSpacePos;
    //public int chargePoint;

    private const int MaxWidth = 7;
    private Item[] items = new Item[MaxWidth];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // まず隙間を詰める
        CompactItems();

        // マージが発生しなくなるまで繰り返す
        bool merged;
        do
        {
            merged = false;
            for (int i = 0; i < MaxWidth - 1; i++)
            {
                if (items[i] != null && items[i + 1] != null)
                {
                    if (items[i].itemId == items[i + 1].itemId)
                    {
                        MergeItems(i, i + 1);
                        CompactItems();
                        merged = true;
                        break; // 配列が変わったのでループをやり直す
                    }
                }
            }
        } while (merged);
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

        // 空きを詰める
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

    private void CheckAndRemoveAdjacent(int index)
    {
        Item current = items[index];
        if (current == null) return;

        // 左隣をチェック
        if (index > 0 && items[index - 1] != null)
        {
            if (items[index - 1].itemId == current.itemId)
            {
                MergeItems(index, index - 1);
            }
        }

        // 削除後に詰め直す
        CompactItems();
    }

    private void MergeItems(int baseIndex, int targetIndex)
    {
        Item baseItem = items[baseIndex];
        Item targetItem = items[targetIndex];

        baseItem.count = baseItem.count + targetItem.count;

        baseItem.count = Mathf.Min(baseItem.count, 999);

        baseItem.UpdateCountUI();

        targetItem.DestroySelf();
        items[targetIndex] = null;
    }
}