
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondaryCamera;

    public Transform markerSur;
    public Transform markerOeste;
    public Transform markerNorte;
    public Transform markerEste;

    private Transform[] markers;
    private int currentMarker;
    private bool secondaryActive;

    private void Start()
    {
        markers = new Transform[]
        {
            markerSur,
            markerOeste,
            markerNorte,
            markerEste
        };

        if (mainCamera == null || secondaryCamera == null)
        {
            Debug.LogError("CameraSwitch: asigná las dos cámaras.");
            enabled = false;
            return;
        }

        mainCamera.enabled = true;
        secondaryCamera.enabled = false;
        secondaryActive = false;

        if (markerSur != null)
        {
            currentMarker = 0;
            secondaryCamera.transform.SetPositionAndRotation(
                markerSur.position, markerSur.rotation);
        }

        Debug.Log("CameraSwitch iniciado correctamente.");
    }



    private void OnGUI()
    {
        float panelWidth = 250f;
        float panelHeight = secondaryActive ? 100f : 65f;
        float x = Screen.width - panelWidth - 10f;

        GUI.Box(
            new Rect(x, 10f, panelWidth, panelHeight),
            "Camera Switch"
        );

        if (GUI.Button(
            new Rect(x + 10f, 35f, 230f, 25f),
            secondaryActive ? "Usar Main Camera" : "Usar Secondary Camera"))
        {
            secondaryActive = !secondaryActive;
            mainCamera.enabled = !secondaryActive;
            secondaryCamera.enabled = secondaryActive;
        }

        if (secondaryActive &&
            GUI.Button(
                new Rect(x + 10f, 65f, 230f, 25f),
                "Siguiente posición"))
        {
            currentMarker = (currentMarker + 1) % markers.Length;

            Transform marker = markers[currentMarker];

            if (marker != null)
            {
                secondaryCamera.transform.SetPositionAndRotation(
                    marker.position,
                    marker.rotation
                );
            }
        }
    }

}


