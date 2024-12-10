
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Systems.WorldGenerator_
{
    public class NoiseMap : INoiseMapGenerator
    {
        private float[,] noiseMap;

        public float[,] GenerateMap(NoiseMapSettings nms, Bounds chunkBounds, int noiseSize)
        {
            noiseMap = new float[noiseSize, noiseSize];
            NativeArray<float> noiseMapNative = new NativeArray<float>(noiseSize * noiseSize, Allocator.TempJob);

            NoiseJob noiseJob = new NoiseJob
            {
                offset_x = nms.offset.x,
                offset_y = nms.offset.y,
                scale = nms.scale,
                distortionStrength = nms.distortionStrength,
                ridged = nms.ridged,
                warpFactor = nms.warpFactor,
                combineNoiseWeight = nms.combineNoiseWeight,
                persistence = nms.persistence,
                spotMultiplier = nms.spotMultiplier,
                spotTreschold = nms.spotTreschold,
                seaLevel = nms.seaLevel,
                matericLevel = nms.matericLevel,
                smotheFactor = nms.smotheFactor,
                mFactor = nms.mFactor,
                amplitude = nms.amplitude,
                frequency = nms.frequency,
                octaves = nms.octaves,
                lacunarity = nms.lacunarity,
                chunkBounds = chunkBounds,
                noiseSize = noiseSize,
                noiseMap = noiseMapNative
            };

            JobHandle handle = noiseJob.Schedule(noiseSize * noiseSize, 64);
            handle.Complete();

            for (int i = 0; i < noiseSize * noiseSize; i++)
            {
                int x = i / noiseSize;
                int y = i % noiseSize;
                noiseMap[x, y] = noiseMapNative[i];
            }

            noiseMapNative.Dispose();

            return noiseMap;
        }
    }
}