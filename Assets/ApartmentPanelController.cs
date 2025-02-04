using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class ApartmentPanelController : MonoBehaviour
{
    private VisualElement ApartmentPanel
    {
        get
        {
            if (apartmentPanel == null)
            {
                var root = uiDocument.rootVisualElement;
                apartmentPanel = root.Q<VisualElement>("ApartmentPanel");
            }

            return apartmentPanel;
        }
    }
    
    public UIDocument uiDocument;
    private VisualElement apartmentPanel;
    public Camera mainCamera;
    public Transform worldPosition;
    public Vector2 offset;

    void Update()
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPosition.position);
        // Flip Y for UI Toolkit.
        screenPos.y = Screen.height - screenPos.y;

        Vector2 finalPosition = (Vector2)screenPos + offset;
        ApartmentPanel.style.left = finalPosition.x;
        ApartmentPanel.style.top = finalPosition.y;
    }
}