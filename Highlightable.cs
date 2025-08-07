using UnityEngine;

// Defines the dropdown menu options for the highlight style.
public enum HighlightType { ColorShift, Outline }

[RequireComponent(typeof(Renderer))]
public class Highlightable : MonoBehaviour
{
    [Tooltip("Choose the style of highlight for this object.")]
    public HighlightType type = HighlightType.ColorShift;

    [Tooltip("The thickness of the outline effect.")]
    [Range(1.0f, 1.05f)] public float outlineSize = 1.02f;

    private Color startColor;
    private Renderer objectRenderer;
    private GameObject outlineObject;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        startColor = objectRenderer.material.color;

        // If the outline style is chosen, prepare the outline object at the start.
        if (type == HighlightType.Outline)
        {
            CreateOutlineObject();
        }
    }

    private void CreateOutlineObject()
    {
        // Create a copy of this object to serve as the outline
        outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        // Destroy any unnecessary components on the outline clone
        Destroy(outlineObject.GetComponent<Highlightable>());
        Destroy(outlineObject.GetComponent<InteractionData>());

        // Get the renderer of the outline object
        Renderer outlineRenderer = outlineObject.GetComponent<Renderer>();
        // Assign our special Outline shader to it
        outlineRenderer.material.shader = Shader.Find("Unlit/Outline");
        // Set the initial size from our public variable
        outlineRenderer.material.SetFloat("_Outline_Width", outlineSize);
        // Turn it off by default
        outlineObject.SetActive(false);
    }

    public void Highlight(Color highlightColor)
    {
        switch (type)
        {
            case HighlightType.ColorShift:
                objectRenderer.material.color = highlightColor;
                break;
            case HighlightType.Outline:
                if (outlineObject != null)
                {
                    // Set the color and activate the outline object
                    outlineObject.GetComponent<Renderer>().material.SetColor("_Color", highlightColor);
                    outlineObject.SetActive(true);
                }
                break;
        }
    }

    public void ClearHighlight()
    {
        switch (type)
        {
            case HighlightType.ColorShift:
                objectRenderer.material.color = startColor;
                break;
            case HighlightType.Outline:
                if (outlineObject != null)
                {
                    // Simply deactivate the outline object
                    outlineObject.SetActive(false);
                }
                break;
        }
    }
}