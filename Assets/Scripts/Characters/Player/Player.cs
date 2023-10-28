using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [field: Header("Refencias")]
    [field: SerializeField] public PlayerSO pData { get; private set; }

    [field: Header("Collisions")]
    [field: SerializeField] public PlayerCapsuleColliderUtility ColliderUtility { get; private set; }
    [field: SerializeField] public PlayerLayerData LayerData { get; private set; }
    
    [field: Header("Camara")]
    [field: SerializeField] public PlayerCamaraUtility camaraUtility { get; private set; }
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    public PlayerInput Input { get; private set; }
    public Transform MainCamaraTransform { get; private set; }

    private PlayerMovementSTM MovementSTM;
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Input = GetComponent<PlayerInput>();

        ColliderUtility.Initialize(gameObject);
        ColliderUtility.CalculateCapsuleColliderDimentions();
        camaraUtility.Inicialaze();
        
        AnimationData.Initialize();
        Animator= GetComponent<Animator>();
        
        MainCamaraTransform = Camera.main.transform;
        MovementSTM = new PlayerMovementSTM(this);
    }

    private void Start()
    {

        MovementSTM.ChangeState(MovementSTM.idleState);

    }

    private void OnTriggerEnter(Collider collider)
    {
        MovementSTM.OnTriggerEnter(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        MovementSTM.OnTriggerExit(collider);
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

    public void OnMovementStateAnimationEnterEvent()
    {
        if (IsInAnimationTransition())
        {
            return;
        }
        MovementSTM.OnAnimationEnterEvent();
    }

    public void OnMovementStateAnimationTransitionEvent()
    {
        if (IsInAnimationTransition())
        {
            return;
        }
        MovementSTM.OnAnimationTransitionEvent();
    }

    public void OnMovementStateAnimationExitEvent()
    {
        if (IsInAnimationTransition())
        {
            return;
        }
        MovementSTM.OnAnimationExitEvent();
    }

    private bool IsInAnimationTransition (int layer =0)
    {
        return Animator.IsInTransition(layer);
    }
}
