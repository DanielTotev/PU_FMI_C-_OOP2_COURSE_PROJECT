namespace TestGame.GameObject
{
    public class Zombie : GameObject
    {
        public class ZombieBuilder : GameObjectBuilder
        {
            public override GameObject build()
            {
                return new Zombie(this);
            }
        }

        public Zombie(GameObjectBuilder builder) : base(builder)
        {
        }


    }
}
