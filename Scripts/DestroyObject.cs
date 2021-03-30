using UnityEngine;

//Add this script to any game object in your starting scene that you want to destroy on loading
public class DestroyObject : MonoBehaviour
{

  private void Awake()
  {
      Destroy(gameObject);
  }

}
