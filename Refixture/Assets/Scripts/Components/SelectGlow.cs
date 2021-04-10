using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGlow : MonoBehaviour
{
    Outline outline;

    private void OnMouseEnter()
    {
        if (outline == null)
            outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineWidth = 1.2f;
        outline.OutlineColor = Color.green;
    }

    private void OnMouseExit()
    {
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineWidth = 0;
    }
}
