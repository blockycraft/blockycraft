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
    private bool isFalling;
    private bool isClimbing;

    private void Update()
    {
        UpdateActionBlock();
        ProcessActions();
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var mouseHorizontal = Input.GetAxis("Mouse X");
        var mouseVertical = Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(KeyCode.LeftShift)) isFalling = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) isFalling = false;
        if (Input.GetKeyDown(KeyCode.Space)) isClimbing = true;
        if (Input.GetKeyUp(KeyCode.Space)) isClimbing = false;

        var velocity = CalculateHorizontalVelocity(horizontal, vertical);
        if (isFalling) velocity += Vector3.down * Time.fixedDeltaTime * speed;
        if (isClimbing) velocity += Vector3.up * Time.fixedDeltaTime * speed;

        transform.Rotate(Vector3.up * mouseHorizontal);
        cam.Rotate(Vector3.right * -mouseVertical);
        transform.Translate(velocity, Space.World);
    }

    private Vector3 CalculateHorizontalVelocity(float horizontal, float vertical)
    {
        return ((transform.forward * vertical) + (transform.right * horizontal)) * Time.fixedDeltaTime * speed;
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