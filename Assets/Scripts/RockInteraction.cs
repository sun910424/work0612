using UnityEngine;

public class RockInteraction : MonoBehaviour
{
    // The distance at which the player can interact with the rock.
    [SerializeField] private float detectionRange = 2f;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private InteractionUI interactionUI;

    private Transform playerTransform;
    private bool playerInRange;

    private void Start()
    {
        TryFindPlayer();

        if (interactionUI == null)
        {
            interactionUI = FindObjectOfType<InteractionUI>();
        }
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            TryFindPlayer();
        }

        if (playerTransform == null)
        {
            playerInRange = false;
            interactionUI?.HidePrompt();
            return;
        }

        var distance = Vector3.Distance(transform.position, playerTransform.position);
        playerInRange = distance <= detectionRange;

        if (playerInRange)
        {
            interactionUI?.ShowPrompt();

            if (Input.GetKeyDown(KeyCode.F))
            {
                PickUp();
            }
        }
        else
        {
            interactionUI?.HidePrompt();
        }
    }

    private void TryFindPlayer()
    {
        var player = GameObject.FindGameObjectWithTag(playerTag);
        playerTransform = player != null ? player.transform : null;
    }

    public void SetInteractionUI(InteractionUI ui)
    {
        interactionUI = ui;
    }

    private void PickUp()
    {
        interactionUI?.HidePrompt();
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        interactionUI?.HidePrompt();
    }
}
