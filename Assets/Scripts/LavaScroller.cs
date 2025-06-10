using UnityEngine;

public class LavaLoop : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public Transform lava1;
    public Transform lava2;
    public Transform lava3;

    private float spriteWidth;

    void Start()
    {
        spriteWidth = lava1.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        lava1.position += Vector3.left * scrollSpeed * Time.deltaTime;
        lava2.position += Vector3.left * scrollSpeed * Time.deltaTime;
        lava3.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // Se lava1 saiu da tela, empurre ela pra frente
        if (lava1.position.x < -spriteWidth)
            lava1.position = new Vector3(lava2.position.x + spriteWidth, lava1.position.y, lava1.position.z);

        // Se lava2 saiu da tela, empurre ela pra frente
        if (lava2.position.x < -spriteWidth)
            lava2.position = new Vector3(lava1.position.x + spriteWidth, lava2.position.y, lava2.position.z);

        // Se lava3 saiu da tela, empurre ela pra frente
        if (lava3.position.x < -spriteWidth)
            lava3.position = new Vector3(lava1.position.x + spriteWidth, lava3.position.y, lava3.position.z);
    }
}
