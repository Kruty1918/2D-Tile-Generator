# 2D Tile Generator

This project is a 2D layered world generator that utilizes a chunk system for efficient generation and management of large maps. The world generation is based on a noise map to create a variety of landscapes.

## Features:
- **Layered Approach**: The world consists of multiple layers, allowing easy customization of different aspects of the landscape such as continents, seas, oceans, lakes, and varying elevations.
- **Chunk System**: The world is divided into chunks to optimize performance and reduce memory usage.
- **Noise Map**: The world generation relies on noise to create natural and random landscapes, including various types of terrain.
- **Diverse Landscapes**: Ability to generate continents, seas, oceans, lakes, mountains, and plains with different heights.
- **Parallel Execution with Job System**: To improve generation efficiency, Unity's Job System is used, allowing data to be processed in parallel.

This generator allows for the creation of large, detailed, and diverse 2D worlds for games with minimal resource consumption.
