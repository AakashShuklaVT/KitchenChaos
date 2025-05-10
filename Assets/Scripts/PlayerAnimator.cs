using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private const string IS_Walking = "IsWalking";
    [SerializeField] private Player player;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_Walking, player.IsWalking());
    }
    void Update()
    {
        animator.SetBool(IS_Walking, player.IsWalking());
    }
}
