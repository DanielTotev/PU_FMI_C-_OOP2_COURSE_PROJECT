

namespace TestGame.GameObject
{
    public class Player : GameObject
    {
        public class PlayerBuilder : GameObjectBuilder
        {
            public override GameObject build()
            {
                return new Player(this);
            }
        }


        public Player(GameObjectBuilder builder) : base(builder)
        {
        }
    }
}
