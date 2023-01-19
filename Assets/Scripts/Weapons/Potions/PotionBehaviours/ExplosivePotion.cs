using UnityEngine;

namespace VR_Prototype
{
  public class ExplosivePotion : Potion
  {
    public override void StopPotionEffect() { }

    protected override void HandlePotionEffect(Collision collision)
    {      
      PlayEffects();
      Vector3 center = transform.position;
      Collider[] collisions = Physics.OverlapSphere(center, this.radius, 1 << 3);
      for (int i = 0; i < collisions.Length; i++)
      {
        Debug.Log(collisions[i].gameObject.name);
        collisions[i].gameObject.GetComponent<Enemy>().Die();
      }
    }

    private void Start()
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