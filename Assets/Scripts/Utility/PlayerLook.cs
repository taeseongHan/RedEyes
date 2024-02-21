using UnityEngine;

public class PlayerLook : Singleton<PlayerLook>
{
    [Header("Look")]
    [SerializeField] private float minXLook;
    [SerializeField] private float maxXLook;
    public float lookSensitivity;
    private Transform _player;

    [HideInInspector]
    public bool canLook = true;

    // Start is called before the first frame update
    void Start()
    {
        _player = transform.root;
        ToggleCursor(false);
        FindObjectOfType<PlayerControl>().OnLookEvent.AddListener(RotateCamera);
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    private void RotateCamera(Vector2 delta)
    {
        float mouseX = delta.x * lookSensitivity / 100;
        float mouseY = delta.y * lookSensitivity / 100;

        transform.Rotate(Vector3.up, mouseX);
        transform.Rotate(Vector3.left, mouseY);

        float currentXRotation = transform.eulerAngles.x;
        if (currentXRotation > 180f)
            currentXRotation -= 360f;
        float newXRotation = Mathf.Clamp(currentXRotation, minXLook, maxXLook);

        transform.rotation = Quaternion.Euler(newXRotation, transform.eulerAngles.y, 0f);

        // Player rotation
        if (_player != null)
        {
            _player.Rotate(Vector3.up, mouseX); // Only rotate around y-axis for player
        }
    }
}
