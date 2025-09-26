using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject prefabRed;
    public Item prefabGreen;
    public Item prefabBlue;
    public Transform slotsParent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate(prefabRed, slotsParent);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(prefabBlue, slotsParent);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(prefabGreen, slotsParent);
        }
    }
}
