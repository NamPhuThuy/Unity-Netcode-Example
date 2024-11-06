using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovementController : NetworkBehaviour
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

    private float _horiInput, _vertInput;
    private Vector2 _direction;
    private float _speed = 5f;

    private int _faceDirection;

    public override void OnNetworkSpawn()
    {
        
    }
    
    
    
    /*private void Update()
    {
        if (!IsOwner) return;
        MovementHandle();
        transform.position = Position.Value;
    }*/

    public void MovementHandle()
    {
        if (!IsOwner) return;
        _horiInput = Input.GetAxis("Horizontal");
        _vertInput = Input.GetAxis("Vertical");
        _direction = new Vector2(_horiInput, _vertInput).normalized;

        if (Mathf.Abs(_horiInput) > Mathf.Epsilon
            || Mathf.Abs(_vertInput) > Mathf.Epsilon)
        {
            SendMovementInforRpc();
        }
    }

    [Rpc(SendTo.Server)]
    private void SendMovementInforRpc(RpcParams rpcParams = default)
    {
        Debug.Log($"Server Received the movement request on NetworkObject #{NetworkObjectId}");
        Position.Value = (Vector2)transform.position + _direction * (_speed * Time.deltaTime);
    }
    
    
    //--MOVE STEP BY STEP
    public void Move()
    {
        SubmitPositionRequestRpc();
    }
    
    [Rpc(SendTo.Server)]
    void SubmitPositionRequestRpc(RpcParams rpcParams = default)
    {
        Debug.Log($"Server Received the movement request on NetworkObject #{NetworkObjectId}");
        var randomPosition = GetRandomPositionOnPlane();
        transform.position = randomPosition;
        Position.Value = randomPosition;
    }
    
    static Vector3 GetRandomPositionOnPlane()
    {
        Vector2 res = Random.insideUnitCircle * 4.6f;
        return res;
    }
    
    void Update()
    {
        transform.position = Position.Value;
    }
}
