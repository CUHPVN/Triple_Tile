using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {  get; private set; }
    [SerializeField] private Animator animator;
    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool("IsRun", MapManager.Instance.GetMove());
    }
    public void Attack()
    {
        animator.SetBool("IsAttack", true);
        Invoke(nameof(StopAttack),0.5f);
    }
    public void StopAttack()
    {
        SoundManager.Instance.PlaySFX(SoundManager.SFX.Attack);
        animator.SetBool("IsAttack", false);
    }
}
