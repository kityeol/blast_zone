using UnityEngine;
using System.Collections;

public class KickCharacter : MonoBehaviour
{
    [SerializeField]
    float acceleration = 10;

    [SerializeField]
    float maxSpeed = 7;

    [SerializeField]
    float minSpeed = 2;

    [SerializeField]
    float speedChangeInterval = 0.5f;

    [SerializeField]
    float punchDuration = 0.5f;

    private float currentTargetSpeed;
    private bool punching;

    private float speed;

    private void Awake()
    {
        currentTargetSpeed = minSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            currentTargetSpeed = Mathf.Clamp(currentTargetSpeed + speedChangeInterval, minSpeed, maxSpeed);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            currentTargetSpeed = Mathf.Clamp(currentTargetSpeed - speedChangeInterval, minSpeed, maxSpeed);

        float moveInput = Input.GetAxisRaw("Horizontal");

        if (!punching)
            speed = Mathf.MoveTowards(speed, currentTargetSpeed * moveInput, acceleration * Time.deltaTime);

        if (!punching && Input.GetKeyDown(KeyCode.Space))
        {
            speed = 0;
            StartCoroutine(Punch());
        }

        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private IEnumerator Punch()
    {
        punching = true;

        yield return new WaitForSeconds(punchDuration);

        punching = false;
    }
}
