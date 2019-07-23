using UnityEngine;

public class KickCharacterAnimator : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    private KickCharacter kickCharacter;

    private void Awake()
    {
        kickCharacter = GetComponent<KickCharacter>();

        kickCharacter.OnKick += KickCharacter_OnKick;
    }

    private void KickCharacter_OnKick()
    {
        animator.SetTrigger("Punch");
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", kickCharacter.speed);
    }
}
