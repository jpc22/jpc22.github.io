using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollViewSelectFixtures : MonoBehaviour
{
    private List<FurnishingSO> _furnishingsList;
    [SerializeField] private GameObject _furnishingSelectionUI;
    //[SerializeField] private FurnishingEventChannelSO _furnishingAddedChannel;

    void Start()
    {
        FurnishingContainerSO furnishingContainer = Resources.Load<FurnishingContainerSO>("AllFurnishings");
        _furnishingsList = furnishingContainer.Furnishings;

        RectTransform thisRect = gameObject.GetComponent<RectTransform>();
        float y = 0;
        thisRect.sizeDelta = new Vector3(thisRect.sizeDelta.x, y);

        foreach (FurnishingSO furnishing in _furnishingsList)
        {
            GameObject selectionUI = Instantiate(_furnishingSelectionUI, this.gameObject.transform);

            // Lower the position of the selection in the list
            RectTransform selectionRect = selectionUI.GetComponent<RectTransform>();
            selectionRect.anchoredPosition = new Vector3(selectionRect.anchoredPosition.x, y);
            y -= selectionRect.rect.height;

            // Match the scrollable UI size
            thisRect.sizeDelta = new Vector3(thisRect.sizeDelta.x, Mathf.Abs(y));

            // Configure the selectable entry
            FixtureSelection selected = selectionUI.GetComponent<FixtureSelection>();
            selected.Selection = furnishing;

        }
    } 
}
