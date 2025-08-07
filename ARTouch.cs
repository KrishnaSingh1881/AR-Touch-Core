using UnityEngine;
using System.Collections;

// Defines the dropdown menu options for the popup's behavior.
public enum FollowType { None, Billboard, SmoothFollow }

public class ARTouch : MonoBehaviour
{
    [Header("Spawning Settings")]
    [Tooltip("How far from the object's surface the popup will spawn.")]
    [SerializeField] private float popupSpawnOffset = 0.5f;

    [Tooltip("Select the behavior for the popup after it spawns.")]
    [SerializeField] private FollowType popupFollowType = FollowType.Billboard;

    [Header("Highlighting Settings")]
    [Tooltip("Enable to highlight objects when the camera points at them.")]
    [SerializeField] private bool enableHighlighting = true;
    [SerializeField] private Color highlightColor = Color.cyan;

    // --- Private Variables ---
    private GameObject activePopup = null;
    private Highlightable lastHighlighted = null;

    void Update()
    {
        HandleHighlighting();
        HandleInput();
    }

    void HandleHighlighting()
    {
        if (!enableHighlighting) return;

        Ray centerRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hitInfo;

        if (Physics.Raycast(centerRay, out hitInfo, 100))
        {
            Highlightable currentHighlight = hitInfo.transform.GetComponent<Highlightable>();
            if (currentHighlight != null && currentHighlight != lastHighlighted)
            {
                ClearLastHighlight();
                currentHighlight.Highlight(highlightColor);
                lastHighlighted = currentHighlight;
            }
            else if (currentHighlight == null && lastHighlighted != null)
            {
                ClearLastHighlight();
            }
        }
        else if (lastHighlighted != null)
        {
            ClearLastHighlight();
        }
    }

    void HandleInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ProcessInput(Input.GetTouch(0).position);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            ProcessInput(Input.mousePosition);
        }
    }

    void ProcessInput(Vector3 screenPosition)
    {
        Ray inputRay = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit, 100))
        {
            // --- SPAWNING LOGIC ---
            InteractionData interaction = hit.transform.GetComponent<InteractionData>();
            if (interaction != null && interaction.prefabToSpawn != null)
            {
                if (activePopup != null)
                {
                    activePopup.GetComponent<PopupController>()?.FadeOutAndDestroy();
                }

                Vector3 offsetDirection = (hit.normal + Vector3.up).normalized;
                Vector3 spawnPosition = hit.point + offsetDirection * popupSpawnOffset;
                
                GameObject newPopup = Instantiate(interaction.prefabToSpawn, spawnPosition, Quaternion.identity);
                activePopup = newPopup;

                switch (popupFollowType)
                {
                    case FollowType.Billboard:
                        newPopup.AddComponent<Billboard>();
                        break;
                    case FollowType.SmoothFollow:
                        newPopup.AddComponent<PopupFollow>();
                        break;
                    case FollowType.None:
                        Vector3 lookDirection = (Camera.main.transform.position - newPopup.transform.position).normalized;
                        lookDirection.y = 0;
                        newPopup.transform.rotation = Quaternion.LookRotation(lookDirection);
                        break;
                }
                return;
            }

            // --- DESTRUCTION LOGIC ---
            PopupController popup = hit.transform.GetComponent<PopupController>();
            if (popup != null)
            {
                popup.FadeOutAndDestroy();
                activePopup = null;
            }
        }
    }

    private void ClearLastHighlight()
    {
        if (lastHighlighted != null)
        {
            lastHighlighted.ClearHighlight();
            lastHighlighted = null;
        }
    }
}