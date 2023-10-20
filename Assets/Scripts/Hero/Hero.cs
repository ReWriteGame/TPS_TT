using UnityEngine;
using System;
using Class.Score;

[SelectionBase]
public class Hero : MonoBehaviour, IInitializable, IDamageble
{
    [HideInInspector] public Vector2 inputUserDirection;

    [SerializeField] private Character character;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float sprintSpeed = 5;
    [Range(0.0f, 0.5f)]
    [SerializeField] private float rotationSmoothTime = 0.12f;

    [SerializeField] private float speedChangeRate = 10.0f;
    [SerializeField] private float speedOffset = 0.1f;

    [SerializeField] private bool sprint = false;
    [SerializeField] private bool analogMovement = true;

    [SerializeField] private Score health;
    [SerializeField] private HeroAnimationEvents heroAnimationEvents;
    [SerializeField] private SlotsSystem slots;
    [SerializeField] private GameObject handItem;

    public Action OnGroundPowerCastSkill;
    public Action OnActivateSkill;
    public Action OnShootAction;

    public Action OnJump;
    public Action OnDied;


    private Camera playerCamera;
    private float speed;
    private float targetSpeed;
    private float currentHorizontalSpeed;
    private float inputMagnitude;
    private bool isDeath = false;

    public bool Sprint { get => sprint; set => sprint = value; }
    public Character Character => character;
    public float TargetSpeed => targetSpeed;
    public float SpeedChangeRate => speedChangeRate;
    public SlotsSystem Slots => slots;

    private void Start() => Initialize();
    private void OnDestroy() => Unsubscribe();
    private void FixedUpdate() => MoveLogic();

    public void Initialize()
    {
        playerCamera = Camera.main;
        Subscribe();
    }

    private void Subscribe()
    {
        character.OnJump += OnJump;
        heroAnimationEvents.OnShootEvent += FireWrapper;
        slots.OnAddObject += TakeNewItem;
        health.OnReachMinValue += Died;
    }

    private void Unsubscribe()
    {
        character.OnJump -= OnJump;
        heroAnimationEvents.OnShootEvent -= FireWrapper;
        slots.OnAddObject -= TakeNewItem;
        health.OnReachMinValue -= Died;
    }

    private void MoveLogic()
    {
        if (isDeath) return;
        targetSpeed = sprint ? sprintSpeed : moveSpeed;
        if (inputUserDirection == Vector2.zero) targetSpeed = 0.0f;

        inputMagnitude = analogMovement ? inputUserDirection.magnitude : 1f;
        speed = targetSpeed;

        currentHorizontalSpeed = new Vector3(character.Movement.Controller.velocity.x, 0.0f, character.Movement.Controller.velocity.z).magnitude;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * speedChangeRate);

            speed = (float)Math.Round(speed, 3);
        }

        Vector3 cameraDirection = GetHorizontalCameraRotate().normalized;
        Vector3 relativeVectorMove = new Vector3(cameraDirection.x, 0, cameraDirection.z);
        Vector3 relativeMoveDirection = character.GetRelativeHorizontalMovement(inputUserDirection, relativeVectorMove).normalized;

        character.RotateTowardsVector(new Vector2(relativeMoveDirection.x, relativeMoveDirection.z));

        character.InputMoveDirection = new Vector3(relativeMoveDirection.x, 0, relativeMoveDirection.z);
        character.MoveSpeed = speed;
    }

    private Vector3 GetHorizontalCameraRotate()
    {
        return playerCamera == null ? Vector2.zero : playerCamera.transform.forward;
    }



    public void FireWrapper(AnimationEvent animationEvent) => Fire();
    public void Fire()
    {
        if (handItem.TryGetComponent(out IHandUsable obj))
        {
            obj.UseHandObject();
        }
    }

    public void FireAction()
    {
        if (handItem == null) return;

        //temp animation
        float currentRotation = Camera.main.transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(0, currentRotation + 30, 0);
        OnShootAction?.Invoke();
    }



    private void TakeNewItem(GameObject obj)
    {
        SelectItem(obj);
        obj.transform.parent = slots.Parant;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
    }

    private void SelectItem(GameObject obj)
    {
        if (obj.TryGetComponent(out IHandUsable handUsable))
        {
            handItem = obj;
            foreach (GameObject item in slots.Objects)
                item.SetActive(false);
            obj.SetActive(true);
        }
    }

    public void SelectNextItem()
    {
        if (slots.Objects.Count <= 0) return;
        int index = slots.Objects.IndexOf(handItem);
        int newIndex = index + 1 < slots.Objects.Count ? index + 1 : 0;
        SelectItem(slots.Objects[newIndex]);
    }

    public void SelectPreviousItem()
    {
        if (slots.Objects.Count <= 0) return; 
        int index = slots.Objects.IndexOf(handItem);
        int newIndex = index - 1 >= 0 ? index - 1 : slots.Objects.Count - 1;
        SelectItem(slots.Objects[newIndex]);
    }

    public void Damaged(float value)
    {
        health.DecreaseValue(value);
    }

    public void Died()
    {
        isDeath = true;
        OnDied?.Invoke();
        character.enabled = false;
        character.Movement.SetMoveValues(Vector3.one, 0);
        character.enabled = false;
    }
}
