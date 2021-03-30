using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour
{

    private Rect _thisRect;
    private void Awake()
    {
        _thisRect = gameObject.GetComponent<RectTransform>().rect;
    }

    private void OnGUI()
    {
        _thisRect = GUI.Window(0, _thisRect, DoMyWindow, "My Window");
    }

    void DoMyWindow(int windowID)
    {
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }
}