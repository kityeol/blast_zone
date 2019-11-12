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
    float waveDuration = 3.0f;

    private float currentTargetSpeed;
    private bool wave;
    public GameObject friend;
    public Animator anim;
    private float speed;
    public ParticleSystem friendSparkles;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentTargetSpeed = minSpeed;
    }

    private void Update()
    {
        #region Logic
        if (Input.GetKeyDown(KeyCode.UpArrow))
            currentTargetSpeed = Mathf.Clamp(currentTargetSpeed + speedChangeInterval, minSpeed, maxSpeed);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            currentTargetSpeed = Mathf.Clamp(currentTargetSpeed - speedChangeInterval, minSpeed, maxSpeed);

        float moveInput = Input.GetAxisRaw("Vertical");
        float rotInput = Input.GetAxisRaw("Horizontal");

        if (!wave)
            speed = Mathf.MoveTowards(speed, currentTargetSpeed * moveInput, acceleration * Time.deltaTime);

        if (!wave && Input.GetMouseButtonDown(0))
        {
            speed = 0;
            StartCoroutine(Wave());
        }

        transform.position += Vector3.right * speed * Time.deltaTime;

        #endregion
        anim.SetFloat("Speed", speed);
    }

    private IEnumerator Wave()
    {
        wave = true;
        friendSparkles.GetComponent<ParticleSystem>().Play();
        GetComponent<Animator>().SetTrigger("Wave");
        if(Vector3.Distance(GetComponent<Transform>().position, friend.GetComponent<Transform>().position) < 100f)
        {
            friendSparkles.GetComponent<ParticleSystem>().Play();
            friend.GetComponent<Animator>().SetTrigger("WaveBack");
        }
        yield return new WaitForSeconds(waveDuration);
        wave = false;
    }
}