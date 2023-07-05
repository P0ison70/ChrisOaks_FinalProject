using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Gives the player controls 
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    public Vector2 input;
    private Animator animator;
    public LayerMask interacterablesLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if(input != Vector2.zero)
            {

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPosition = transform.position;
                targetPosition.x += input.x;
                targetPosition.y += input.y;

                if(IsWalkable(targetPosition))
                    StartCoroutine(Move(targetPosition));
            }
        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.F))
            Interact();

    }
    /// <summary>
    /// What happens when the player hits F
    /// </summary>
    void Interact()
    {
        //Getting direction
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPosition = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPosition, 0.2f, interacterablesLayer);
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }

    }
    /// <summary>
    /// Handles the player movement
    /// </summary>
    /// <param name="targetPosition">where the player ends up</param>
    /// <returns></returns>
    IEnumerator Move(Vector3 targetPosition)
    {
        isMoving = true;

        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;

        isMoving = false; 

    }

    /// <summary>
    /// Handles collisions
    /// </summary>
    /// <param name="targetPosition">where the player ends up</param>
    /// <returns>false if there is something in the way, true if there is nothing</returns>
    //Currently only works with things you can interact with
    private bool IsWalkable(Vector3 targetPosition)
    {
        if(Physics2D.OverlapCircle(targetPosition,0.2f, interacterablesLayer) != null)
        {
            return false;
        }

        return true;
    }

}
