using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 人物只有一个不需要其他扩展
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody2D _rd_2d;
    private Animator _anim;
    private int _IsRun = Animator.StringToHash("IsRun");
    private int _IsShin = Animator.StringToHash("IsShin");
    private int _IsDie = Animator.StringToHash("IsDie");
    private int _Jump = Animator.StringToHash("Jump");
    private int _Sprint = Animator.StringToHash("Sprint");
    private int _VelocityY = Animator.StringToHash("VelocityY");

    [SerializeField] private float _speed_move = 1f;

    [SerializeField] private float _speed_jump = 1f;

    [SerializeField] private float _multiple_shin = 0.1f;

    private float _speed_move_nor = 1f;
    private float _speed_move_getheart = 1f;

    private float _speed_jump_nor = 1f;
    private float _speed_jump_getheart = 1f;

    #region Bool 属性Get、Set方法
    private bool IsMove { get; set; }

    private bool IsJump { get; set; }

    private bool IsShin { get; set; }

    private bool IsShining { get; set; }

    private bool IsSprint { get; set; }

    private bool IsDie { get; set; }

    private bool IsCanMove { get; set; }

    private bool IsCanJump { get; set; }

    private bool IsCanShin { get; set; }

    private bool IsCanSprint { get; set; }

    private bool IsCanInverse { get; set; }

    private bool IsInvincibleing { get; set; }

    private Vector3 WallColliderPos { get; set; }


    private Rigidbody2D Rd_2d
    {
        get
        {
            if (_rd_2d == null)
            {
                _rd_2d = GetComponent<Rigidbody2D>();
            }
            return _rd_2d;
        }
    }

    private Animator Anim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
            }
            return _anim;
        }
    }

    private bool IsLookRight
    {
        get
        {
            return InputBase.Instance.GetAxisRawX() > 0;
        }
    }
    #endregion

    private void Start()
    {
        Init();
    }


    private void Init()
    {
        IsCanMove = true;
        IsCanJump = true;
        IsCanShin = false;
        IsCanSprint = true;
        IsCanInverse = false;
        _speed_move_nor = _speed_move;
        _speed_move_getheart = _speed_move_nor * 0.5f;
        _speed_jump_nor = _speed_jump;
        _speed_jump_getheart = _speed_jump_nor * 0.5f;
        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    private void Update()
    {
        Anim.SetBool(_IsDie, IsDie);
        if (IsDie) return;
        IsMove = Mathf.Abs(InputBase.Instance.GetAxisRawX()) > 0.5f && IsCanMove;
        IsJump = InputBase.Instance.IsJumpDown() && IsCanJump;
        IsShin = InputBase.Instance.IsShinDown() && IsCanShin;
        IsSprint = InputBase.Instance.IsSprintDown() && IsCanSprint;
        Anim.SetBool(_IsRun, IsMove);
        Anim.SetBool(_IsShin, IsShining);
        Anim.SetFloat(_VelocityY, Rd_2d.velocity.y);
        if (IsMove)
        {
            Move();
        }
        if (IsJump)
        {
            Jump();
        }
        if (IsShin)
        {
            Shin();
        }
        if (IsSprint)
        {
            Sprint();
        }

        if (IsCanInverse && Vector3.Dot(transform.position - WallColliderPos, transform.right) >= 0/*( dirRight &&  IsLookRight||!dirRight && !IsLookRight)*/)
        {
            Rd_2d.velocity -= new Vector2(Rd_2d.velocity.x, 0);
        }


        IsShining = Rd_2d.gravityScale < 1;

        if (IsShining)
        {
            IsCanJump = true;
        }
    }

    #region 具体功能实现

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.collider.CompareTag(CustomTag.GROUND)) {
        //    IsCanJump = true;
        //    IsCanInverse = false;
        //    IsShining = false;
        //}

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(CustomTag.WALL))
        {
            WallColliderPos = collision.transform.position;
            IsCanShin = true;
            IsCanInverse = true;
        }
        else if (collision.collider.CompareTag(CustomTag.GROUND))
        {
            IsCanJump = true;
            IsCanInverse = false;
            IsShining = false;
        }
        else if (collision.collider.CompareTag(CustomTag.UNTAGGED))
        {
            WallColliderPos = collision.transform.position;
            IsCanInverse = true;
        }

    }


    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.collider.CompareTag(CustomTag.WALL))
        {
            IsCanShin = false;
            IsCanInverse = false;
            Rd_2d.gravityScale = 1;
        }
        else if (collision.collider.CompareTag(CustomTag.GROUND))
        {
            IsCanJump = false;
            IsCanInverse = false;
        }
        else if (collision.collider.CompareTag(CustomTag.UNTAGGED))
        {
            IsCanInverse = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(CustomTag.FALLTRIGGER))
        {
            Invoke("Resurgence_Fall", 0.8f);
        }
    }

    void Resurgence_Fall() {
        CustomPlayerPrefs.Instance.Health = CustomVariable.HEALTH_MIN;
        Invincible();
        transform.position = LevelController.Instance.GetPlayerRebirthPos_Fall();
    }


    /// <summary>
    /// 移动功能实现
    /// </summary>
    public void Move()
    {
        float axisX = InputBase.Instance.GetAxisRawX();
        int angleY = IsLookRight ? 180 : 0;
        transform.eulerAngles = new Vector3(0, angleY, 0);
        Vector2 vel = Rd_2d.velocity;
        Rd_2d.velocity = new Vector2(axisX * _speed_move, vel.y);
        IsMove = true;
    }

    /// <summary>
    /// 跳跃功能实现
    /// </summary>
    public void Jump()
    {
        Anim.SetTrigger(_Jump);
        Rd_2d.gravityScale = 1;
        Vector2 vel = Rd_2d.velocity;
        Rd_2d.velocity = new Vector2(vel.x, _speed_jump);
        IsJump = true;
        IsCanJump = false;
    }


    /// <summary>
    /// 攀爬功能实现
    /// </summary>
    public void Shin()
    {
        Rd_2d.gravityScale = _multiple_shin;
        if (Rd_2d.velocity.y > 0) Rd_2d.velocity = Vector2.zero;
        //InputBase.Instance.GetAxisRawY();
        IsShin = true;
    }

    /// <summary>
    /// 冲刺功能实现
    /// </summary>
    public void Sprint()
    {
        IsSprint = true;
    }

    public void Die()
    {
        if (IsDie || IsInvincibleing) return;
        IsDie = true;
        Invoke("Resurgence_Die", 0.8f);
    }

    public void Resurgence_Die()
    {
        if (!IsDie || LevelController.Instance.IsChangeLeveling) return;
        IsDie = false;
        CustomPlayerPrefs.Instance.Health = CustomVariable.HEALTH_MIN;
        Invincible();
        transform.position = LevelController.Instance.GetPlayerRebirthPos_Die();

    }
    #endregion

    public void Invincible(float time = 2f)
    {
        StartCoroutine(Invincible_IE(time));
    }

    public void RefreshHeart()
    {
        var health = CustomPlayerPrefs.Instance.Health;
        _speed_jump = health > CustomVariable.HEALTH_MIN ? _speed_jump_getheart : _speed_jump_nor;
        _speed_move = health > CustomVariable.HEALTH_MID ? _speed_move_getheart : _speed_move_nor;
        //if (health == CustomVariable.HEALTH_MIN) {
        //    _speed_jump = _speed_jump_nor;
        //    _speed_move = _speed_move_nor; ;
        //} else if (health == CustomVariable.HEALTH_MID) {
        //    _speed_jump = _speed_jump_getheart;
        //    _speed_move = _speed_move_nor; ;
        //} else if (health == CustomVariable.HEALTH_MAX) {
        //    _speed_jump = _speed_jump_getheart;
        //    _speed_move = _speed_move_getheart;
        //}

    }

    private IEnumerator Invincible_IE(float time = 2f)
    {
        RefreshHeart();
        IsInvincibleing = true;
        tag = CustomTag.UNTAGGED;

        GetComponent<DG.Tweening.DOTweenAnimation>().DOPlay();


        yield return new WaitForSeconds(time);

        GetComponent<DG.Tweening.DOTweenAnimation>().DOPause();

        GetComponent<SpriteRenderer>().color = Color.white;

        tag = CustomTag.PLAYER;
        IsInvincibleing = false;
    }
}
