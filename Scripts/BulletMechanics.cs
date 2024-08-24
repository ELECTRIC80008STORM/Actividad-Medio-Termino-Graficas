using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMechanics : MonoBehaviour
{
    public float speed = 10f; // Speed at which the bullet moves
    public float lifetime = 5f; // How long the bullet will stay alive

    // Chatgpt Suggestion after BatchRunner couldn't be imported, displays the different graphs given the information obtained
    // OpenAI. (2024). ChatGPT (Aug 23 version) [Large language model]. https://chat.openai.com/chat
    // Delegate and event to notify when the bullet is destroyed
    public delegate void BulletDestroyed();
    public event BulletDestroyed OnBulletDestroyed;

    void Start()
    {
        // Schedule the bullet for destruction after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the bullet along the negative Z axis
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnDestroy()
    {
        // Notify subscribers (like the BulletCounter) that this bullet was destroyed
        if (OnBulletDestroyed != null)
        {
            OnBulletDestroyed();
        }
    }

}
