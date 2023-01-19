using UnityEngine;

namespace VR_Prototype
{
  public class LightningPotion : Potion
  {
    public int lightningDamage = 1;
    public override void StopPotionEffect() { }

    protected override void HandlePotionEffect(Collision collision)
    {
      Enemy[] aliveEnemies = EnemyPool.instance.GetLivingEnemies();

      if (aliveEnemies.Length > 0)
      {
        int randomIndex = Random.Range(0, aliveEnemies.Length);

        Vector3 center = aliveEnemies[randomIndex].transform.position;

        for(int i = 0; i < potionEffects.Length; i++)
        {
          potionEffects[i].transform.position = center;
        }
        PlayEffects();

        aliveEnemies[randomIndex].Die();
        Collider[] collisions = Physics.OverlapSphere(center, radius, 1 << 3);

        for (int i = 0; i < collisions.Length; i++)
        {
          Debug.Log(collisions[i].gameObject.name);
          collisions[i].gameObject.GetComponent<Enemy>().TakeDamage(lightningDamage);
        }
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