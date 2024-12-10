using Mediators.Singleton;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

namespace Systems.ChunkSystem_
{

    public class ChunkSystem : MonoSingleton<ChunkSystem>
    {
        [Min(1)]
        [SerializeField] private int fieldOfView = 1;
        [Min(5)]
        [SerializeField] private int chunkSize;
        [SerializeField] private ChunkAgent agent;
        public Dictionary<Vector2Int, Chunk> pastVisibleChunks = new Dictionary<Vector2Int, Chunk>();
        private Dictionary<Vector2Int, Chunk> newVisibleChunks = new Dictionary<Vector2Int, Chunk>();

        private List<Chunk> newCreatedChunks = new List<Chunk>();
        private ChunkPool chunkPool;

        public int ChunkSize { get { return chunkSize; } }
        public int FieldOfView { get { return fieldOfView; } }

        private void OnDrawGizmos()
        {
            if (GridSystem.GetAllElement<Chunk>(GridId.ChunkGrid).Count > 0 && Application.isPlaying)
            {
                Vector2Int[] keys = new Vector2Int[GridSystem.GetAllElement<Chunk>(GridId.ChunkGrid).Count];
                GridSystem.GetAllElement<Chunk>(GridId.ChunkGrid).Keys.CopyTo(keys, 0);

                foreach (var key in keys)
                {
                    Chunk chunk = GridSystem.GetAllElement<Chunk>(GridId.ChunkGrid)[key];
                    chunk.Draw();
                }
            }
        }

        private void Start()
        {
            chunkPool = new ChunkPool(16);
            SetStartChunks(Vector2.zero);
        }

        private void SetStartChunks(Vector2 centerPos)
        {
            if (newCreatedChunks.Count > 0) newCreatedChunks.Clear();

            for (int x = -fieldOfView; x <= fieldOfView; x++)
            {
                for (int y = -fieldOfView; y <= fieldOfView; y++)
                {
                    Vector2 chunkWorldPos = new Vector2(centerPos.x + x * chunkSize, centerPos.y + y * chunkSize);

                    Chunk newChunk = chunkPool.Instantiate(chunkWorldPos, chunkSize);

                    newCreatedChunks.Add(newChunk);
                }
            }

            foreach (Chunk chunk in newCreatedChunks)
            {
                if (chunk.WorldPos == centerPos) agent.CenterChunk = chunk;
                chunk.SetActive(true);
                pastVisibleChunks.Add(chunk.Coord, chunk);
            }
        }


        public void SetNeighbourChunks(Chunk centerChunk)
        {
            newVisibleChunks.Clear();

            List<Vector2> newCoords = new List<Vector2>();


            for (int x = -fieldOfView; x <= fieldOfView; x++)
            {
                for (int y = -fieldOfView; y <= fieldOfView; y++)
                {
                    Vector2Int neighbourCoord = new Vector2Int(centerChunk.Coord.x + x, centerChunk.Coord.y + y);

                    //Якщо навколо центрального чанку є не існуючий чанк 
                    if (!GridSystem.ContainsElement<Chunk>(GridId.ChunkGrid, neighbourCoord))
                    {
                        Vector2 chunkWorldPos = new Vector2(centerChunk.WorldPos.x + x * chunkSize, centerChunk.WorldPos.y + y * chunkSize);
                        newCoords.Add(chunkWorldPos);
                        //createdChunks.Add(new Chunk(chunkWorldPos, chunkSize));
                    }
                    else
                    {
                        Chunk chunk = GridSystem.GetElement<Chunk>(GridId.ChunkGrid, neighbourCoord);
                        newVisibleChunks.Add(chunk.Coord, chunk);

                        GridSystem.RemoveElement<Chunk>(GridId.ChunkGrid, chunk.Coord);
                        GridSystem.AddElement<Chunk>(GridId.ChunkGrid, chunk.Coord, chunk);
                    }
                }
            }

            //foreach (Chunk chunk in pastVisibleChunks.Values)
            //{
            //    if (!newVisibleChunks.ContainsKey(chunk.Coord))
            //    {
            //        if (chunk.Active != false)
            //        {
            //            GridSystem.RemoveElement<Chunk>(GridId.ChunkGrid, chunk.Coord);

            //            chunk.UpdateChunk(newCoords.FirstOrDefault());
            //            newCoords.Remove(newCoords.FirstOrDefault());

            //            GridSystem.AddElement<Chunk>(GridId.ChunkGrid, chunk.Coord, chunk);

            //        }
            //    }
            //}

            Debug.Log(GridSystem.GetAllElement<Chunk>(GridId.ChunkGrid).Count);
        }

        private void SetVisibleChunks()
        {
            foreach (Chunk chunk in pastVisibleChunks.Values)
            {
                if (!newVisibleChunks.ContainsKey(chunk.Coord))
                {
                    if (chunk.Active != false)
                    {
                        chunk.SetActive(false);
                    }
                }
            }

            foreach (Chunk chunk in newVisibleChunks.Values)
            {
                if (chunk.Active != true)
                {
                    chunk.SetActive(true);
                }
            }
        }

        public bool IsCentralChunk(Chunk chunk)
        {
            return chunk == agent.CenterChunk;
        }
        
    }
}