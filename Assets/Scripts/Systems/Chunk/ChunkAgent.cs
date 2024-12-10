using Mediators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.ChunkSystem_
{
    public class ChunkAgent : MonoBehaviour
    {
        private List<Chunk> neighbourChunks = new List<Chunk>();
        private Chunk centerChunk;

        public Chunk CenterChunk
        {
            get => centerChunk;
            set
            {
                centerChunk = value;
                StopCurrentCoroutine();
                currentCoroutine = StartCoroutine(CheckChunkBounds());
            }
        }

        private Coroutine currentCoroutine;
        private WaitWhile WaitWhileInChunk => new WaitWhile(() => centerChunk != null && centerChunk.Bounds.Contains(transform.position));

        private void StopCurrentCoroutine()
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
        }

        private IEnumerator CheckChunkBounds()
        {
            yield return WaitWhileInChunk;
            AgentOutCenterChunk();
            currentCoroutine = null;
        }

        private void AgentOutCenterChunk()
        {
            Vector2Int direction = BoundsExtensions.GetChunkDirection(transform.position, centerChunk.Bounds);
            Vector2Int nextCenterChunkCoord = new Vector2Int(centerChunk.Coord.x + direction.x, centerChunk.Coord.y + direction.y);

            GetNeighbourChunks(centerChunk.Coord);

            Chunk newCenterChunk = GridSystem.GetElement<Chunk>(GridId.ChunkGrid, nextCenterChunkCoord);
            CenterChunk = newCenterChunk;
           
            SM.Instance<ChunkSystem>().SetNeighbourChunks(centerChunk);
        }

        private void GetNeighbourChunks(Vector2Int centerChunkCoord)
        {
            var chunkSystem = SM.Instance<ChunkSystem>();

            SM.Instance<ChunkSystem>().pastVisibleChunks.Clear();

            if (neighbourChunks.Count > 0) neighbourChunks.Clear();
            neighbourChunks = FindNeighbourElements.Find(GridSystem.GetAllElement<Chunk>(GridId.ChunkGrid), centerChunkCoord, chunkSystem.FieldOfView, true);

            for (int i = 0; i < neighbourChunks.Count; i++)
            {
                if (!chunkSystem.pastVisibleChunks.ContainsKey(neighbourChunks[i].Coord))
                {
                    chunkSystem.pastVisibleChunks.Add(neighbourChunks[i].Coord, neighbourChunks[i]);
                }
            }

        }
    }
}


