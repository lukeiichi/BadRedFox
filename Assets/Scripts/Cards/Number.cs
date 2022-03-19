using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
  public Text number;
  private int alive = 20;

  private void Start(){
    number.text = alive.ToString();
  }

  public void Kill(){
    alive--;
    number.text = alive.ToString();
  }
}
