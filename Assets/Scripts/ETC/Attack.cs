using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackAmout = 1;

    public virtual void MeleeAttack() { }

    public virtual void RangeAttack() { }

}
