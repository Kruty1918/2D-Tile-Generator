using Mediators;
using System;
using System.Collections.Generic;
using Systems.WorldGenerator_;
using UnityEngine;

namespace Systems.ChunkSystem_
{
    public class Chunk
    {
        #region Properties
        public bool Active { get; private set; }
        public Vector2 WorldPos;
        public Vector2Int Coord;
        public Bounds Bounds;

        public readonly Color DefaultColor = Color.red;
        public readonly Color ActiveColor = Color.green;
        public readonly Color CenterColor = Color.blue;


        public bool CenterChunk => SM.Instance<ChunkSystem>().IsCentralChunk(this);
        public readonly int ChunkSize;

        public GameObject ChunkObject { get; private set; }
        public MeshRenderer MeshRenderer { get; private set; }

        #endregion

        #region Constructors
        public Chunk(Vector2 worldPos, int chunkSize, Action<bool> onActiveStateChanged)
        {
            WorldPos = worldPos;
            ChunkSize = chunkSize;
            Coord = ConvertCoords.ConvertToTileCoord(worldPos, false, chunkSize, chunkSize);
            GridSystem.AddElement(GridId.ChunkGrid, Coord, this);
            Bounds = new Bounds(worldPos, new Vector2(chunkSize, chunkSize));
            CreateObjectsHierarchy();
        }

        public Chunk(Vector2 worldPos, int chunkSize)
        {
            WorldPos = worldPos;
            ChunkSize = chunkSize;
            Coord = ConvertCoords.ConvertToTileCoord(worldPos, false, chunkSize, chunkSize);
            GridSystem.AddElement(GridId.ChunkGrid, Coord, this);
            Bounds = new Bounds(worldPos, new Vector2(chunkSize, chunkSize));
            CreateObjectsHierarchy();
        }
        #endregion

        #region Private Methods
        private GameObject CreateChunk()
        {
            var chunk = new GameObject("Chunk_" + Coord.x + "_" + Coord.y);
            chunk.transform.position = WorldPos;
            return chunk;
        }

        private GameObject CreateParentObject()
        {
            var parentObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            parentObject.name = "GameObjects";
            parentObject.transform.SetParent(CreateChunk().transform);
            parentObject.transform.position = WorldPos;
            parentObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
            return parentObject;
        }

        private void ScaleParentObject(GameObject parentObject)
        {
            float chunckSize = ChunkSize * 0.1f;
            parentObject.transform.localScale = new Vector3(chunckSize, chunckSize, chunckSize);
        }

        private void CreateAndApplyMaterial(GameObject parentObject)
        {
            Material material = new Material(Shader.Find("Custom/RotateTexture2D"));
            material.mainTexture = Texture2D.grayTexture;
            MeshRenderer = parentObject.GetComponent<MeshRenderer>();
            MeshRenderer.material = material;
        }

        private void SetEnvironmentObjectsParent(List<GameObject> environmentObjects, GameObject parent)
        {
            foreach (GameObject childObject in environmentObjects)
            {
                childObject.transform.SetParent(parent.transform);
            }
        }

        private void CreateObjectsHierarchy()
        {
            ChunkObject = CreateParentObject();

            GenerationResult result = SM.Instance<WorldGenerator>().Generation(Bounds, ChunkSize);

            ScaleParentObject(ChunkObject);
            CreateAndApplyMaterial(ChunkObject);
            SetEnvironmentObjectsParent(result.EnvironmentObjects, ChunkObject);
            SetTileSetTexture(result.Tiles);
        }

        #region Texture
        private void SetTileSetTexture(Dictionary<Vector2Int, Texture2D> keyValuePairs)
        {
            MeshRenderer.material.mainTexture = CreateTextureAtlas(keyValuePairs);
        }

        private Texture2D CreateTextureAtlas(Dictionary<Vector2Int, Texture2D> keyValuePairs, int chunkSize = 16, int textureSize = 16)
        {
            Texture2D textureAtlas = new Texture2D(chunkSize * textureSize, chunkSize * textureSize, TextureFormat.ARGB32, false);

            foreach (var kvp in keyValuePairs)
            {
                Vector2Int position = kvp.Key;
                Texture2D texture = kvp.Value;

                CheckTextureSize(texture, textureSize);

                textureAtlas.SetPixels(position.x * textureSize, position.y * textureSize, texture.width, texture.height, texture.GetPixels());
            }

            ApplyTextureSettings(textureAtlas);

            return textureAtlas;
        }

        private void CheckTextureSize(Texture2D texture, int textureSize)
        {
            if (texture.width != textureSize || texture.height != textureSize)
                throw new ArgumentException("All textures in the atlas should be of size 16x16 pixels. Check the textures you're providing");
        }

        private void ApplyTextureSettings(Texture2D texture)
        {
            texture.filterMode = FilterMode.Point;
            texture.Apply();

            MeshRenderer.material.SetFloat("_RotationX", 180);
            MeshRenderer.material.SetFloat("_RotationZ", -180);
        }
        #endregion

        #endregion

        #region Public Methods
        public void SetActive(bool active)
        {
            Active = active;
            ChunkObject.SetActive(active);
        }

        public void Draw()
        {
            if (Active)
            {
                Gizmos.color = CenterChunk ? CenterColor : ActiveColor;
            }
            else
            {
                Gizmos.color = DefaultColor;
            }

            Gizmos.DrawWireCube(WorldPos, new Vector2(ChunkSize * (Active ? 0.9f : 1), ChunkSize * (Active ? 0.9f : 1)));
        }
        #endregion
    }
}