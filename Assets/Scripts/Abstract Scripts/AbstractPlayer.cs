using UnityEngine;

/// <summary>
/// The abstract layer of the player
/// </summary>
public abstract class AbstractPlayer : MonoBehaviour {

    protected Animator _anim;           // Players animator object
    protected Rigidbody _rb;            // Players rigidbody
    protected PlayerHealth _ph;         // Players health object
    protected PlayerMP _pm;             // Players mana object

    //private float _hp { set; get; }             // Players HP                         
    //private float _mp { set; get; }             // Players MP
    protected const float _baseMoveSpeed = 2f;  // Players base movespeed
    protected float _moveSpeed = 2f;            // Players current movespeed
    protected float _runSpeed = 4f;             // Players run speed
    protected float _turnSpeed = 4f;            // Players turn speed
    protected string _inputHorizontalAxis = "Horizontal";   // Players x axis
    protected string _inputVerticalAxis = "Vertical";       // Players z axis
    protected float _inputHorizontal;           // Input on x axis
    protected float _inputVertical;             // Input on z axis
    protected bool _run;                        // Is this player running?
    protected bool _jump;                       // Is this player jumping?

    /// <summary>
    /// Called by Unity (More less a constructor)
    /// </summary>
	public virtual void Start ()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _ph = GetComponent<PlayerHealth>();
        _pm = GetComponent<PlayerMP>();

        _ph.SetHealth(100);
        _ph.TakeDamage(0);
        _pm.SetMP(100);
        _pm.LoseMP(0);

        //_mp = _pm.GetMP();
        //_hp = _ph.GetHealth();
    }
	
    /// <summary>
    /// Called by Unity every frame
    /// </summary>
	public virtual void Update ()
    {
        _inputHorizontal = Input.GetAxis(_inputHorizontalAxis);
        _inputVertical = Input.GetAxis(_inputVerticalAxis);

        if (Input.GetKey(KeyCode.LeftShift)) _run = true;
        else _run = false;

        if (Input.GetKey(KeyCode.Space)) _jump = true;
        else _jump = false;

        SetAnimations();
    }

    /// <summary>
    /// Called by Unity for physics calculations
    /// </summary>
    public virtual void FixedUpdate()
    {
        // ===== MOVE =====
        Vector3 move = transform.forward * _inputVertical * _moveSpeed * Time.deltaTime;

        if (!_run)
        {
            _moveSpeed = _baseMoveSpeed;
            _anim.speed = _moveSpeed;
        }
        else _moveSpeed = _runSpeed;
        //_anim.speed = _moveSpeed / ((_moveSpeed + _runSpeed) / 2);

        _rb.MovePosition(_rb.position + move);

        // ===== ROTATION =====

        float turn = _inputHorizontal * 2f;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        _rb.MoveRotation(_rb.rotation * turnRotation);
    }

    /// <summary>
    /// Called to set the animation variables
    /// </summary>
    private void SetAnimations()
    {
        _anim.SetFloat("InputHorizontal", _inputHorizontal);
        _anim.SetFloat("InputVertical", _inputVertical);
        _anim.SetBool("Run", _run);
        _anim.SetBool("Jump", _jump);
    }
}
