using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Class_War
{
    public static class Constants
    {
        public const string Consolas12 = "Consolas12";

        public const string DefaultFont = Consolas12;

        public const int Level1MaxEnemyCount = 10;
        public const int Level1EnemyHealth = 100;

        public const string MainCharacterSpriteImage = "maincharaimage";
        public static readonly Vector2 MainCharacterStartingPosition;

        public const int LifeLostScreenDuration = 3;
        public const int AfterLifeLostImmunityTime = LifeLostScreenDuration + 5;

        public const int RightMostEdge = 800;
        public const int BottomMostEdge = 500;


        static Constants ()
        {
            MainCharacterStartingPosition = new Vector2(330, 375); // TODO: add
        }

    }
}
