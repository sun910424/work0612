using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 交互 UI 系統
/// 負責顯示 "按 F 撿起" 的提示文字
/// </summary>
public class InteractionUI : MonoBehaviour
{
    [Header("UI 引用")]
    [SerializeField] private Text promptText; // 提示文字
    [SerializeField] private CanvasGroup canvasGroup; // Canvas Group（用於淡入淡出）
    
    [Header("動畫設置")]
    [SerializeField] private float fadeDuration = 0.3f; // 淡入淡出時間
    
    private string promptMessage = "按 F 撿起"; // 提示文字內容
    private Coroutine fadeCoroutine;
    
    private void Start()
    {
        // 如果沒有分配，自動尋找
        if (promptText == null)
        {
            promptText = GetComponentInChildren<Text>();
        }
        
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
        
        // 初始化文字
        if (promptText != null)
        {
            promptText.text = promptMessage;
        }
        
        // 初始狀態：隱藏
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }
    }
    
    /// <summary>
    /// 顯示或隱藏提示
    /// </summary>
    public void ShowPrompt(bool show)
    {
        if (canvasGroup == null) return;
        
        // 停止之前的淡入淡出動畫
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        
        // 啟動新的淡入淡出動畫
        fadeCoroutine = StartCoroutine(FadePrompt(show ? 1f : 0f));
    }
    
    /// <summary>
    /// 淡入淡出動畫
    /// </summary>
    private IEnumerator FadePrompt(float targetAlpha)
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