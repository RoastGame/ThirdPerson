using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    bool isStafe = false;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        anim.SetBool("iS", isStafe);

        if(Input.GetKeyDown(KeyCode.F))
        {
            isStafe = !isStafe;
        }

        if(isStafe == true)
        {
            GetComponent<Controller>().HareketTipi = Controller.MovementType.Strafe;

        }

        if(isStafe == false)
        {
            GetComponent<Controller>().HareketTipi = Controller.MovementType.Directional;

        }

    }
}
