using TMPro;
using UnityEngine;

public class BulletCounter : MonoBehaviour
{
    public TextMeshProUGUI bulletCounterText; // Reference to the TextMeshProUGUI component
    private int bulletCount = 0;

    void Start()
    {
        UpdateBulletCountText();
    }

    public void IncreaseBulletCount()
    {
        bulletCount++;
        UpdateBulletCountText();
    }

    public void DecreaseBulletCount()
    {
        bulletCount = Mathf.Max(0, bulletCount - 1); // Prevents bulletCount from going negative
        UpdateBulletCountText();
    }

    private void UpdateBulletCountText()
    {
        if (bulletCounterText != null)
        {
            bulletCounterText.text = $"Bullets Currently Alive: {bulletCount}";
        }
    }
}
