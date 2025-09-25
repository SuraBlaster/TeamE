using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasItem : MonoBehaviour
{
    public List<Item> items;

    // Start is called before the first frame update
    void Start()
    {
        items = new List<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            // ‰¼
            item.DestroySelf();
        }
    }
}
