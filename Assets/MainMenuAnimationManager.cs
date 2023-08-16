using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimationManager : MonoBehaviour
{

    public Animator planeAnim;
    float timer = -1f;

    void Update()
    {
        if (!planeAnim.GetCurrentAnimatorStateInfo(0).IsName("PlaneAnim"))
        {
            timer -= Time.deltaTime;
        }
        
        //Debug.Log(timer + " " + this.planeAnim.GetCurrentAnimatorStateInfo(0).IsName("PlaneAnim"));
        if(timer < 0)
        {
            timer = Random.Range(10f, 20f);
            planeAnim.Play("PlaneAnim");
        }
    }
}
