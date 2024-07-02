using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    // The instance of the Singleton
    public static TilemapManager Instance { get; private set; }
    // The Tilemap to be shared across all instances
    public Tilemap wallTilemap;
    private void Awake()
    {
        if (Instance == null){
            // set this instance as the Singleton instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
