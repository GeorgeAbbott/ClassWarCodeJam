using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Class_War
{
    class Menu
    {
        ContentManager Content;
        SpriteFont font;
        bool indented = false; // on the zeroth indentation level
        int currentSelection = 0;
        int indentedCurrSel = 0;
        int maxSelection = 2;
        public int outValue = 328;

        public void HandleInput (InputHandler ih)
        {
            if (!indented)
            {
                if (ih.OnKeyRelease(Keys.W) || ih.OnKeyRelease(Keys.Up))
                {
                    currentSelection--;
                    if (currentSelection < 0) currentSelection = maxSelection;
                }
                else if (ih.OnKeyRelease(Keys.S) || ih.OnKeyRelease(Keys.Down))
                {
                    currentSelection++;
                    if (currentSelection > maxSelection) currentSelection = 0;
                }
                else if (ih.OnKeyRelease(Keys.Enter))
                {
                    if (currentSelection == 2) outValue = 0;
                    else
                    {
                        indented = true;
                    }
                }
            }
            else
            {
                if (ih.OnKeyRelease(Keys.W) || ih.OnKeyRelease(Keys.Up))
                {
                    indentedCurrSel--;
                    if (indentedCurrSel < 0) indentedCurrSel = maxSelection;
                }
                else if (ih.OnKeyRelease(Keys.S) || ih.OnKeyRelease(Keys.Down))
                {
                    indentedCurrSel++;
                    if (indentedCurrSel > maxSelection) indentedCurrSel = 0;
                }
                else if (ih.OnKeyRelease(Keys.Back))
                {
                    indentedCurrSel = 0;
                    indented = false;
                }
                else if (ih.OnKeyRelease(Keys.Enter))
                {
                    // Select options, by setting outValue to appropriate setting.
                    switch (currentSelection)
                    {
                        case 0 when (indentedCurrSel == 0): outValue =  3; break; // C#
                        case 0 when (indentedCurrSel == 1): outValue =  4; break; // Assembly
                        case 0 when (indentedCurrSel == 2): outValue =  5; break; // C++
                        case 1 when (indentedCurrSel == 0): outValue =  6; break; // Black
                        case 1 when (indentedCurrSel == 1): outValue =  7; break; // Code
                        case 1 when (indentedCurrSel == 2): outValue =  8; break; // Shill
                    }

                    indentedCurrSel = 0;
                    indented = false;
                }
            }
        }

        public Menu (ContentManager Content, string font)
        {
            this.Content = Content;
            this.font = Content.Load<SpriteFont>(font);

        }

       public void Draw(SpriteBatch sb, bool isShill)
        {
            sb.DrawString(font, "Language", new Vector2(100, 100), Color.White);
            sb.DrawString(font, "Background", new Vector2(100, 120), Color.White);
            sb.DrawString(font, "Exit Menu", new Vector2(100, 140), Color.DarkRed);

            if (indented && currentSelection == 0)
            {
                sb.DrawString(font, "C#", new Vector2(230, 100), Color.White);
                sb.DrawString(font, "Assembly", new Vector2(230, 120), Color.White);
                sb.DrawString(font, "C++", new Vector2(230, 140), Color.White);
            }
            else if (indented && currentSelection == 1)
            {
                sb.DrawString(font, "Black", new Vector2(230, 120), Color.White);
                sb.DrawString(font, "Code", new Vector2(230, 140), Color.White);
                sb.DrawString(font, "Sellout", new Vector2(230, 160), Color.White);
            }

            if (isShill) sb.DrawString(font, "Go subscribe to javidx9!!!", new Vector2(300, 300), Color.Green);

            Vector2 position;
            switch (currentSelection)
            {
                case 0 when (!indented): position = new Vector2(90, 100); break;
                case 1 when (!indented): position = new Vector2(90, 120); break;
                case 2 when (!indented): position = new Vector2(90, 140); break;


                // Render Positions for when in LANGUAGE
                case 0 when (indented && indentedCurrSel == 0): position = new Vector2(220, 100); break;
                case 0 when (indented && indentedCurrSel == 1): position = new Vector2(220, 120); break;
                case 0 when (indented && indentedCurrSel == 2): position = new Vector2(220, 140); break;

                // Render Positions for when in BACKGROUND
                case 1 when (indented && indentedCurrSel == 0): position = new Vector2(220, 120); break;
                case 1 when (indented && indentedCurrSel == 1): position = new Vector2(220, 140); break;
                case 1 when (indented && indentedCurrSel == 2): position = new Vector2(220, 160); break;

                // Default: should never happen.
                default: position = new Vector2(0, 0); break; // This should never happen.
            };

            sb.DrawString(font, ">", position, Color.White);
        }
    }
}
