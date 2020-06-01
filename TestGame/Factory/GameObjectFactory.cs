using TestGame.Constatns;
using TestGame.GameObject;

namespace TestGame.Factory
{
    public sealed class  GameObjectFactory
    {
        private GameObjectFactory() { }


        public static GameObject.GameObject CreatePlayer() => 
            new Player.PlayerBuilder()
                .X(PlayerConstants.PLAYER_X)
                .Y(PlayerConstants.PLAYER_Y)
                .Width(PlayerConstants.PLAYER_WIDTH)
                .Height(PlayerConstants.PLAYER_HEIGHT)
                .Image(PlayerConstants.PLAYER_IMAGE)
                .build();


        public static GameObject.GameObject CreateZombie() =>
            new Zombie.ZombieBuilder()
            .X(ZombieConstants.ZOMBIE_X)
            .Y(ZombieConstants.ZOMBIE_Y)
            .Width(ZombieConstants.ZOMBIE_WIDTH)
            .Height(ZombieConstants.ZOMBIE_HEIGHT)
            .Image(ZombieConstants.COVID_IMAGE)
            .build();

        public static GameObject.GameObject CreatePlatform(int x, int y, int width, int height, string image)
            => new Platform.PlatformBuilder()
            .X(x)
            .Y(y)
            .Width(width)
            .Height(height)
            .Image(image)
            .build();


        public static GameObject.GameObject CreateBullet(int x, int y)
            => new BulletBuilder()
            .X(x)
            .Y(y)
            .Width(BulletConstants.BULLET_WIDTH)
            .Height(BulletConstants.BULLET_HEIGHT)
            .Image(BulletConstants.BULLET_IMAGE)
            .build();

    }
}
