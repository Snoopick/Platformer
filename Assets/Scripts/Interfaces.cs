public interface IPlayer
{
    void RegisterPlayer();
}

public interface IEnemy
{
    void RegisterEnemy();
}

public interface IDamager
{
    int Damage { get; }
    void SetDamage();
}

public interface IHitBox
{
    int Heals { get; }
    void Hit(int damage);
    void Die();
}