using Sirenix.OdinInspector;
using System.Collections.Generic;

public class WorldManager : SerializedMonoBehaviour
{
    #region Singleton
    private static WorldManager instance;
    public static WorldManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WorldManager>();
            }
            return instance;
        }
    }
    #endregion  

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }
}
