using UnityEngine;
using Cinemachine;

public class PanAndZoom : MonoBehaviour
{

    [SerializeField] private float panSpeed = 6f;
    [SerializeField] private float zoomSpeed = 6f;
    [SerializeField] private float zoomInMax = 10f;
    [SerializeField] private float zoomOutMax = 100f;

    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;

    private Transform cameraTransform;


    private void Awake()
    {
        inputProvider = GetComponent<CinemachineInputProvider>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = virtualCamera.VirtualCameraGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float x = inputProvider.GetAxisValue(0);
        float y = inputProvider.GetAxisValue(1);
        float z = inputProvider.GetAxisValue(2);

        if (x != 0 || y != 0)
        {
            PanScreen(x, y);
        }
        if (z != 0)
        {
            ZoomScreen(z);
        }
    }

    public void ZoomScreen(float increment)
    {
        float fov = virtualCamera.m_Lens.OrthographicSize;
        float target = Mathf.Clamp(fov - increment * zoomSpeed, zoomInMax, zoomOutMax);

        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(fov, target, zoomSpeed * Time.deltaTime);
    }


    public Vector2 PanDirection(float x, float y)
    {
        Vector2 direction = Vector2.zero;

        if (y >= Screen.height * .95f)
        {
            direction.y += 1;
        }
        else if (y <= Screen.height * .05f)
        {
            direction.y -= 1;
        }

        if (x >= Screen.width * .95f)
        {
            direction.x += 1;
        }
        else if (x <= Screen.width * .05f)
        {
            direction.x -= 1;
        }

        return direction;
    }


    public void PanScreen(float x, float y)
    {
        Vector2 direction = PanDirection(x, y);
        cameraTransform.position = Vector3.Lerp(
            cameraTransform.position,
            cameraTransform.position + (Vector3)direction * panSpeed,
            panSpeed * Time.deltaTime
        );



    }
}