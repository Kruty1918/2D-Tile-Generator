using UnityEngine;

public class SimpleOutlineEditor : MonoBehaviour
{
    public Vector2 outlineSize = new Vector2(5f, 3f);

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // �������� ������� ��'���� � ������� �����������
        Vector3 objectPosition = transform.position;

        // ����� ���������
        float halfWidth = outlineSize.x * 0.5f;
        float halfHeight = outlineSize.y * 0.5f;

        // �������� ���������� ���� ���������
        Vector3 topLeft = new Vector3(objectPosition.x - halfWidth, objectPosition.y + halfHeight, objectPosition.z);
        Vector3 topRight = new Vector3(objectPosition.x + halfWidth, objectPosition.y + halfHeight, objectPosition.z);
        Vector3 bottomLeft = new Vector3(objectPosition.x - halfWidth, objectPosition.y - halfHeight, objectPosition.z);
        Vector3 bottomRight = new Vector3(objectPosition.x + halfWidth, objectPosition.y - halfHeight, objectPosition.z);

        // ������� �� ���������
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
