using System;
using System.Collections.Generic;
using UnityEngine;

namespace Systems.ChunkSystem_
{
    public class ChunkPool
    {
        private Queue<Chunk> pool;
        private int maxPoolSize;

        public ChunkPool(int maxPoolSize)
        {
            this.maxPoolSize = maxPoolSize;
            this.pool = new Queue<Chunk>(maxPoolSize);
        }

        public Chunk Instantiate(Vector2 position, int chunkSize)
        {
            Chunk newChunk = null;

            if (pool.Count > 0)
            {
                newChunk = pool.Dequeue();
                //newChunk.UpdateChunk(position);

                Debug.Log("Використано чанк з пулу.");
            }
            else if (pool.Count < maxPoolSize)
            {
                newChunk = new Chunk(position, chunkSize, active => {
                    if (!active)
                    {
                        // Перевіряємо, чи пул повний, перед тим як повернути чанк
                        if (!IsFull())
                        {
                            ReturnToPool(newChunk);
                        }
                        else
                        {
                            // Знищуємо чанк або обробляємо цю ситуацію іншим чином
                            Debug.LogError("Пул чанків повний. Неможливо повернути чанк до пулу.");
                        }
                    }
                });

                Debug.Log("Створено новий чанк, оскільки пул не був повним.");
            }
            else
            {
                Debug.LogError("Пул чанків повний. Неможливо створити новий чанк.");
                throw new InvalidOperationException("Пул чанків повний. Неможливо створити новий чанк.");
            }

            return newChunk;
        }

        public void ReturnToPool(Chunk chunk)
        {
            if (pool.Count >= maxPoolSize)
            {
                Debug.LogError("Пул чанків повний. Неможливо повернути чанк до пулу.");
                throw new InvalidOperationException("Пул чанків повний. Неможливо повернути чанк до пулу.");
            }

            pool.Enqueue(chunk);
            Debug.Log("Повернуто чанк до пулу.");
        }

        public bool IsFull()
        {
            return pool.Count >= maxPoolSize;
        }

        public bool IsEmpty()
        {
            return pool.Count == 0;
        }

        public void Clear()
        {
            pool.Clear();
            Debug.Log("Cleared the pool.");
        }
    }
}
