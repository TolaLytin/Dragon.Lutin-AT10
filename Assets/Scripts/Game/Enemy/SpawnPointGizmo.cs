using UnityEngine;

public class SpawnPointGizmo : MonoBehaviour
{
    [SerializeField] private Color _gizmoColor = Color.red;
    [SerializeField] private float _radius = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawSphere(transform.position, _radius);
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}