using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTurnOnGlow : MonoBehaviour
{
   public Material material;
   public SpriteRenderer SpriteRenderer;

   public void ChangeToGlowMaterial()
   {
       SpriteRenderer.material = material;
   }
   
}
