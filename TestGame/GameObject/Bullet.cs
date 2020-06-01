using static TestGame.GameObject.GameObject;

namespace TestGame.GameObject
{
    public class BulletBuilder : GameObjectBuilder
    {
        public override GameObject build()
        {
            return new Bullet(this);
        }
    }

    public class Bullet : GameObject
    {
        public Bullet(GameObjectBuilder builder) : base(builder)
        {
        }
    }
}
