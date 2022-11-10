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



    private void Start()
    {
        InitializeCard();
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
}
