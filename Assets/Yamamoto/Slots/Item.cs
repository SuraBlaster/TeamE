using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Slot slot;
    private Canvas canvas;
    private Vector2 originalPos;
    private GameObject player;

    public GameObject charge;
    public int count = 1;
    public int itemId;
    public GameObject weaponPrefab;
    public GameObject nextWeaponPrefab;
    public Transform slotsParent;
    public TextMeshProUGUI countText;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPos = rectTransform.anchoredPosition;

        if (slot == null)
        {
            slot = FindObjectOfType<Slot>();
        }
        slot.AddItem(this);

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        UpdateCountUI();
    }

    void Update()
    {
        
    }

    public void ChangeNextWeapon()
    {
        if (nextWeaponPrefab == null) return;
        GameObject weapon = Instantiate(nextWeaponPrefab, slotsParent.transform.position, Quaternion.identity, slotsParent.transform);
        weapon.GetComponent<Item>().slotsParent = this.slotsParent;
        DestroySelf();
    }

    public void UpdateCountUI()
    {
        if (countText != null)
        {
            if (nextWeaponPrefab == null)
                countText.text = "";
            else
                countText.text = count.ToString();
        }
    }

    public IEnumerator MoveToPosition(Vector3 targetPos)
    {
        if (rectTransform == null) yield break;

        float duration = 0.2f;
        Vector2 startPos = rectTransform.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (rectTransform == null) yield break;

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

    private IEnumerator MoveBackToOriginal()
    {
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

        // Chargeのワールド座標の四隅を取得
        Vector3[] corners = new Vector3[4];
        chargeRect.GetWorldCorners(corners);

        // Chargeの当たり判定
        bool chargeFlag = (localPoint.x >= corners[0].x && localPoint.x <= corners[2].x &&
                       localPoint.y >= corners[0].y && localPoint.y <= corners[2].y);

        Debug.Log($"判定: {chargeFlag}, マウス={localPoint}, Charge範囲=({corners[0]} - {corners[2]})");

        // Player の当たり判定（Collider 必須）
        bool playerFlag = false;
        Collider2D playerCol = player.GetComponent<Collider2D>();
        if (playerCol != null)
        {
            // マウスのスクリーン座標をワールド座標に変換
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
                new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane)
            );

            playerFlag = playerCol.OverlapPoint(mouseWorld);
        }

        Debug.Log($"判定: Player={playerFlag}, マウス={eventData.position}, Position={player.transform.position}");

        if (chargeFlag)
        {
            charge.GetComponent<Charge>().AddChargePoint();
            Destroy(gameObject);
        }
        else if (playerFlag)
        {
            int size = 0;
            while (size != count)
            {
                Instantiate(weaponPrefab, player.transform.position, Quaternion.identity, player.transform);
                size++;
            }
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(MoveBackToOriginal());
        }
    }
}