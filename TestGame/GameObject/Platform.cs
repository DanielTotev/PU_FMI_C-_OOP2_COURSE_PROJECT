using TestGame.Constatns;

namespace TestGame.GameObject
{
    public class Platform : GameObject
    {
        public class PlatformBuilder : GameObjectBuilder
        {
            public override GameObject build()
            {
                return new Platform(this);
            }
        }


        public Platform(GameObjectBuilder builder) : base(builder)
        {
        }

        public override void Move(Direction direction, int speed)
        {
            PictureBox.Left += direction == Direction.RIGHT ? speed : -1 * speed;
        }
    }
}
