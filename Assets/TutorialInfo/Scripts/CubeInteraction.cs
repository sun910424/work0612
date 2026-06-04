using UnityEngine;
using System.Collections;

/// <summary>
/// 方塊交互系統
/// 按 F 顯示字幕「只是個方塊」，3 秒後消失
/// </summary>
public class CubeInteraction : MonoBehaviour
{
    [Header("交互設置")]
    [SerializeField] private float detectionRange = 3f; // 檢測範圍
    [SerializeField] private KeyCode interactKey = KeyCode.F; // 交互按鍵
    [SerializeField] private float displayDuration = 3f; // 字幕顯示時間（秒）
    
    [Header("引用")]
    [SerializeField] private InteractionUI interactionUI; // UI 管理器
    
    private Transform playerTransform; // 玩家位置
    private bool isPlayerNearby = false; // 玩家是否在附近
    private Coroutine hideCoroutine; // 隱藏字幕的協程
    
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
        bool shouldShowHint = distance <= detectionRange;
        
        // 如果玩家在附近且按下 F 鍵
        if (shouldShowHint && Input.GetKeyDown(interactKey))
        {
            InteractCube();
        }
    }
    
    /// <summary>
    /// 交互方塊：顯示字幕
    /// </summary>
    private void InteractCube()
    {
        if (interactionUI == null) return;
        
        // 停止之前的隱藏協程
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        
        // 顯示字幕
        interactionUI.ShowMessage("只是個方塊", true);
        
        // 設置 3 秒後隱藏
        hideCoroutine = StartCoroutine(HideMessageAfterDelay());
    }
    
    /// <summary>
    /// 延遲後隱藏字幕
    /// </summary>
    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        
        if (interactionUI != null)
        {
            interactionUI.ShowMessage("", false);
        }
    }
    
    /// <summary>
    /// 視覺化調試：在編輯器中顯示檢測範圍
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
