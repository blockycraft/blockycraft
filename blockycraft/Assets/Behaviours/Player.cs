using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

public sealed class Player : MonoBehaviour
{
    public float Speed { get; } = 10.0f;
    public float Sensitivity { get; } = 0.15f;
    private Vector3 lastMouse = new Vector3(255, 255, 255);

    public World world;
    public Transform cam;
    public Transform highlightBlock;
    public Transform placeBlock;
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
    }

    private Vector3Int GetChunkCoordFromPosition(Vector3 position)
    {
        return new Vector3Int(
            (int)(position.x / WorldComponent.SIZE),
            (int)(position.y / WorldComponent.SIZE),
            (int)(position.z / WorldComponent.SIZE)
        );
    }

    private void UpdateActionBlock()
    {
        float step = 8f;
        Vector3 pos = cam.position + (cam.forward * step);
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        int z = Mathf.FloorToInt(pos.z);
        var type = world.component.GetBlock(x, y, z);
        if (type == null)
        {
            Debug.Log($"Issue occurred getting block at {x}:{y}{z}");
        }

        var coord = new Vector3Int(Mathf.FloorToInt(x / WorldComponent.SIZE), Mathf.FloorToInt(y / WorldComponent.SIZE), Mathf.FloorToInt(z / WorldComponent.SIZE));
        var block = new Vector3Int(
            x - coord.x * WorldComponent.SIZE,
            y - coord.y * WorldComponent.SIZE,
            z - coord.z * WorldComponent.SIZE
        );

        var adjusted = new Vector3(
            coord.x * WorldComponent.SIZE + block.x,
            coord.y * WorldComponent.SIZE + block.y,
            coord.z * WorldComponent.SIZE + block.z
        );

        highlightBlock.position = adjusted;
        placeBlock.position = adjusted;
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