using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Transform topLeft;
    [SerializeField] private Transform bottomRight;
    [SerializeField] private float size;

    public Vector3 GetNearestGrid(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x / size);
        int y = Mathf.RoundToInt(pos.y / size);
        int z = Mathf.RoundToInt(pos.z / size);
        Vector3 res = new Vector3((float)x * size, (float)y * size, (float)z * size);
        if (res.x <= topLeft.position.x || res.z >= topLeft.position.z || res.x >= bottomRight.position.x || res.z <= bottomRight.position.z)
            return new Vector3(10000, 10000, 10000);
        return res;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = -5 + transform.position.x; x < 6 + transform.position.x; x++)
        {
            for (float z = -5 + transform.position.z; z < 6 + transform.position.z; z++)
            {
                Vector3 p = GetNearestGrid(new Vector3(x, 0, z));
                if (p != new Vector3(10000, 10000, 10000))
                    Gizmos.DrawWireCube(p, new Vector3(2f, 0, 2f));
            }
        }
    }
}
