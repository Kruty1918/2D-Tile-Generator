using System.Collections.Generic;
using UnityEngine;

namespace Systems.GridSystem_
{
    public class Grid<T>
    {
        private Dictionary<Vector2Int, T> elements = new Dictionary<Vector2Int, T>();
        public Dictionary<Vector2Int, T> Elements { get { return elements; } }

        public void AddElement(Vector2Int elementCoord, T elementToAdd)
        {
            if (!ContainsElement(elementCoord)) elements.Add(elementCoord, elementToAdd);           
        }

        public void RemoveElement(Vector2Int elementCoord)
        {
            if (ContainsElement(elementCoord)) elements.Remove(elementCoord);
        }
      
        public T GetElement(Vector2Int elementCoord)
        {
            if (ContainsElement(elementCoord)) return elements[elementCoord];
            else return default;
        }

        public bool ContainsElement(Vector2Int elementCoordToCheck)
        {
            if (elements.ContainsKey(elementCoordToCheck)) return true;
            else return false;
        }

        public void ClearElements()
        {
            elements.Clear();
        }
    }
}
