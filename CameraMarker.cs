
using UnityEngine;

public class CameraMarker : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Matrix4x4 previousMatrix = Gizmos.matrix;
        Color previousColor = Gizmos.color;

        Gizmos.matrix = transform.localToWorldMatrix;

        // Cubo verde de 10 unidades.
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * 10f);

        // Línea roja en dirección forward.
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            Vector3.zero,
            Vector3.forward * 20f
        );

        Gizmos.matrix = previousMatrix;
        Gizmos.color = previousColor;
    }
}
