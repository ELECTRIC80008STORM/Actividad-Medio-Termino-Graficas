using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public enum ShootingPattern { Star, Lotus, HalfMoon }
    public ShootingPattern currentPattern = ShootingPattern.Star;

    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float shootInterval = 1f; // Time between shots
    public BulletCounter bulletCounter; // Reference to the BulletCounter component
    public float bulletSpeed = 5f; // Speed of the bullets

    private float shootTimer;
    private float patternTimer = 0f; // Timer for switching patterns
    private float patternChangeInterval = 10f; // Interval for changing patterns (10 seconds)


    void Update()
    {
        shootTimer += Time.deltaTime;
        patternTimer += Time.deltaTime;

        // Change pattern every 10 seconds
        if (patternTimer >= patternChangeInterval)
        {
            // Cycle through the patterns
            if (currentPattern == ShootingPattern.Star)
            {
                currentPattern = ShootingPattern.Lotus;
            }
            else if (currentPattern == ShootingPattern.Lotus)
            {
                currentPattern = ShootingPattern.HalfMoon;
            }
            else
            {
                currentPattern = ShootingPattern.Star;
            }

            patternTimer = 0f; // Reset the pattern timer
        }

        if (shootTimer >= shootInterval)
        {
            // Call the selected shooting pattern based on the currentPattern value
            switch (currentPattern)
            {
                case ShootingPattern.Star:
                    StarShoot();
                    break;
                case ShootingPattern.Lotus:
                    LotusShoot();
                    break;
                case ShootingPattern.HalfMoon:
                    HalfMoon();
                    break;
            }

            shootTimer = 0f; // Reset the shooting interval timer
        }
    }

    // Chatgpt Suggestion after BatchRunner couldn't be imported, displays the different graphs given the information obtained
    // OpenAI. (2024). ChatGPT (Aug 23 version) [Large language model]. https://chat.openai.com/chat
    void StarShoot()
    {
        bulletSpeed = 8f;

        int points = 5; // Number of points in the star
        float angleStep = 360f / points; // Angle between each point

        for (int i = 0; i < points; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.back;

            ShootBullet(direction);
        }
    }

    void LotusShoot()
    {
        bulletSpeed = 5f;

        int layers = 5; // Number of petals
        int bulletsPerLayer = 12; // Number of bullets per petal
        float angleStep = 360f / bulletsPerLayer; // Angle between each bullet in a petal
        float rotationStep = 15f; // Rotation difference between petals

        for (int j = 0; j < layers; j++)
        {
            float currentRotation = j * rotationStep; // Rotate each layer slightly

            for (int i = 0; i < bulletsPerLayer; i++)
            {
                float angle = i * angleStep + currentRotation;
                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.back;

                ShootBullet(direction);
            }
        }
    }

    void HalfMoon()
    {
        bulletSpeed = 3f;

        int bulletCount = 15; // Number of bullets in the half-moon
        float arcAngle = 180f; // Total spread of the arc (half-moon)
        float startAngle = -arcAngle / 2; // Starting angle of the arc

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + i * (arcAngle / (bulletCount - 1));
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.back;

            ShootBullet(direction);
        }
    }

    void ShootBullet(Vector3 direction)
    {
        // Instantiate a bullet at the generator's position
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(direction));

        // Set the bullet's speed
        BulletMechanics bulletMechanics = bullet.GetComponent<BulletMechanics>();
        if (bulletMechanics != null)
        {
            bulletMechanics.speed = bulletSpeed; // Apply the current bullet speed
            bulletMechanics.OnBulletDestroyed += bulletCounter.DecreaseBulletCount;
        }

        // Increase the bullet count in the counter
        if (bulletCounter != null)
        {
            bulletCounter.IncreaseBulletCount();
        }
    }

}
