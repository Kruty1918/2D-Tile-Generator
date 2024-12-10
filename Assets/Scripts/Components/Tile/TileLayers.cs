using System;

[Flags]
public enum TileLayers
{
    None = 0,
    Grass = 1,
    Sand = 2,
    Water = 4,
    Everything = 0b1111
}
