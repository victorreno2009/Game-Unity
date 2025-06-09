using UnityEngine;

public class LavaBackground : MonoBehaviour
{
    public float scrollSpeed = 0.1f;

    private Material lavaMaterial;
    private Vector2 offset;

    void Start()
    {
        // Pega o material do Sprite Renderer e instancia
        lavaMaterial = new Material(GetComponent<SpriteRenderer>().material);
        GetComponent<SpriteRenderer>().material = lavaMaterial;
    }

    void Update()
    {
        offset.x += scrollSpeed * Time.deltaTime;
        lavaMaterial.mainTextureOffset = offset;
    }
}
