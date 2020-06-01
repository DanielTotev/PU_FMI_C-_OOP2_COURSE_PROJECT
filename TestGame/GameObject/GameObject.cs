using System.Windows.Forms;
using TestGame.Constatns;

namespace TestGame.GameObject
{
    public abstract class GameObject
    {

        public abstract class GameObjectBuilder
        {
            public PictureBox pictureBox = new PictureBox();
            

            public GameObjectBuilder Width(int width)
            {
                pictureBox.Width = width;
                return this;
            }

            public GameObjectBuilder Height(int height)
            {
                pictureBox.Height = height;
                return this;
            }

            public GameObjectBuilder X (int x)
            {
                pictureBox.Left = x;
                return this;
            }

            public GameObjectBuilder Y (int y)
            {
                pictureBox.Top = y;
                return this;
            }

            public GameObjectBuilder Image(string image)
            {
                pictureBox.BackgroundImage = System.Drawing.Image.FromFile(image);
                return this;
            }

            public abstract GameObject build();

        }


        public PictureBox PictureBox { get; set; }

        protected GameObject(GameObjectBuilder builder)
        {
            PictureBox = builder.pictureBox;
        }

        //public void changeImage(string image)
        //{
        //    if(PictureBox.Image != null)
        //    {
        //        PictureBox.Image.Dispose();
        //    }
        //    PictureBox.Image = Image.FromFile(image);
        //}

        public virtual void Move(Direction direction, int speed)
        {
            if (direction == Direction.LEFT)
            {
                PictureBox.Left -= speed;
            }
            else if (direction == Direction.RIGHT)
            {
                PictureBox.Left += speed;
            }
            else if (direction == Direction.TOP)
            {
                PictureBox.Top -= speed;
            } else
            {
                PictureBox.Top += speed;
            }
        }

        public bool isAtRightEnd()
        {
            return PictureBox.Left + PictureBox.Width >= GlobalConstants.SCREEN_WIDTH;
        }

        public bool isAtLeftEnd()
        {
            return PictureBox.Left <= 0;
        }

        public void Remove()
        {
            PictureBox.Visible = false;
        }

        public bool IntersectsWith(GameObject gameObject)
        {
            return PictureBox.Bounds.IntersectsWith(gameObject.PictureBox.Bounds);
        }
    }
}
