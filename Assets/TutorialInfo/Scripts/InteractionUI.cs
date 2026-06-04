using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 交互 UI 系統
/// 負責顯示字幕提示
/// </summary>
public class InteractionUI : MonoBehaviour
{
    [Header("UI 引用")]
    [SerializeField] private Text messageText; // 字幕文字
    [SerializeField] private CanvasGroup canvasGroup; // Canvas Group（用於淡入淡出）
    
    [Header("動畫設置")]
    [SerializeField] private float fadeDuration = 0.2f; // 淡入淡出時間
    
    private Coroutine fadeCoroutine;
    
    private void Start()
    {
        // 如果沒有分配，自動尋找
        if (messageText == null)
        {
            messageText = GetComponentInChildren<Text>();
        }
        
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
        
        // 初始狀態：隱藏
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }
    }
    
    /// <summary>
    /// 顯示或隱藏訊息
    /// </summary>
    public void ShowMessage(string message, bool show)
    {
        if (messageText == null || canvasGroup == null) return;
        
        // 設置文字
        messageText.text = message;
        
        // 停止之前的淡入淡出動畫
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        
        // 啟動新的淡入淡出動畫
        fadeCoroutine = StartCoroutine(FadeMessage(show ? 1f : 0f));
    }
    
    /// <summary>
    /// 淡入淡出動畫
    /// </summary>
    private IEnumerator FadeMessage(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;
        
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }
        
        canvasGroup.alpha = targetAlpha;
    }
}
