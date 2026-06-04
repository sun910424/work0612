using UnityEngine;

public class InteractionSceneSetup : MonoBehaviour
{
    // Auto-create the minimum scene objects needed for the interaction demo.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void SetupScene()
    {
        EnsureCamera();

        var ui = Object.FindObjectOfType<InteractionUI>();
        if (ui == null)
        {
            ui = new GameObject("InteractionUI").AddComponent<InteractionUI>();
        }

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.name = "Player";
            player.tag = "Player";
            player.transform.position = new Vector3(0f, 0f, 0f);
        }

        var rock = Object.FindObjectOfType<RockInteraction>();
        if (rock == null)
        {
            var rockObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rockObject.name = "Rock";
            rockObject.transform.position = new Vector3(1.5f, 0f, 0f);
            rock = rockObject.AddComponent<RockInteraction>();
        }

        rock.SetInteractionUI(ui);
    }

    private static void EnsureCamera()
    {
        if (Camera.main != null)
        {
            return;
        }

        var cameraObject = new GameObject("Main Camera");
        var camera = cameraObject.AddComponent<Camera>();
        cameraObject.tag = "MainCamera";
        cameraObject.transform.position = new Vector3(0f, 2f, -10f);
        cameraObject.transform.LookAt(Vector3.zero);
        camera.orthographic = true;
        camera.orthographicSize = 5f;
    }
}
