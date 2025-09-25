using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    private Slot slot;
    private HasItem hasItem;
    private Camera camera;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 originalPos;

    [HideInInspector] public bool isTrash = false;
    [HideInInspector] public bool isCharge = false;
    void Start()
    {
        camera = Camera.main;

        if (slot == null)
        {
            slot = FindObjectOfType<Slot>();
        }
        slot.AddItem(this);
    }

    public IEnumerator MoveToPosition(Vector3 targetPos)
    {
        float duration = 0.2f;
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        originalPos = targetPos;
    }

    public void SetSlot(Slot slot)
    {
        this.slot = slot;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        isDragging = true;
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        offset = transform.position - mousePos;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (isTrash)
        {
            if (isCharge)
            {
                slot.AddChargePoint();
            }

            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(MoveBackToOriginal());
        }

    }

    private IEnumerator MoveBackToOriginal()
    {
        float duration = 0.2f;
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, originalPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
}