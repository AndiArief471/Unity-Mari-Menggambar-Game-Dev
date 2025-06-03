using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public Transform player;
    public float visionRange = 100f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    public bool playerInSight;
    public Vector2 lastSeenPosition;

    void Update()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < visionRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, visionRange, playerLayer | obstacleLayer);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                playerInSight = true;
                lastSeenPosition = player.position;
            }
            else
            {
                playerInSight = false;
            }
        }
        else
        {
            playerInSight = false;
        }
    }
}