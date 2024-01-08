using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    [SerializeField]
    [Tooltip("Add AR camera")]
    GameObject cam;

    [SerializeField]
    [Tooltip("Add the gameobject which contains the scan surface animation")]
    GameObject Animation;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public static GameObject spawnedObject { get; private set; }

    static bool isObjectPlaced = false;

    static Vector3 initialScale;

    Quaternion previousRotation;

    Quaternion rotation;

    Vector3 previousPosition;

    float time = 0;


    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        Animation.SetActive(true);
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                TouchIndicatorHandler.isTouchedTheObject = false;
            }
            return true;
        }

        else
        {
            TouchIndicatorHandler.isTouchedTheObject = false;
        }
        touchPosition = default;
        return false;
    }
    void Start()
    {
        spawnedObject = Instantiate(placedPrefab);
        initialScale = spawnedObject.transform.localScale;
        spawnedObject.transform.parent = cam.transform.transform;
        rotation = spawnedObject.transform.rotation;
        spawnedObject.transform.rotation = rotation;
        spawnedObject.transform.position = cam.transform.position + new Vector3(0, 0, 0.75f);
        spawnedObject.SetActive(false);
        delayToShowSpawnedObject();
    }
    void Update()
    {
        if (!isObjectPlaced)
        {
            delayToShowSpawnedObject();
            spawnedObject.transform.rotation = rotation;
            cam.transform.position = new Vector3(0, 0, 0);
        }

        if (!isObjectPlaced)
        {
            Vector3 rayEmitPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            if (m_RaycastManager.Raycast(rayEmitPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                Destroy(spawnedObject);
                var hitPose = s_Hits[0].pose;
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                previousRotation = hitPose.rotation;
                previousPosition = hitPose.position;
                isObjectPlaced = true;
                Animation.SetActive(false);
            }
        }
        else
        {
            if (TouchIndicatorHandler.isTouchedTheObject && (Input.touchCount < 2))
            {
                if (!TryGetTouchPosition(out Vector2 touchPosition))
                    return;

                if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    // Raycast hits are sorted by distance, so the first one
                    // will be the closest hit.
                    var hitPose = s_Hits[0].pose;
                    spawnedObject.transform.position = hitPose.position;
                    previousPosition = hitPose.position;
                }
            }
        }
        freezePositionWhenRotate();

    }
    public static void hideTouchIndicator()
    {
        if (isObjectPlaced)
        {
            spawnedObject.transform.GetChild(0).gameObject.SetActive(false);
            
            // spawnedObject.GetComponent<LeanPinchScale>().enabled = false;
        }

    }
    public static void showTouchIndicator()
    {
        if (isObjectPlaced)
        {
            spawnedObject.transform.GetChild(0).gameObject.SetActive(true);
           
            // spawnedObject.GetComponent<LeanPinchScale>().enabled = true;
        }

    }

    void delayToShowSpawnedObject()
    {
        if (time > 1.2f)
        {
            spawnedObject.SetActive(true);
        }
        else
        {
            time += Time.deltaTime;
            Debug.Log(time);
        }
    }
    void freezePositionWhenRotate()
    {
        if (isObjectPlaced && (Input.touchCount > 1))
        {
            if (previousRotation != spawnedObject.transform.rotation)
            {
                spawnedObject.transform.position = previousPosition;
                previousRotation = spawnedObject.transform.rotation;
            }
            else if (previousRotation == spawnedObject.transform.rotation)
            {
                previousPosition = spawnedObject.transform.position;
            }
        }
    }

    public static void resetToInitialScale()
    {
        spawnedObject.transform.localScale = initialScale;
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
}

