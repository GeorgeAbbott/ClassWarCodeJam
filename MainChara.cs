using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Class_War
{
    class MainChara : Sprite
    {
        Texture2D image;
        public Vector2 topleft;
        ContentManager Content;
        Vector2 size = new Vector2(50, 50);

        public List<Vector2> Borders
        {
            get
            {
                Vector2 bottomright = new Vector2();
                bottomright.X = (topleft.X + size.X);
                bottomright.Y = (topleft.Y + size.Y);
                return new List<Vector2>() { topleft, bottomright };
            }
        }

        public void GoUp(int speed = 5)
        {
            topleft.Y -= speed;
        }
        public void GoDown(int speed = 5)
        {
            topleft.Y += speed;
        }
        public void GoLeft(int speed = 5)
        {
            topleft.X -= speed;
        }
        public void GoRight(int speed = 5)
        {
            topleft.X += speed;
        }
        public void Fire(ref List<Bullet> bullets, int speed = 5)
        {
            bullets.Add(new Bullet(Content, topleft, Direction.Up, speed, true));
        }



        public MainChara (ContentManager Content, string spriteImage, Vector2 position)
        {
            this.Content = Content;
            image = Content.Load<Texture2D>(spriteImage);
            this.topleft = position;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, topleft, Color.White);
        }
    }
}
