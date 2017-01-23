using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The abstract layer of the player
/// </summary>
public abstract class AbstractPlayer : MonoBehaviour {

    protected Animator _anim;                               // Players animator object
    protected Rigidbody _rb;                                // Players rigidbody
    public PlayerHealth _ph;                                // Players health object
    public PlayerMP _pm;                                    // Players mana object
    public PlayerPhase _pp;                                 // Players phase object
    public PlayerRoundNumber _prn;                          // Players round number object

    [SerializeField] protected float _baseMoveSpeed;        // Players base movespeed
    [SerializeField] protected float _moveSpeed;            // Players current movespeed
    [SerializeField] protected float _runSpeed;             // Players run speed
    [SerializeField] protected float _turnSpeed;            // Players turn speed
    protected string _inputHorizontalAxis = "Horizontal";   // Players x axis input
    protected string _inputVerticalAxis = "Vertical";       // Players z axis input
    protected float _inputHorizontal;                       // Input on x axis
    protected float _inputVertical;                         // Input on z axis
    protected bool _run;                                    // Is this player running?
    protected bool _jump;                                   // Is this player jumping?

    protected Level1Manager _gm;                            // Level1Manager object

    [SerializeField] AbstractTower _towerPrefab;            // Tower prefab
    protected List<AbstractTower> _towers;                  // List of this players towers

    /// <summary>
    /// Called by Unity on object creation
    /// </summary>
	public virtual void Start ()
    {
        // Get all components for this class to work
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _ph = GetComponent<PlayerHealth>();
        _pm = GetComponent<PlayerMP>();
        _pp = GetComponent<PlayerPhase>();
        _prn = GetComponent<PlayerRoundNumber>();
        _gm = GameObject.Find("Level1Manager").GetComponent<Level1Manager>();
        _towers = new List<AbstractTower>();
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
    /// Called by Unity every frame
    /// </summary>
	public virtual void Update ()
    {
        _inputHorizontal = Input.GetAxis(_inputHorizontalAxis);
        _inputVertical = Input.GetAxis(_inputVerticalAxis);

        KeyController();

        if(!(_pp.GetCurrentPhase() == _gm.GetPhase()))_pp.SetCurrentPhase(_gm.GetPhase());
        if(!(_prn.GetRoundNumber() == _gm.GetRound()))_prn.SetRoundNumber(_gm.GetRound());
    }

    /// <summary>
    /// Render method called by Unity
    /// </summary>
    public virtual void LateUpdate()
    {
        SetAnimations();
    }

    /// <summary>
    /// Called by Unity when a trigegr collision is detected
    /// </summary>
    /// <param name="collision"></param>
    public virtual void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Chest c = collision.gameObject.GetComponent<Chest>();
            _pm.SetMP(_pm.GetMP() + c.GetAmountOfMp());
            c.gameObject.SetActive(false);
        }
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

    /// <summary>
    /// Used to check input that isn't on the x or z axis
    /// </summary>
    private void KeyController()
    {
        if (Input.GetKey(KeyCode.LeftShift)) _run = true;
        else _run = false;

        if (Input.GetKey(KeyCode.Space)) _jump = true;
        else _jump = false;

        if (Input.GetKeyUp(KeyCode.Q))
        {
            _towers.Add(Instantiate(_towerPrefab, transform.position + transform.forward, transform.rotation));
        }
    }
}