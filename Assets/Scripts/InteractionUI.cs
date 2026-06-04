using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    // Prompt shown when the player can pick up the rock.
    [SerializeField] private string promptMessage = "按 F 撿起";
    [SerializeField] private Text promptText;

    private void Awake()
    {
        EnsurePromptText();
        HidePrompt();
    }

    private void EnsurePromptText()
    {
        if (promptText != null)
        {
            return;
        }

        var canvasObject = new GameObject("InteractionCanvas");
        var canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        var textObject = new GameObject("PromptText");
        textObject.transform.SetParent(canvasObject.transform, false);

        promptText = textObject.AddComponent<Text>();
        promptText.text = promptMessage;
        promptText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        promptText.fontSize = 32;
        promptText.alignment = TextAnchor.MiddleCenter;
        promptText.color = Color.white;

        var rect = promptText.rectTransform;
        rect.anchorMin = new Vector2(0.5f, 0f);
        rect.anchorMax = new Vector2(0.5f, 0f);
        rect.pivot = new Vector2(0.5f, 0f);
        rect.anchoredPosition = new Vector2(0f, 30f);
        rect.sizeDelta = new Vector2(420f, 60f);
    }

    public void ShowPrompt()
    {
        EnsurePromptText();
        promptText.text = promptMessage;
        promptText.enabled = true;
    }

    public void HidePrompt()
    {
        if (promptText != null)
        {
            promptText.enabled = false;
        }
    }
}
