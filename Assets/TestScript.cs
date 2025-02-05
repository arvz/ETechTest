using UnityEngine;

[ExecuteInEditMode]
public class TestScript : MonoBehaviour
{
    [SerializeField] private Transform _targetWorld;

    private void Update()
    {
        var screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _targetWorld.position);
        transform.position = screenPoint;

    }
    
}
