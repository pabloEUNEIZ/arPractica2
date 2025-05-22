using UnityEngine;
using UnityEngine.XR.ARFoundation; // Necesario para ARPlaneManager, ARPlane, ARPlanesChangedEventArgs
using UnityEngine.XR.ARSubsystems; // Necesario para PlaneAlignment, TrackingState
using System.Collections.Generic;  // Necesario para List

public class PlaneCounter : MonoBehaviour
{
    public int HorizontalPlanesCount { get; private set; }
    public int VerticalPlanesCount { get; private set; }

    [Header("Debug Counts (Read-Only)")]
    [SerializeField] private int _debugHorizontalCount = 0;
    [SerializeField] private int _debugVerticalCount = 0;

    [Header("Cube Placement Settings")]
    public GameObject cubePrefab; // Asigna tu prefab de cubo aquí en el Inspector
    public GameObject  goPlaneManager;
    private ARPlaneManager planeManager;


    void Awake()
    {
        planeManager = goPlaneManager.GetComponent<ARPlaneManager>();
        if (planeManager == null)
        {
            Debug.LogError("ARPlaneManager no encontrado en este GameObject. El script PlaneCounter no funcionará.");
            enabled = false;
            return;
        }

        if (cubePrefab == null)
        {
            Debug.LogWarning("Cube Prefab no asignado en PlaneCounter. La función de instanciar cubos no funcionará hasta que se asigne.");
        }
    }

    void OnEnable()
    {
        if (planeManager != null)
        {
            planeManager.planesChanged += OnPlanesChanged;
            RecalculatePlaneCounts();
        }
    }

    void OnDisable()
    {
        if (planeManager != null)
        {
            planeManager.planesChanged -= OnPlanesChanged;
        }
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        RecalculatePlaneCounts();
    }

    private void RecalculatePlaneCounts()
    {
        if (planeManager == null || !planeManager.enabled) return;

        int currentHorizontal = 0;
        int currentVertical = 0;

        foreach (ARPlane plane in planeManager.trackables)
        {
            if (plane.trackingState == TrackingState.Tracking && plane.subsumedBy == null)
            {
                if (plane.alignment == PlaneAlignment.HorizontalDown ||
                    plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    currentHorizontal++;
                }
                else if (plane.alignment == PlaneAlignment.Vertical)
                {
                    currentVertical++;
                }
            }
        }

        HorizontalPlanesCount = currentHorizontal;
        VerticalPlanesCount = currentVertical;

        _debugHorizontalCount = HorizontalPlanesCount;
        _debugVerticalCount = VerticalPlanesCount;
        //Debug.Log("_debugHorizontalCount"+_debugHorizontalCount);
        //Debug.Log("_debugVerticalCount"+_debugVerticalCount);

        this.gameObject.GetComponent<UIManagerJuego>().planoDectado(VerticalPlanesCount,HorizontalPlanesCount);
    }

    public void InstantiateCubesOnDetectedPlanes(int numCubesOnVertical, int numCubesOnHorizontal)
    {
        if (cubePrefab == null)
        {
            Debug.LogError("Cube Prefab no asignado. No se pueden instanciar cubos.");
            return;
        }

        if (planeManager == null || !planeManager.enabled)
        {
            Debug.LogWarning("ARPlaneManager no está disponible. No se pueden instanciar cubos.");
            return;
        }

        List<ARPlane> validHorizontalPlanes = new List<ARPlane>();
        List<ARPlane> validVerticalPlanes = new List<ARPlane>();

        foreach (ARPlane plane in planeManager.trackables)
        {
            if (plane.trackingState == TrackingState.Tracking && plane.subsumedBy == null)
            {
                if (plane.alignment == PlaneAlignment.HorizontalDown ||
                    plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    validHorizontalPlanes.Add(plane);
                }
                else if (plane.alignment == PlaneAlignment.Vertical)
                {
                    validVerticalPlanes.Add(plane);
                }
            }
        }

        Debug.Log($"Intentando instanciar cubos. Planos horizontales válidos: {validHorizontalPlanes.Count}, Planos verticales válidos: {validVerticalPlanes.Count}");

        int horizontalPlaced = 0;
        for (int i = 0; i < validHorizontalPlanes.Count && horizontalPlaced < numCubesOnHorizontal; i++)
        {
            ARPlane planeToUse = validHorizontalPlanes[i];


            Instantiate(cubePrefab, planeToUse.center, planeToUse.gameObject.transform.rotation);
            horizontalPlaced++;
            Debug.Log($"Cubo instanciado en plano horizontal {planeToUse.trackableId} en {planeToUse.center}");
        }
        if (horizontalPlaced < numCubesOnHorizontal)
        {
            Debug.LogWarning($"Se solicitaron {numCubesOnHorizontal} cubos en planos horizontales, pero solo se pudieron instanciar {horizontalPlaced}.");
        }

        int verticalPlaced = 0;
        for (int i = 0; i < validVerticalPlanes.Count && verticalPlaced < numCubesOnVertical; i++)
        {
            ARPlane planeToUse = validVerticalPlanes[i];


            Instantiate(cubePrefab, planeToUse.center, planeToUse.gameObject.transform.rotation);
            verticalPlaced++;
            Debug.Log($"Cubo instanciado en plano vertical {planeToUse.trackableId} en {planeToUse.center}");
        }
        if (verticalPlaced < numCubesOnVertical)
        {
            Debug.LogWarning($"Se solicitaron {numCubesOnVertical} cubos en planos verticales, pero solo se pudieron instanciar {verticalPlaced}.");
        }
    }

    void Update()
    {
        //     if (Input.GetKeyDown(KeyCode.P))
        // {
        //     Debug.Log("Tecla 'P' presionada. Intentando instanciar cubos...");
        //     InstantiateCubesOnDetectedPlanes(numCubesOnVertical: 5, numCubesOnHorizontal: 7);
        // }
    }
}