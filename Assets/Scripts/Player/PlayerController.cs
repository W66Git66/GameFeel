using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ScriptableStats _stats;
    //private
    //

    private Rigidbody2D _rb;
    private BoxCollider2D _col;
    private FrameInput _frameInput;//存储输入的结构体
    private Vector2 _frameVelocity;
    private bool _cachedQueryStartInColliders;//射线检测碰撞

    #region Interface

    public Vector2 FrameInput => _frameInput.Move;
    public event Action<bool, float> GroundedChanged;
    public event Action Jumped;

    #endregion

    private float _time;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();

        _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        GatherInput();
    }

    private void GatherInput()
    {
        //获取输入
        _frameInput = new FrameInput
        {
            JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
            JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
        };

        //死区限制，若输入小于死区，则不移动，否则全速移动
        if (_stats.SnapInput)
        {
            _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
            _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
        }

        //如果按下跳跃键，处理跳跃逻辑
        if (_frameInput.JumpDown)
        {
            _jumpToConsume = true;
            _timeJumpWasPressed = _time;
        }

    }



    #region Jumping

    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private float _timeJumpWasPressed;

    private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
    private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

    private void HandleJump()
    {
        if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

        if (!_jumpToConsume && !HasBufferedJump) return;

        if (_grounded || CanUseCoyote) ExecuteJump();

        _jumpToConsume = false;
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;
        _timeJumpWasPressed = 0;
        _bufferedJumpUsable = false;
        _coyoteUsable = false;
        _frameVelocity.y = _stats.JumpPower;
        Jumped?.Invoke();
    }

    #endregion
    //记录短按跳跃键，长按跳跃键和移动事件
    struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }

    //接口  猜测是为了处理事件
    public interface IPlayerController
    {
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
        public Vector2 FrameInput { get; }
    }
}
