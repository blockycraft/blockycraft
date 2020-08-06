using Blockycraft;
using Blockycraft.World;
using UnityEngine;

public sealed class Player : MonoBehaviour
{
    public float Speed { get; } = 10.0f;
    public float Sensitivity { get; } = 0.15f;
    private Vector3 lastMouse = new Vector3(255, 255, 255);

    public World world;
    public Transform cam;
    public Transform highlightBlock;
    public Transform placeBlock;
    public BlockType air;
    public BlockType selected;
    public float checkIncrement = 0.1f;
    public float reach = 8f;

    private void Update()
    {
        var adjustedPosition = (Input.mousePosition - lastMouse) * Sensitivity;
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x - adjustedPosition.y,
            transform.eulerAngles.y + adjustedPosition.x,
            0
        );

        transform.Translate(GetMovementDirection() * Speed * Time.deltaTime);
        lastMouse = Input.mousePosition;

        var coord = GetChunkCoordFromPosition(transform.position);
        world.Ping(coord);

        // A rough action block highlight
        UpdateActionBlock();
        ProcessActions();
    }

    private Vector3Int GetChunkCoordFromPosition(Vector3 position)
    {
        return new Vector3Int(
            (int)(position.x / WorldComponent.SIZE),
            (int)(position.y / WorldComponent.SIZE),
            (int)(position.z / WorldComponent.SIZE)
        );
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
            world.Set(placeBlock.position, selected);
        }
    }

    private void UpdateActionBlock()
    {
        var (lastPos, pos, type) = world.Detect(cam.position, cam.forward, checkIncrement, reach);
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

    private static Vector3 GetMovementDirection()
    {
        var direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            direction += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            direction += Vector3.back;
        if (Input.GetKey(KeyCode.A))
            direction += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            direction += Vector3.right;

        return direction;
    }
}