using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                Move();
            }
        }

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
            return new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);
        }

        void Update()
        {
            transform.position = Position.Value;
        }
    }
}