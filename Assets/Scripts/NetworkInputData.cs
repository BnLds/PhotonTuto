using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public const byte MOUSE_BUTTON_1 = 0x01;
    public const byte MOUSE_BUTTON_2 = 0x02;

    public byte Buttons;
    public Vector3 Direction;
}
