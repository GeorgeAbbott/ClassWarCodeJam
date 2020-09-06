using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using static Class_War.Constants;

namespace Class_War
{

    enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }


    class Enemy : Sprite
    {
        bool OutOfBounds
        {
            get
            {
                // OutOfBounds is where
                /*
                 * -> The TopLeft X is beyond the screen's RIGHT edge, or ...
                 * -> The TopRight X is beyond the screen's LEFT edge, or ...
                 * -> The Bottom Y is beyond the screen's TOPMOST edge, or ...
                 * -> The Top Y is beyond the screen's BOTTOMMOST edge
                 */

                if (position.X > RightMostEdge+12) return true; //
                else if (Vector2.Add(position, sizeOfString).X < 0) return true;
                else if (Vector2.Add(position, sizeOfString).Y < 0) return true;
                else if (position.Y > BottomMostEdge+12) return true; //
                else return false;
            }
        }

        public List<Vector2> Borders
        {
            get
            {
                Vector2 bottomright = new Vector2();
                bottomright.X = (position.X + sizeOfString.X);
                bottomright.Y = (position.Y + sizeOfString.Y);
                return new List<Vector2>() { position, bottomright };
            }
        }

        ContentManager Content;
        int hp;
        string keyword;
        public Vector2 position;
        SpriteFont font;
        public int points;
        Color color;
        Vector2 sizeOfString;
        float speed;
        public Direction direction;
        bool isDestroyed = false;
        public bool IsDestroyed { get => isDestroyed; set => isDestroyed = value; }
        Random random = new Random();

        DateTime timeOfLastBullet = DateTime.Now;




        public Enemy
            (ContentManager Content, int hp, string keyword,
            string font, Color color, double speed, int level,
            bool isException, Vector2 position, Direction direction)
        {
            this.Content = Content;
            this.hp = hp * (isException ? 2 : 1);
            this.keyword = keyword;
            this.font = Content.Load<SpriteFont>(font);
            this.color = color;
            this.sizeOfString = this.font.MeasureString(keyword);
            this.points = Convert.ToInt32(Convert.ToDouble(level) * speed);
            this.speed = (float)speed;
            this.direction = direction;
            this.position = position;
        }

        public void Update(ref List<Bullet> bullets)
        // containingList is the list containing this object, to allow for it to destroy itself when out of bounds.
        {
            Move();
            if (OutOfBounds)
                IsDestroyed = true;

            // Create bullets
            if (DateTime.Now - timeOfLastBullet > TimeSpan.FromSeconds(
                (((15*random.NextDouble()) - (0.4*speed) + 1) > 0.1) ? ((15*random.NextDouble()) - (0.4 * speed) + 1) : 0.05))
            {
                timeOfLastBullet = DateTime.Now;
                bullets.Add(new Bullet(Content, position, 
                    ((direction == Direction.Left || direction == Direction.Right) ? Direction.Down : new List<Direction>() { Direction.Left, Direction.Right }[random.Next(0,1)])
                    , 5, false));
            }

        }

        public void Move()
        {
            if (direction == Direction.Left)
            {
                this.position.X -= speed;
            }
            if (direction == Direction.Right)
            {
                this.position.X += speed;
            }
            if (direction == Direction.Up)
            {
                this.position.Y -= speed;
            }
            if (direction == Direction.Down)
            {
                this.position.Y += speed;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(font, keyword, position, color);
        }

    }

    class Bullet
    {
        bool OutOfBounds
        {
            get
            {
                // OutOfBounds is where
                /*
                 * -> The TopLeft X is beyond the screen's RIGHT edge, or ...
                 * -> The TopRight X is beyond the screen's LEFT edge, or ...
                 * -> The Bottom Y is beyond the screen's TOPMOST edge, or ...
                 * -> The Top Y is beyond the screen's BOTTOMMOST edge
                 */

                if (position.X > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) return true;
                else if ((position).X < 0) return true;
                else if ((position).Y < 0) return true;
                else if (position.Y > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height) return true;
                else return false;
            }
        }



        public Vector2 position;
        public bool isPlayersBullet;
        Direction direction;
        int speed;

        Texture2D image;

        bool isDestroyed = false;
        public bool IsDestroyed { get => isDestroyed ; set => isDestroyed = value; }

        public void Move(bool autoUpdate = true)
        {
            if (direction == Direction.Left)
            {
                this.position.X -= speed;
            }
            if (direction == Direction.Right)
            {
                this.position.X += speed;
            }
            if (direction == Direction.Up)
            {
                this.position.Y -= speed;
            }
            if (direction == Direction.Down)
            {
                this.position.Y += speed;
            }

            if (autoUpdate)
                Update();
        }

        public void Update()
        {
            if (OutOfBounds)
                IsDestroyed = true;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, position, Color.White);
        }

        public Bullet(ContentManager Content, Vector2 position, Direction direction, int speed, bool isPlayersBullet)
        {
            this.position = position;
            this.direction = direction;
            this.speed = speed;
            this.isPlayersBullet = isPlayersBullet;
            this.image = Content.Load<Texture2D>("bullet");
        }
    } // End of Class Bracket
}
