using UnityEngine;

public interface ISkill
{
    void shoot(Vector3 target ,float pen, float cooldown, float mana, float apOrAd);
}
