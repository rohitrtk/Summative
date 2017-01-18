using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractPlayer
{
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown("1"))
        {
            _anim.Play("WAIT01", -1, 0f);
        }
        else if (Input.GetKey("2"))
        {
            _anim.Play("WAIT02", -1, 0f);
        }
        else if (Input.GetKey("3"))
        {
            _anim.Play("WAIT03", -1, 0f);
        }
        else if (Input.GetKey("4"))
        {
            _anim.Play("WAIT04", -1, 0f);
        }
        else if(Input.GetMouseButtonDown(0))
        {
            _anim.Play("DAMAGED00", -1, 0f);
        }
        else if(Input.GetMouseButtonDown(1))
        {
            _anim.Play("DAMAGED01", -1, 0f);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}