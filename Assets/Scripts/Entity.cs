using UnityEngine;

public abstract class AbstractPlayer : MonoBehaviour {

    protected Animator _anim;
    protected Rigidbody _rb;

    protected const float _baseMoveSpeed = 2f;
    protected float _moveSpeed = 2f;
    protected float _runSpeed = 4f;
    protected float _turnSpeed = 4f;
    protected string _inputHorizontalAxis = "Horizontal";
    protected string _inputVerticalAxis = "Vertical";
    protected float _inputHorizontal;
    protected float _inputVertical;
    protected bool _run;
    protected bool _jump;

	public virtual void Start ()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
	}
	
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

    public virtual void FixedUpdate()
    {
        Vector3 move = transform.forward * _inputVertical * _moveSpeed * Time.deltaTime;

        if (!_run)
        {
            _moveSpeed = _baseMoveSpeed;
            _anim.speed = _moveSpeed;
        }
        else
        {
            _moveSpeed = _runSpeed;
            _anim.speed = _moveSpeed / ((_moveSpeed + _runSpeed) / 2);
        }

        _rb.MovePosition(_rb.position + move);

        float turn = _inputHorizontal * 2f;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        _rb.MoveRotation(_rb.rotation * turnRotation);
    }

    private void SetAnimations()
    {
        _anim.SetFloat("InputHorizontal", _inputHorizontal);
        _anim.SetFloat("InputVertical", _inputVertical);
        _anim.SetBool("Run", _run);
        _anim.SetBool("Jump", _jump);
    }
}
