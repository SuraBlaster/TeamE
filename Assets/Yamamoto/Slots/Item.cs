using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //private Slot slot;
    //private Camera camera;
    //private bool isDragging = false;
    //private Vector3 offset;
    //private Vector3 originalPos;

    private RectTransform rectTransform;
    private Slot slot;
    private Canvas canvas;
    private Vector2 originalPos;

    public Charge charge;

    //[HideInInspector] public bool isTrash = false;
    //[HideInInspector] public bool isCharge = false;
    //[HideInInspector] public bool takeWeapon = false;
    void Start()
    {
        //camera = Camera.main;
        //
        //if (slot == null)
        //{
        //    slot = FindObjectOfType<Slot>();
        //}
        //slot.AddItem(this);

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPos = rectTransform.anchoredPosition;

        if (slot == null)
        {
            slot = FindObjectOfType<Slot>();
        }
        slot.AddItem(this);
    }

    void Update()
    {
        
    }

    public IEnumerator MoveToPosition(Vector3 targetPos)
    {
        //float duration = 0.2f;
        //Vector3 startPos = transform.position;
        //float elapsed = 0f;
        //
        //while (elapsed < duration)
        //{
        //    transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
        //    elapsed += Time.deltaTime;
        //    yield return null;
        //}
        //
        //targetPos.z = -2;
        //
        //transform.position = targetPos;
        //originalPos = targetPos;

        float duration = 0.2f;
        Vector2 startPos = rectTransform.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPos;
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

    //void OnMouseDown()
    //{
    //    isDragging = true;
    //    Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    //    mousePos.z = -2;
    //    offset = transform.position - mousePos;
    //}

    //void OnMouseDrag()
    //{
    //    if (isDragging)
    //    {
    //        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    //        mousePos.z = -2;
    //        transform.position = mousePos + offset;
    //    }
    //}

    //void OnMouseUp()
    //{
    //    isDragging = false;
    //
    //    if (takeWeapon)
    //    {
    //        Destroy(GetComponent<Item>());
    //    }
    //
    //    //if (isTrash)
    //    //{
    //    //    if (isCharge)
    //    //    {
    //    //        slot.AddChargePoint();
    //    //    }
    //    //
    //    //    Destroy(gameObject);
    //    //}
    //    //else
    //    {
    //        StartCoroutine(MoveBackToOriginal());
    //    }
    //
    //}

    private IEnumerator MoveBackToOriginal()
    {
        //float duration = 0.2f;
        //Vector3 startPos = transform.position;
        //float elapsed = 0f;
        //
        //while (elapsed < duration)
        //{
        //    transform.position = Vector3.Lerp(startPos, originalPos, elapsed / duration);
        //    elapsed += Time.deltaTime;
        //    yield return null;
        //}
        //
        //transform.position = originalPos;

        float duration = 0.2f;
        Vector2 startPos = rectTransform.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, originalPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = originalPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 手前に出す
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // マウスに追従
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPoint))
        {
            rectTransform.anchoredPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform chargeRect = charge.GetComponent<RectTransform>();

        // マウスのスクリーン座標を Canvas ローカル座標に変換
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPoint
        );

        // Charge のワールド座標の四隅を取得
        Vector3[] corners = new Vector3[4];
        chargeRect.GetWorldCorners(corners);

        bool inside = (localPoint.x >= corners[0].x && localPoint.x <= corners[2].x &&
                       localPoint.y >= corners[0].y && localPoint.y <= corners[2].y);

        Debug.Log($"判定: {inside}, マウス={localPoint}, Charge範囲=({corners[0]} - {corners[2]})");

        if (inside)
        {
            charge.AddChargePoint();
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(MoveBackToOriginal());
        }
    }
}