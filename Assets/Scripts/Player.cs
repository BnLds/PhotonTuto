using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Ball _prefabBall;
    [SerializeField] private PhysxBall _prefabPhysxBall;

    [Networked] private TickTimer _delay { get; set; }

    private Vector3 _forward;
    private NetworkCharacterControllerPrototype _cc;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
        _forward = transform.forward;
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData data))
        {
            //The provided input is normalized to prevent cheating
            data.Direction.Normalize();
            _cc.Move(5 * data.Direction * Runner.DeltaTime);

            if (data.Direction.sqrMagnitude > 0)
                _forward = data.Direction;

            if(_delay.ExpiredOrNotRunning(Runner))
            {
                if((data.Buttons & NetworkInputData.MOUSE_BUTTON_1) != 0)
                {
                    _delay = TickTimer.CreateFromSeconds(Runner, .5f);
                    Runner.Spawn(_prefabBall,
                    transform.position + _forward, Quaternion.LookRotation(_forward),
                    Object.InputAuthority, (runner, o) =>
                    {
                        // Initialize the Ball before synchronizing it
                        o.GetComponent<Ball>().Init();
                    });
                }
                else if((data.Buttons & NetworkInputData.MOUSE_BUTTON_2) != 0)
                {
                    _delay = TickTimer.CreateFromSeconds(Runner, .5f);
                    Runner.Spawn(_prefabPhysxBall,
                    transform.position + _forward, Quaternion.LookRotation(_forward),
                    Object.InputAuthority, (runner, o) =>
                    {
                        o.GetComponent<PhysxBall>().Init(10 * _forward);
                    });
                }

            }
        }
    }
}
