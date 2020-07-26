using Assets.Scripts;
using Assets.Scripts.Geometry;
using Assets.Scripts.World.Chunk;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BlockType))]
public class BlockTypeEditor : Editor
{
    public const float ANGLE = 90.0f;

    private PreviewRenderUtility previewRenderUtility;
    private MeshFilter targetMeshFilter;
    private MeshRenderer targetMeshRenderer;
    private GameObject previewRendererObject;
    private readonly System.Type[] components = new System.Type[] { typeof(MeshRenderer), typeof(MeshFilter) };

    private void Initialize(BlockType block)
    {
        if (previewRenderUtility != null)
            return;

        previewRenderUtility = new PreviewRenderUtility(false);

        previewRenderUtility.camera.transform.position = new Vector3(5, 5, 5);
        previewRenderUtility.camera.transform.LookAt(Vector3.zero, Vector3.up);

        previewRendererObject = EditorUtility.CreateGameObjectWithHideFlags(
            "Preview Object",
            HideFlags.HideAndDontSave,
            components
        );

        InitializeLighting(previewRenderUtility);
        ReloadMesh(previewRendererObject, block);

        targetMeshFilter = previewRendererObject.GetComponent<MeshFilter>();
        targetMeshRenderer = previewRendererObject.GetComponent<MeshRenderer>();
        targetMeshRenderer.transform.position = -Voxel.Center;
    }

    private void InitializeLighting(PreviewRenderUtility utility)
    {
        if (utility.lights.Length != 2)
        {
            throw new System.ArgumentOutOfRangeException("Expected PreviewRenderUtility to have a constant 2 lights");
        }

        utility.lights[0].color = new Color(1f, 1f, 1f, 0f) * 0.7f;
        utility.lights[0].intensity = 1.4f;
        utility.lights[0].transform.rotation = Quaternion.Euler(40f, 40f, 0f);

        utility.lights[1].color = new Color(1f, 1f, 1f, 0f) * 0.7f;
        utility.lights[1].intensity = 1.4f;
        utility.lights[1].transform.rotation = Quaternion.Euler(340, 218, 177);
    }

    private void ReloadMesh(GameObject previewRendererObject, BlockType block)
    {
        var mesh = ChunkFactory.Build(block);
        previewRendererObject.GetComponent<MeshFilter>().mesh = mesh;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        if (EditorGUI.EndChangeCheck())
        {
            ReloadMesh(previewRendererObject, (BlockType)target);
        }

        GUILayout.Label("Viewer", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Dimension X", EditorStyles.miniLabel);
        if (GUILayout.Button("Rotate Left", EditorStyles.miniButtonLeft))
        {
            previewRendererObject.transform.RotateAround(Vector3.zero, Vector3.up, ANGLE);
        }
        if (GUILayout.Button("Rotate Right", EditorStyles.miniButtonRight))
        {
            previewRendererObject.transform.RotateAround(Vector3.zero, Vector3.up, -ANGLE);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Dimension Y", EditorStyles.miniLabel);
        if (GUILayout.Button("Rotate Left", EditorStyles.miniButtonLeft))
        {
            previewRendererObject.transform.RotateAround(Vector3.zero, Vector3.right, -ANGLE);
        }
        if (GUILayout.Button("Rotate Right", EditorStyles.miniButtonRight))
        {
            previewRendererObject.transform.RotateAround(Vector3.zero, Vector3.right, ANGLE);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Dimension Z", EditorStyles.miniLabel);
        if (GUILayout.Button("Rotate Left", EditorStyles.miniButtonLeft))
        {
            previewRendererObject.transform.RotateAround(Vector3.zero, Vector3.forward, -ANGLE);
        }
        if (GUILayout.Button("Rotate Right", EditorStyles.miniButtonRight))
        {
            previewRendererObject.transform.RotateAround(Vector3.zero, Vector3.forward, ANGLE);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Rotation", EditorStyles.miniLabel);
        if (GUILayout.Button("Reset"))
        {
            previewRendererObject.transform.rotation = Quaternion.identity;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndFoldoutHeaderGroup();
    }

    public override bool HasPreviewGUI()
    {
        if (!(target is BlockType))
            return false;

        Initialize((BlockType)target);

        return true;
    }

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        if (Event.current.type != EventType.Repaint && !(target is BlockType))
            return;

        var block = (BlockType)target;
        targetMeshRenderer.sharedMaterial = block.material;

        previewRenderUtility.BeginPreview(r, background);
        previewRenderUtility.DrawMesh(targetMeshFilter.sharedMesh,
            previewRendererObject.transform.localToWorldMatrix,
            targetMeshRenderer.sharedMaterial, 0);
        previewRenderUtility.camera.Render();
        Texture resultRender = previewRenderUtility.EndPreview();

        GUI.DrawTexture(r, resultRender, ScaleMode.StretchToFill, false);
    }

    private void OnDisable()
    {
        previewRenderUtility?.Cleanup();
    }
}