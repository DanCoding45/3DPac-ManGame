using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    public float moveSpeed = 2.0f;
    public float timeToChangeDestination = 5.0f;
    public float detectionRadius = 3.0f;
    public float chaseSpeed = 5.0f;
    public Transform player;
    public float minDistanceToPlayer = 1.0f;
    public PlayerPowerUp playerPowerUp;

    [SerializeField] private AudioSource guardTouchPlayerAudio; // Serialized field for the audio source

    private Vector3 centerPoint;
    private Vector3 randomDestination;
    private float timer;
    private bool isChasingPlayer = false;
    private bool isFleeing = false;
    private bool isKilled = false;

    private float fleeDuration = 10.0f;
    private float fleeTimer = 0.0f;
    private bool fleeIndefinitely = false;

    void Start()
    {
        centerPoint = (waypoint1.position + waypoint2.position) / 2;
        CalculateRandomDestination();
    }

    void Update()
    {
        if (isKilled) return;

        if (isChasingPlayer)
        {
            ChasePlayer();
        }
        else if (isFleeing)
        {
            FleeFromPlayer();
            fleeTimer += Time.deltaTime;
            if (!fleeIndefinitely && fleeTimer >= fleeDuration)
            {
                isFleeing = false;
                fleeTimer = 0.0f;
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, randomDestination, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, randomDestination) < 0.1f || timer >= timeToChangeDestination)
        {
            CalculateRandomDestination();
            timer = 0;
        }

        timer += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius && !isChasingPlayer && !isFleeing)
        {
            StartChasing(player);
        }
    }

    void CalculateRandomDestination()
    {
        float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float randomX = centerPoint.x + Mathf.Cos(randomAngle) * (Vector3.Distance(waypoint1.position, waypoint2.position) / 2);
        float randomZ = centerPoint.z + Mathf.Sin(randomAngle) * (Vector3.Distance(waypoint1.position, waypoint2.position) / 2);
        randomDestination = new Vector3(randomX, transform.position.y, randomZ);
    }

    void ChasePlayer()
    {
        float step = chaseSpeed * Time.deltaTime;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > minDistanceToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }

        if (distanceToPlayer > detectionRadius)
        {
            isChasingPlayer = false;
        }
    }

    void FleeFromPlayer()
    {
        Vector3 fleeDirection = transform.position - player.position;
        fleeDirection.Normalize();
        transform.position = Vector3.MoveTowards(transform.position, transform.position + fleeDirection, moveSpeed * Time.deltaTime);
    }

    public void StartChasing(Transform target)
    {
        isChasingPlayer = true;
        player = target;
    }

    public void TryKillGuard()
    {
        if (playerPowerUp != null && playerPowerUp.IsPoweredUp)
        {
            isFleeing = true;
        }
    }

    public void TryFlee()
    {
        if (isChasingPlayer || (playerPowerUp != null && playerPowerUp.GetCollectedTreasureBoxesCount() >= playerPowerUp.requiredTreasureBoxes))
        {
            isFleeing = true;
            fleeTimer = 0.0f;
            fleeIndefinitely = true;
        }
    }

    public void GuardKilled()
    {
        isKilled = true;
    }

    // Call this method when the player touches the guard
    public void PlayerTouchesGuard()
    {
        // Play audio when the player touches the guard
        if (guardTouchPlayerAudio != null)
        {
            guardTouchPlayerAudio.Play();
        }
    }
}
