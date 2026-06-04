using UnityEngine;

/// <summary>
/// 石頭交互系統
/// 負責檢測玩家靠近並處理撿起邏輯
/// </summary>
public class RockInteraction : MonoBehaviour
{
    [Header("交互設置")]
    [SerializeField] private float detectionRange = 3f; // 檢測範圍
    [SerializeField] private KeyCode pickupKey = KeyCode.F; // 撿起按鍵
    
    [Header("引用")]
    [SerializeField] private InteractionUI interactionUI; // UI 管理器
    
    private Transform playerTransform; // 玩家位置
    private bool isPlayerNearby = false; // 玩家是否在附近
    
    private void Start()
    {
        // 尋找玩家（假設玩家標籤為 "Player"）
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("找不到玩家！請確保玩家 GameObject 標籤為 'Player'");
        }
        
        // 獲取 InteractionUI 引用
        if (interactionUI == null)
        {
            interactionUI = FindObjectOfType<InteractionUI>();
        }
    }
    
    private void Update()
    {
        if (playerTransform == null) return;
        
        // 計算與玩家的距離
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        
        // 檢查玩家是否在範圍內
        bool shouldShowPrompt = distance <= detectionRange;
        
        // 如果狀態改變，更新 UI
        if (shouldShowPrompt != isPlayerNearby)
        {
            isPlayerNearby = shouldShowPrompt;
            if (interactionUI != null)
            {
                interactionUI.ShowPrompt(isPlayerNearby);
            }
        }
        
        // 如果玩家在附近且按下 F 鍵
        if (isPlayerNearby && Input.GetKeyDown(pickupKey))
        {
            PickupRock();
        }
    }
    
    /// <summary>
    /// 撿起石頭
    /// </summary>
    private void PickupRock()
    {
        // 隱藏 UI 提示
        if (interactionUI != null)
        {
            interactionUI.ShowPrompt(false);
        }
        
        // 銷毀石頭物體
        Destroy(gameObject);
    }
    
    /// <summary>
    /// 視覺化調試：在編輯器中顯示檢測範圍
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}