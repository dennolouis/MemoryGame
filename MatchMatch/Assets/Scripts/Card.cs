using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField]
    Material back;

    [SerializeField]
    Material front;

    Animator anim;

    MeshRenderer rend;


    private void Start()
    {
        InitializeCard();
    }

    public void ApplyBackMaterial()
    {
        rend.sharedMaterial = back;
        anim.SetBool("Player_Clicked", false);
    }
    public void ApplyFrontMaterial()
    {
        rend.sharedMaterial = front;
        anim.SetBool("Player_Clicked", false);
    }
    
    
    private void OnMouseDown()
    {
        anim.SetBool("Player_Clicked", true);
    }

    void InitializeCard()
    {
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
}
