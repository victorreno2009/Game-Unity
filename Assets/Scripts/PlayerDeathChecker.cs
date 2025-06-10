using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerDeathChecker : MonoBehaviour
{
    public Tilemap groundTilemap;
    public float checkOffset = 0.1f;

    void Update()
    {
        Vector3 positionBelow = transform.position + Vector3.down * checkOffset;
        Vector3Int cellPosition = groundTilemap.WorldToCell(positionBelow);

        if (groundTilemap.GetTile(cellPosition) == null)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("morreu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
