
using UnityEngine;

public class CameraConfigurator : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void OnGUI()
    {
        if (cam == null)
            return;

        GUILayout.BeginArea(new Rect(10, 10, 300, 320), GUI.skin.box);

        GUILayout.Label("Camera Configuration");

        string projection = cam.orthographic ? "Orthographic" : "Perspective";

        if (GUILayout.Button("Projection: " + projection))
        {
            cam.orthographic = !cam.orthographic;
        }

        if (cam.orthographic)
        {
            GUILayout.Label("Size: " + cam.orthographicSize.ToString("F1"));
            cam.orthographicSize = GUILayout.HorizontalSlider(
                cam.orthographicSize, 0.1f, 100f);
        }
        else
        {
            GUILayout.Label("Field of View: " + cam.fieldOfView.ToString("F1"));
            cam.fieldOfView = GUILayout.HorizontalSlider(
                cam.fieldOfView, 1f, 180f);
        }

        GUILayout.Space(8);
        GUILayout.Label("Clipping Planes");

        GUILayout.Label("Near: " + cam.nearClipPlane.ToString("F2"));
        float near = GUILayout.HorizontalSlider(
            cam.nearClipPlane, 0.01f, 100f);

        GUILayout.Label("Far: " + cam.farClipPlane.ToString("F0"));
        float far = GUILayout.HorizontalSlider(
            cam.farClipPlane, 1f, 1000f);

        // Mantener Far por delante de Near.
        cam.nearClipPlane = Mathf.Min(near, far - 0.01f);
        cam.farClipPlane = Mathf.Max(far, cam.nearClipPlane + 0.01f);

        GUILayout.EndArea();
    }



    private void OnDrawGizmos()
    {
        Camera cam = GetComponent<Camera>();

        if (cam == null)
            return;

        Matrix4x4 previousMatrix = Gizmos.matrix;
        Color previousColor = Gizmos.color;

        // Alinear los gizmos con la posición y rotación de Main Camera.
        Gizmos.matrix = transform.localToWorldMatrix;

        // Esfera roja de radio 10.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, 10f);

        // Línea roja en dirección forward.
        Gizmos.DrawLine(
            Vector3.zero,
            Vector3.forward * 20f
        );

        // Frustum amarillo de la cámara.
        Gizmos.color = Color.yellow;
        Gizmos.DrawFrustum(
            Vector3.zero,
            cam.fieldOfView,
            cam.farClipPlane,
            cam.nearClipPlane,
            cam.aspect
        );

        // Restaurar la configuración anterior.
        Gizmos.matrix = previousMatrix;
        Gizmos.color = previousColor;
    }


}
