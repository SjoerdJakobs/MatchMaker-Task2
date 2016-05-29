using UnityEngine;

public interface IDamageable// a interface, every script that implements this interface is forced to have this method(TakeDamg) including it.
{
    void TakeTrueDamg(float damage, float scaling, bool physical);

    void TakeDamg(float damage, float pen, float scaling, bool physical);

    void TakeDamgOverTime(float time, float damagePerTick, float pen, float scaling, bool physical);

    void DebufAndBuf(float time, float ammount, int stat);

    void changeStat(float ammount, int stat);
}
