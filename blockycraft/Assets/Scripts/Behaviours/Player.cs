using Blockycraft;
using UnityEngine;

public sealed class Player : MonoBehaviour
{
    public World world;
    public Transform cam;
    public Transform highlightBlock;
    public Transform placeBlock;
    public BlockType air;
    public BlockSelector selector;
    public float increment;
    public float reach;
    public float speed;
    public float factor;

    private bool isFalling;
    private bool isClimbing;
    private float mouseHorizontal;
    private float mouseVertical;
    private Vector3 velocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        mouseHorizontal = Input.GetAxis("Mouse X");
        mouseVertical = Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.LeftShift)) isFalling = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) isFalling = false;
        if (Input.GetKeyDown(KeyCode.Space)) isClimbing = true;
        if (Input.GetKeyUp(KeyCode.Space)) isClimbing = false;

        float value = 0f;
        if (isClimbing && isFalling) value = 0f;
        else if (isClimbing) value = 1f;
        else if (isFalling) value = -1f;

        velocity = CalculateVelocity(horizontal, vertical, value);

        UpdateActionBlock();
        ProcessActions();
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * mouseHorizontal * factor);
        cam.Rotate(Vector3.right * -mouseVertical * factor);
        transform.Translate(velocity, Space.World);
    }

    private Vector3 CalculateVelocity(float horizontal, float depth, float vertical)
    {
        return ((transform.forward * depth) +
                (transform.right * horizontal) +
                (transform.up * vertical)) * Time.fixedDeltaTime * speed;
    }

    public Vector3Int Chunk()
    {
        var position = transform.position;
        return MathHelper.Anchor((int)position.x, (int)position.y, (int)position.z, World.SIZE);
    }

    public Vector3Int WhereAmI()
    {
        var position = transform.position;
        return new Vector3Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z));
    }

    private void ProcessActions()
    {
        if (!highlightBlock.gameObject.activeSelf)
        {
            return;
        }

        // Destroy block.
        if (Input.GetMouseButtonDown(0))
        {
            world.Set(highlightBlock.position, air);
        }

        // Place block.
        if (Input.GetMouseButtonDown(1))
        {
            world.Set(placeBlock.position, selector.Selected);
        }
    }

    private void UpdateActionBlock()
    {
        var (lastPos, pos, type) = world.Detect(cam.position, cam.forward, increment, reach);
        if (type != null)
        {
            highlightBlock.position = pos;
            placeBlock.position = lastPos;

            highlightBlock.gameObject.SetActive(true);
            placeBlock.gameObject.SetActive(true);
        }
        else
        {
            highlightBlock.gameObject.SetActive(false);
            placeBlock.gameObject.SetActive(false);
        }
    }
}