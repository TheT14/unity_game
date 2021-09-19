using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class text : MonoBehaviour
{
     public Text thing_to_change;
      public void text_Change( string tex)
     {
          thing_to_change.text = tex;
     }
   
}
