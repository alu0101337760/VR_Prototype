using UnityEngine;

namespace VR_Prototype
{
  public class SlowPotion : Potion
  {
    public override void StopPotionEffect() { }

    protected override void HandlePotionEffect(Collision collision)
    {
      this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
      SphereCollider triggerZone = this.gameObject.AddComponent<SphereCollider>();
      triggerZone.isTrigger = true;
      triggerZone.radius = this.radius;
    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.layer == (1 << 3))
      {
        other.GetComponent<Enemy>().SetSpeed(0.7f);
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.gameObject.layer == (1 << 3))
      {
        other.GetComponent<Enemy>().Resume();
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