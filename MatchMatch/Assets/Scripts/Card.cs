using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    Material back;

    Material front;

    Animator anim;

    MeshRenderer rend;

    CardManager manager;


    enum CardState
    {
        ShowingBack,
        ShowingFront
    };

    CardState state;

    float shiftvalue = 1;
    private void Start()
    {
        InitializeCard();
        StartCoroutine(ShiftUp());
        
    }


    private void Update()
    {
        if(manager.FlipCardsback())
            FlipBack();
    }
    public void ApplyBackMaterial()
    {
        rend.sharedMaterial = back;
        anim.SetBool("ShowBack", false);
        state = CardState.ShowingBack;
    }
    public void ApplyFrontMaterial()
    {
        rend.sharedMaterial = front;
        anim.SetBool("ShowFront", false);
        state = CardState.ShowingFront;
    }
    
    
    private void OnMouseDown()
    {
        if(!manager.CanFlip()) return;
        switch(state)
        {
            case CardState.ShowingFront:
                anim.SetBool("ShowBack", true);
                break;
            case CardState.ShowingBack:
                anim.SetBool("ShowFront", true);
                break;
        }

        manager.SetCard(this);
    }

    public void FlipBack()
    {
        anim.SetBool("ShowBack", true);
    }

    void InitializeCard()
    {   
        manager = FindObjectOfType<CardManager>();

        state = CardState.ShowingBack;


        anim = GetComponent<Animator>();
        rend = GetComponent<MeshRenderer>();
        rend.enabled = true;
        
        ApplyBackMaterial();
    }

    IEnumerator ShiftUp()
    {
        Vector3 target = transform.position;
        target.y += shiftvalue;

        float speed = 1.5f;
        while(transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return 0;
        }
    }

    public void SetBackMaterial(Material mat)
    {
        back = mat;
    }

    public void SetFrontMaterial(Material mat)
    {
        front = mat;
    }

    public Material GetFront()
    {
        return front;
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        if(transform.position.y > -4.5 + shiftvalue) GetComponent<Rigidbody>().useGravity = true;
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit(Collider other)
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;

    }



}
