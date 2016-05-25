using UnityEngine;

public interface IDamageable// a interface, every script that implements this interface is forced to have this method(TakeDamg) including it.
{
    void TakeTrueDamg(float damage);

    void TakeDamg(float damage, float pen,bool physical);

    void TakeDamgOverTime(float time, float tickTime, float damagePerTick, bool physical);

    void DebufAndBuf(float time, float ammount);

    void permanentBuf(float ammount);
}
