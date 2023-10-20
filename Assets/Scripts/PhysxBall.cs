using Fusion;
using UnityEngine;

public class PhysxBall : NetworkBehaviour
{
    [Networked] private TickTimer _life { get; set; }

    public void Init(Vector3 forward)
    {
        _life = TickTimer.CreateFromSeconds(Runner, 5.0f);
        GetComponent<Rigidbody>().velocity = forward;
    }

    public override void FixedUpdateNetwork()
    {
        if (_life.Expired(Runner))
            Runner.Despawn(Object);
    }
}
