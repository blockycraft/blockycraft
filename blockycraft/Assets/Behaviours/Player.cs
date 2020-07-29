using Assets.Scripts.World;
using Assets.Scripts.World.Chunk;
using UnityEngine;

public sealed class Player : MonoBehaviour
{
    public float Speed { get; } = 10.0f;
    public float Sensitivity { get; } = 0.15f;
    private Vector3 lastMouse = new Vector3(255, 255, 255);

    public World world;

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
    }

    private Vector3Int GetChunkCoordFromPosition(Vector3 position)
    {
        return new Vector3Int(
            (int)(position.x / WorldComponent.SIZE),
            (int)(position.y / WorldComponent.SIZE),
            (int)(position.z / WorldComponent.SIZE)
        );
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