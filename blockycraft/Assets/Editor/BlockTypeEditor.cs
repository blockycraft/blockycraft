﻿using Blockycraft.Engine.Geometry;
using Blockycraft;
using Blockycraft.World.Chunk;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Blockycraft.Assets.Editor;

[CustomEditor(typeof(BlockType))]
public class BlockTypeEditor : Editor
{
    public const int DefaultPreviewSize = 1024;
    public const float ANGLE = 90.0f;

    private PreviewRenderUtility previewRenderUtility;
    private Scene previewScene;
    private MeshFilter targetMeshFilter;
    private MeshRenderer targetMeshRenderer;
    private GameObject previewRendererObject;
    private readonly System.Type[] components = new System.Type[] { typeof(MeshRenderer), typeof(MeshFilter) };

    private void Initialize(BlockType type)
    {
        if (previewRenderUtility != null)
            return;

        previewScene = EditorSceneManager.NewPreviewScene();
        previewRenderUtility = new PreviewRenderUtility(false);

        previewRenderUtility.camera.transform.position = new Vector3(5, 5, 5);
        previewRenderUtility.camera.transform.LookAt(Vector3.zero, Vector3.up);
        previewRenderUtility.camera.scene = previewScene;

        previewRendererObject = EditorUtility.CreateGameObjectWithHideFlags(
            "Preview Object",
            HideFlags.HideAndDontSave,
            components
        );
        SceneManager.MoveGameObjectToScene(previewRendererObject, previewScene);
        targetMeshFilter = previewRendererObject.GetComponent<MeshFilter>();
        targetMeshRenderer = previewRendererObject.GetComponent<MeshRenderer>();
        previewRendererObject.transform.position = -Voxel.Center;

        InitializeLighting(previewRenderUtility);

        ReloadMesh(previewRendererObject, type);
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
        if (previewRendererObject == null)
        {
            return;
        }

        var mesh = ChunkFactory.Build(block);
        previewRendererObject.GetComponent<MeshFilter>().mesh = mesh;
    }

    public override void OnInspectorGUI()
    {
        var block = target as BlockType;
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();
        if (EditorGUI.EndChangeCheck())
        {
            ReloadMesh(previewRendererObject, block);
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
            previewRendererObject.transform.position = -Voxel.Center;
        }
        if (GUILayout.Button("Preview [*]"))
        {
            GeneratePreview();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndFoldoutHeaderGroup();
    }

    public override bool HasPreviewGUI()
    {
        if (!(target is BlockType))
            return false;

        var block = (BlockType)target;
        if (!block.IsValid())
            return false;

        Initialize(block);
       
        return true;
    }

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        if (Event.current.type != EventType.Repaint && !(target is BlockType))
            return;

        var block = (BlockType)target;
        if (block.textures == null)
            return;

        targetMeshRenderer.sharedMaterial = block.textures.Material;

        var resultRender = RenderPreview(r, background);
        GUI.DrawTexture(r, resultRender, ScaleMode.StretchToFill, false);
    }

    public void GeneratePreview()
    {
        if (!(target is BlockType))
            return;

        var block = (BlockType)target;
        var rectangle = new Rect(0, 0, DefaultPreviewSize, DefaultPreviewSize);

        var background = previewRenderUtility.camera.backgroundColor;
        previewRenderUtility.camera.backgroundColor = Color.clear;

        var resultRender = RenderPreview(rectangle, GUIStyle.none);
        TextureHelper.SaveTexture(block.blockName, (RenderTexture)resultRender);

        previewRenderUtility.camera.backgroundColor = background;
    }

    private Texture RenderPreview(Rect r, GUIStyle background)
    {
        previewRenderUtility.BeginPreview(r, background);
        previewRenderUtility.DrawMesh(targetMeshFilter.sharedMesh,
            previewRendererObject.transform.localToWorldMatrix,
            targetMeshRenderer.sharedMaterial, 0);
        previewRenderUtility.camera.Render();
        return previewRenderUtility.EndPreview();
    }

    private void OnEnable()
    {
        HasPreviewGUI();
    }

    private void OnDisable()
    {
        previewRenderUtility?.Cleanup();
        EditorSceneManager.ClosePreviewScene(previewScene);
        previewRenderUtility = null;
    }
}