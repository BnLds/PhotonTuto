using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private NetworkCharacterControllerPrototype _cc;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterControllerPrototype>();
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData data))
        {
            //The provided input is normalized to prevent cheating
            data.Direction.Normalize();
            _cc.Move(5 * data.Direction * Runner.DeltaTime);
        }
    }
}
