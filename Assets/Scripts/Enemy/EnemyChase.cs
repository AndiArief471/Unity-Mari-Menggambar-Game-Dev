using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed = 2f;
    private EnemySight sight;
    public Transform player;
    private bool reachedLastSeen = true;
    private float stopDistance = 0.1f;

    void Start()
    {
        sight = GetComponent<EnemySight>();
    }

    void Update()
    {
        if (sight.playerInSight)
        {
            reachedLastSeen = false;
            MoveTowards(sight.player.position);
        }
        else if (!reachedLastSeen)
        {
            MoveTowards(sight.lastSeenPosition);

            // Stop if close enough to last seen position
            if (Vector2.Distance(transform.position, sight.lastSeenPosition) < stopDistance)
            {
                reachedLastSeen = true;
            }
        }
    }

    void MoveTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}