using UnityEngine;

namespace VR_Prototype
{
  public class IcePotion : Potion
  {
    Enemy[] affectedEnemies;
    public override void StopPotionEffect()
    {
      for (int i = 0; i < affectedEnemies.Length; i++)
      {
        affectedEnemies[i].Resume();
      }
    }

    protected override void HandlePotionEffect(Collision collision)
    {
      gameObject.GetComponent<Rigidbody>().isKinematic = true;
      Vector3 center = transform.position;
      Collider[] collisions = Physics.OverlapSphere(center, this.radius, 1 << 3);
      affectedEnemies = new Enemy[collisions.Length];
      for (int i = 0; i < collisions.Length; i++)
      {
        Debug.Log(collisions[i].gameObject.name);
        affectedEnemies[i] = collisions[i].gameObject.GetComponent<Enemy>();
        affectedEnemies[i].Halt();
      }
    }

    void Start()
    {
      int maxDuration = 0;
      for (int i = 0; i < potionEffects.Length; i++)
      {
        if (maxDuration < potionEffects[i].main.duration)
        {
          maxDuration = (int)potionEffects[i].main.duration;
        }
      }

      this.effectDuration = (this.effectDuration > maxDuration) ? this.effectDuration : maxDuration;
    }
  }
}
