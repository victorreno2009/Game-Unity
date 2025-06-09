using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerDeathChecker : MonoBehaviour
{
    public Tilemap groundTilemap; // Referência ao Tilemap do chão
    public float checkOffset = 0.1f; // Um pequeno deslocamento para pegar o tile sob os pés do personagem

    void Update()
    {
        Vector3 positionBelow = transform.position + Vector3.down * checkOffset;
        Vector3Int cellPosition = groundTilemap.WorldToCell(positionBelow);

        if (groundTilemap.GetTile(cellPosition) == null)
        {
            // O personagem está fora do chão
            Die();
        }
    }

    void Die()
    {
        Debug.Log("O personagem caiu e morreu!");
        // Aqui você pode ativar animação de morte, reiniciar cena etc.
    }
}
