using TriInspector;
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

    [Button]
    private void GetScreenPoint()
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPosition.position);
        Debug.Log($"#arvz# screenPos {screenPos}");
    }
    
    private void Update()
    {
        var panel = uiDocument.rootVisualElement.panel;

        Vector2 screenPos = mainCamera.WorldToScreenPoint(worldPosition.position);
        screenPos.y = Screen.height - screenPos.y; // Invert Y
        screenPos += offset;

        Vector2 panelPos = RuntimePanelUtils.ScreenToPanel(panel, screenPos);
        ApartmentPanel.style.left = panelPos.x;
        ApartmentPanel.style.top  = panelPos.y;
        
        ConsoleProDebug.Watch("left", panelPos.x.ToString());
        ConsoleProDebug.Watch("top", panelPos.y.ToString());
    }

    public void SetPanelText(string text)
    {
        var root = uiDocument.rootVisualElement;
        var apartmentNumberText = root.Q<TextElement>("ApartmentNumber");
        apartmentNumberText.text = text;
    }
    
}