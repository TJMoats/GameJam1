using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : SerializedMonoBehaviour
{
    [SerializeField, ReadOnly]
    private string guid;
    public string GUID
    {
        get
        {
            if (guid == "")
            {
                guid = System.Guid.NewGuid().ToString();
            }
            return guid;
        }
        private set => guid = value;
    }

    public static Vector2 GetSpawnPosition(string _guid)
    {
        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
        foreach (SpawnPoint sp in spawnPoints)
        {
            if (sp.GUID == _guid)
            {
                return sp.transform.position;
            }
        }
        if (spawnPoints.Length > 0)
        {
            return spawnPoints[0].transform.position;
        }
        return Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, .2f, .8f, .5f);
        Gizmos.DrawSphere(transform.position, .5f);
    }

    private void OnValidate()
    {
        GUID = GUID;
    }
}
