using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodoList : MonoBehaviour
{
    bool isScrollOpened = false;
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scrollStateChange()
    {
        isScrollOpened = !isScrollOpened;
        animator.SetBool("IsOpened", isScrollOpened);
    }
}
