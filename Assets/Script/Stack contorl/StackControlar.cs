using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackControlar : MonoBehaviour
{
   [SerializeField] StackBabyControlar[] stackBabyControlars;

   public void ShatterAllParts()
   {
    if(transform.parent != null)
    {
        transform.parent= null;
        FindObjectOfType<Player>().ADDPoint();
    }
        // transform.SetParent(null);

    foreach (StackBabyControlar item in stackBabyControlars)
    {
        item.stack();
    }
    StartCoroutine(distroyobject());
   }

   IEnumerator distroyobject()
   {
    yield return new WaitForSeconds(.5f);
    Destroy(gameObject);
   }



}
