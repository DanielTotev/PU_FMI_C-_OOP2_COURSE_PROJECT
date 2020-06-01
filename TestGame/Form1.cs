using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TestGame.Constatns;
using TestGame.Factory;
using TestGame.GameObject;

namespace TestGame
{
    public partial class Form1 : Form
    {
        private GameObject.GameObject player;

        private GameObject.GameObject zombie;

        private GameObject.GameObject firstPlatform;

        private GameObject.GameObject secondPlatform;

        private IList<GameObject.GameObject> bullets = new List<GameObject.GameObject> ();

        private Direction firstPlatformDirection = Direction.RIGHT;

        private Direction secondPlatformDirection = Direction.LEFT;

        private Direction zombieDirection = Direction.RIGHT;

        private bool playerJumped;

        public Form1()
        {
            InitializeComponent();
            LoadGameObjects();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void GameLoop(object sender, EventArgs e)
        {
            MovePlatfroms();
            Gravitation();
            UpdatePlayerPosition();
            UpdateZombiePosition();
            MoveZombie();
            CheckForWinner();
            UpdateBulletsPositions();
        }

        private void UpdateBulletsPositions()
        {
            foreach(var bullet in bullets)
            {
                bullet.Move(Direction.RIGHT, BulletConstants.BULLET_SPEED);
            }
        }

        private void LoadGameObjects()
        {
            player = GameObjectFactory.CreatePlayer();
            zombie = GameObjectFactory.CreateZombie();
            firstPlatform = GameObjectFactory.CreatePlatform(PlatformConstants.FIRST_PLATFORM_X,
                PlatformConstants.FIRST_PLATFORM_Y,
                PlatformConstants.PLATFORM_WIDTH,
                PlatformConstants.PLATFORM_HEIGHT,
                PlatformConstants.PLATFORM_IMAGE);
            secondPlatform = GameObjectFactory.CreatePlatform(PlatformConstants.SECOND_PLATFORM_X,
                PlatformConstants.SECOND_PLATFORM_Y,
                PlatformConstants.PLATFORM_WIDTH,
                PlatformConstants.PLATFORM_HEIGHT,
                PlatformConstants.PLATFORM_IMAGE);

            Render(firstPlatform);
            Render(secondPlatform);
            Render(player);
            Render(zombie);
        }

        private void CheckForWinner()
        {
            if(player.IntersectsWith(zombie))
            {
                player.Remove();
                PrintWinner("Zombie won!");
            } else
            {
                foreach (var bullet in bullets)
                {
                    if(bullet.IntersectsWith(zombie))
                    {
                        zombie.Remove();
                        PrintWinner("Player won!");
                        break;
                    }
                }
            }
        }

        private void PrintWinner(string winnerText)
        {
            winnerLabel.Text = winnerText;
            timer1.Stop();
        }

        private void MovePlatfroms()
        {
            UpdatePlatformsDirections();
            firstPlatform.Move(firstPlatformDirection, 1);
            secondPlatform.Move(secondPlatformDirection, 1);
        }

        private void MoveZombie()
        {
            if (zombie.PictureBox.Left + ZombieConstants.ZOMBIE_WIDTH >= secondPlatform.PictureBox.Left + PlatformConstants.PLATFORM_WIDTH)
            {
                zombieDirection = Direction.LEFT;
            }
            else if (zombie.PictureBox.Left - ZombieConstants.ZOMBIE_SPEED < secondPlatform.PictureBox.Left)
            {
                zombieDirection = Direction.RIGHT;
            }

            zombie.Move(zombieDirection, ZombieConstants.ZOMBIE_SPEED);
        }

        private void UpdatePlayerPosition()
        {
            if(player.IntersectsWith(secondPlatform))
            {
                player.Move(secondPlatformDirection, 1);
            } else
            {
                player.Move(firstPlatformDirection, 1);
            }
        }

        private void UpdateZombiePosition()
        {
            zombie.Move(secondPlatformDirection, 1);
        }

        private void Gravitation()
        {
            if(playerJumped && !player.IntersectsWith(secondPlatform))
            {
                player.Move(Direction.BOTTOM, PlayerConstants.JUMP_SPEED);
                playerJumped = false;
            }else if(playerJumped && player.PictureBox.Bounds.IntersectsWith(secondPlatform.PictureBox.Bounds))
            {
                player.PictureBox.Left = secondPlatform.PictureBox.Left + 1;
                playerJumped = false;
            }
        }

        private void UpdatePlatformsDirections()
        {
            if(firstPlatform.isAtRightEnd())
            {
                firstPlatformDirection = Direction.LEFT;
            } else if(firstPlatform.isAtLeftEnd())
            {
                firstPlatformDirection = Direction.RIGHT;
            }

            if(secondPlatform.isAtLeftEnd())
            {
                secondPlatformDirection = Direction.RIGHT;
            } else if(secondPlatform.isAtRightEnd())
            {
                secondPlatformDirection = Direction.LEFT;
            }
        }

        private void HandlePlayerInput(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left && !PlayerExits(Direction.LEFT))
            {
                player.Move(Direction.LEFT, PlayerConstants.PLAYER_SPEED);
            } else if (e.KeyCode == Keys.Right && !PlayerExits(Direction.RIGHT))
            {
                player.Move(Direction.RIGHT, PlayerConstants.PLAYER_SPEED);
            } else if(e.KeyCode == Keys.Up)
            {
                player.Move(Direction.TOP, PlayerConstants.JUMP_SPEED);
                playerJumped = true;
            } else if(e.KeyCode == Keys.Space)
            {
                var bullet = GameObjectFactory.CreateBullet(player.PictureBox.Left + 1, player.PictureBox.Top + PlayerConstants.PLAYER_X);
                Render(bullet);
                bullets.Add(bullet);
            }
        }

        private void Render(GameObject.GameObject gameObject)
        {
            Controls.Add(gameObject.PictureBox);
        }

        private bool PlayerExits(Direction direction)
        {
            var playerX = direction == Direction.LEFT ? player.PictureBox.Left - PlayerConstants.PLAYER_SPEED :
                player.PictureBox.Left + PlayerConstants.PLAYER_SPEED;
            var platform = player.IntersectsWith(secondPlatform) ? secondPlatform : firstPlatform;
            return playerX < platform.PictureBox.Left || playerX > platform.PictureBox.Left + PlatformConstants.PLATFORM_WIDTH - PlayerConstants.PLAYER_WIDTH;
        }
    }
}
