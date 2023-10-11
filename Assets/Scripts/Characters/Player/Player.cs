using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [field:Header("Refencias")]
    [field:SerializeField] public PlayerSO pData {get; private set;}

    [field: Header("Collisions")]
    [field: SerializeField] public CapsuleColliderUtility ColliderUtility { get; private set;}
    [field: SerializeField] public PlayerLayerData LayerData { get; private set;}   

    public Rigidbody Rigidbody { get; private set;}

    public PlayerInput Input { get; private set; }
    public Transform MainCamaraTransform { get; private set; }

    private PlayerMovementSTM MovementSTM;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Input = GetComponent<PlayerInput>();

        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimentions();

        MainCamaraTransform = Camera.main.transform;
        MovementSTM = new PlayerMovementSTM(this); 
    }

    private void Start()
    {
        Input = GetComponent<PlayerInput>();
        MovementSTM.ChangeState(MovementSTM.idleState);
        
    }

    private void OnValidate()
    {
        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimentions();
    }

    private void Update()
    {
        MovementSTM.HandleInput();
        MovementSTM.Update();
    }

    private void FixedUpdate()
    {
        MovementSTM.PhysicsUpdates();
    }
}
