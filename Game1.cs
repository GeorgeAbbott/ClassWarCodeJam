using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Class_War.Constants;
using System.Diagnostics;
using System.Collections.Generic;
using System;
 

namespace Class_War
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont Consolas12SF;

        Random random = new Random();

        InputHandler inputHandler;

        int score;
        byte level;
        int enemiesSpawnedForLevel;
        int maxEnemiesForCurrLevel;
        bool isPaused;
        DateTime startTime;
        bool newLevel;
        bool beginNewLevel = false;
        int lives = 3;
        bool lifeLost; // TODO: initialize these
        bool inMenu = false;
        bool isShill = false;
        bool usesBackgroundImage = false;

        Texture2D codeBackgroundImage;
        Texture2D shillBackgroundImage;


        DateTime lifeLostTime;

        Timer collisionTimer = new Timer();

        Menu menu;

        MainChara mainchara;
        List<Enemy> enemies;
        List<Bullet> bullets;


        List<string> csharpKeywords;
        List<string> assemblyKeywords;
        List<string> cppKeywords;
        List<string> keywords;
        List<string> exceptions;

        public bool IsCollision(List<Vector2> borders, Vector2 point)
        {
            // If point within the borders, return true.
            if (point.X >= borders[0].X && point.X <= borders[1].X && point.Y >= borders[0].Y && point.Y <= borders[1].Y) return true;
            else return false;

        } // TODO: add


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            isPaused = false;
            mainchara = new MainChara(Content, MainCharacterSpriteImage, MainCharacterStartingPosition);

            enemies = new List<Enemy>();
            bullets = new List<Bullet>();

            menu = new Menu(Content, Consolas12);

            // Level and Enemy loading logic
            newLevel = true;
            startTime = DateTime.Now;
            level = 1;
            score = 0;
            maxEnemiesForCurrLevel = Level1MaxEnemyCount;

            inputHandler = new InputHandler();

            shillBackgroundImage = Content.Load<Texture2D>("shillbg");
            codeBackgroundImage = Content.Load<Texture2D>("codebg");

            lifeLost = false;
            lifeLostTime = DateTime.Now.Subtract(new TimeSpan(0, 0, AfterLifeLostImmunityTime));

            // TODO: actually add keyword loading mechanism
            csharpKeywords = new List<string>()
            { "abstract", "base", "bool", "break", "byte", "case", "catch",
            "char", "checked", "class", "const", "continue", "decimal", "default",
            "delegate", "do", "double", "else", "enum", "event", "explicit",
            "extern", "false", "finally", "fixed", "float", "for", "foreach",
            "goto", "if", "implicit", "in", "int", "interface", "internal",
            "is", "lock", "long", "namespace", "new", "null", "object", "operator",
            "out", "override", "params", "private", "protected", "public",
            "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof",
            "stackalloc", "static", "string", "struct", "switch", "this",
            "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort",
            "using", "virtual", "void", "volatile", "while"};
            assemblyKeywords = new List<string>()
            {
                "aaa", "aad", "aam", "aas", "adc", "add", "and", "call", "cbw",
                "clc", "cld", "cli", "cmc", "cmp", "cmpsb", "cmpsw", "cwd", "daa",
                "das", "dec", "div", "esc", "hlt", "idiv", "imul", "in", "inc",
                "int", "into", "iret", "ja", "jae", "jb", "jbe", "jc", "je", "jg",
                "jge", "jl", "jle", "jna", "jnae", "jnb", "jnbe", "jnc", "jne", "jng", 
                "jnge", "jnl", "jnle", "jno", "jnp", "jns", "jnz", "jo", "jp", "jpe",
                "jpo", "js", "jz", "jcxz", "jmp", "lahf", "lds", "lea", "les", "lock",
                "lodsb", "lodsw", "loop", "mov", "movsb", "movsw", "mul", "neg",
                "nop", "not", "or", "out", "pop", "popf", "push", "pushf", "rcl",
                "rcr", "rep", "repe", "repne", "repnz", "repz", "retn", "retf", 
                "ret", "rol", "ror", "sahf", "sal", "sar", "sbb", "scasb", "scasw",
                "shl", "shr", "stc", "std", "sti", "stosb", "stosw", "sub",
                "test", "wait", "xchg", "xlat", "xor"
            };
            cppKeywords = new List<string>() { "alignas", "alignof", "and",
            "and_eq", "asm", "atomic_cancel", "atomic_commit", "atomic_noexcept",
            "auto", "bitand", "bitor", "bool", "break", "case", "catch", "char",
            "char8_t", "char16_t", "char32_t", "class", "compl", "concept",
            "const", "consteval", "constexpr", "constinit", "const_cast",
            "continue", "co_await", "co_return", "co_yield", "decltype",
            "default", "delete", "do", "double", "dynamic_cast", "else",
            "enum", "explicit","export", "extern", "false", "float",
            "for", "friend", "goto", "if", "inline", "int", "long",
            "mutable", "namespace", "new", "noexcept", "not", "not_eq",
            "nullptr", "operator", "or", "or_eq", "private", "protected",
            "register", "reinterpret_cast", "requires", "return", "short",
            "signed", "sizeof", "static", "static_assert", "static_cast",
            "struct", "switch", "this", "template", "thread_local", "throw",
            "true", "try", "typedef", "typeid", "typename", "union", "unsigned",
            "using", "virtual", "void", "volatile", "wchar_t", "while",
            "xor", "xor_eq", "final", "override", "ifdef", "elif", "endif", "pragma",
            "undef", "define", "error", "line", "include", "export", "import",
            "module"};

                keywords = csharpKeywords;

            

            exceptions = new List<string>();
            exceptions.Add("NullReferenceException");


            // Add Level One enemies



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Consolas12SF = Content.Load<SpriteFont>(DefaultFont);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (newLevel)
            { // If newLevel begin the timer process, to regularly spawn the new enemies.
                if ((DateTime.Now - startTime) > TimeSpan.FromSeconds(30d / maxEnemiesForCurrLevel))// SystemOverFlowexception here.
                {
                    // Adequate time has passed, so spawn in a new enemy
                    string text = keywords[random.Next(keywords.Count)];
                    bool isException = false;

                    // Generate position and is left to right (or up and down)
                    Vector2 position = new Vector2(2,2); // Just to stop the error basically, position is overridden later
                    Direction direction;

                    // Testing for Random number gen
                    /*Debug.WriteLine(new string('\n', 20));
                    for (int amountoftimestodebugforwiththisthing = 0; amountoftimestodebugforwiththisthing < 150; amountoftimestodebugforwiththisthing++)
                    {
                        Debug.Write($"{(Direction)random.Next(0, 4)} - ");
                    }
                    Debug.WriteLine(new string('\n', 10));*/

                    if (level <= 5)
                    { // Can only declare left to right positions.
                        direction = (Direction)random.Next(0, 2); // Only generating 1
                    }
                    else
                        direction = (Direction)random.Next(0, 4); // Only generating 3

                    Debug.WriteLine($"\t\t\t\tCreated new Direction -> {direction.ToString()}");

                    if (direction == Direction.Left)
                    {
                            position = new Vector2(RightMostEdge+10f,
                                random.Next(0, BottomMostEdge-75));
                    }
                    else if (direction == Direction.Right)
                    {
                        position = new Vector2(-10f, random.Next(0, BottomMostEdge-75));
                    }
                    else if (direction == Direction.Down)
                    {
                        position = new Vector2(random.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width), -10f);
                    }
                    else if (direction == Direction.Up)
                    {
                        position = new Vector2(random.Next(0, RightMostEdge-5),
                            BottomMostEdge + 10f);
                    }
                    



                    if (exceptions.Contains(text)) isException = true;
                    enemies.Add(new Enemy(Content, Level1EnemyHealth * level, text,
                        Consolas12,
                        Color.White,
                        random.NextDouble() + 0.3 + (level*0.15),
                        level, isException, position, direction));

                    enemiesSpawnedForLevel++;
                    if (enemiesSpawnedForLevel == maxEnemiesForCurrLevel) newLevel = false; // Once everything is spawned, no longer new level
                }

            }
            // Begin the New Level once all Enemies down
            if (!newLevel && (enemies.Count == 0)) // If all for this level spawned and disappeared out of list
            {
                beginNewLevel = true;
            }


            // Updating spawning logic for the next level
            if (enemiesSpawnedForLevel >= maxEnemiesForCurrLevel && beginNewLevel)
            { // Time for each spawning wave will be 30 seconds
                level++;
                newLevel = true;
                beginNewLevel = false; // the level is begun-- this is set to true again once all enemies are gone
                startTime = DateTime.Now;
                enemiesSpawnedForLevel = 0;
                maxEnemiesForCurrLevel = ((Level1MaxEnemyCount) + maxEnemiesForCurrLevel);
            }
            
            

            // Handle Input
            inputHandler.ReadInput();
            Debug.WriteLine("Jello"); // This is working every loop

            /*if (inputHandler.IsKeyDown(Keys.P, 0.5))
            { // Toggle Pausing with TAB
                if (isPaused) isPaused = false;
                else isPaused = true;
            }*/

            Debug.WriteLine("---- ABOUT TO CHECK FOR IF TAB PRESSED ----");
            bool menuCodeExecuted = false;
            if (isPaused && inMenu)
            {
                menu.HandleInput(inputHandler);

                // TODO: handle states
                // ....
                if (menu.outValue == 0) {
                    isPaused = inMenu = false;
                    menu.outValue = 328;
                }
                else if (menu.outValue == 3) // C# Language
                    { keywords = csharpKeywords; }
                else if (menu.outValue == 4) // Assembly
                    { keywords = assemblyKeywords; }
                else if (menu.outValue == 5)
                    { keywords = cppKeywords; }
                else if (menu.outValue == 6) // BLACK
                    { isShill = usesBackgroundImage = false; }
                else if (menu.outValue == 7) // CODE
                    { usesBackgroundImage = true; isShill = false; }
                else if (menu.outValue == 8) // SHILL
                    { usesBackgroundImage = isShill = true; }


                else if (false) { } // TODO: add


                if (inputHandler.OnKeyPress(Keys.Tab)) isPaused = inMenu = false;

                menuCodeExecuted = true;
            }

            if (inputHandler.OnKeyPress(Keys.Tab) && menuCodeExecuted == false)
            {
                Debug.WriteLine("NOW IN MENU");
                isPaused = true;
                inMenu = true;
            }

            // If In Menu


            if (!isPaused)
            {
                if (inputHandler.IsKeyDown(Keys.W) || inputHandler.IsKeyDown(Keys.Up))
                    mainchara.GoUp();
                else if (inputHandler.IsKeyDown(Keys.S) || inputHandler.IsKeyDown(Keys.Down))
                    mainchara.GoDown();
                else if (inputHandler.IsKeyDown(Keys.D) || inputHandler.IsKeyDown(Keys.Right))
                    mainchara.GoRight();
                else if (inputHandler.IsKeyDown(Keys.A) || inputHandler.IsKeyDown(Keys.Left))
                    mainchara.GoLeft();
                if (inputHandler.IsKeyDown(Keys.Enter))
                    mainchara.Fire(ref bullets);
            }

            // Update Enemys and Bullets, e.g. deleting them where necessary
            /*List<Enemy> toBeRemoved = new List<Enemy>();
            foreach (Enemy e in enemies)
            {
                e.Update(ref bullets);
                if (e.IsDestroyed) toBeRemoved.Add(e);
            }
            enemies.RemoveAll(e => toBeRemoved.Contains(e));*/
            if (!isPaused)
            {
                List<Bullet> bulletsToBeRemoved = new List<Bullet>();
                foreach (Bullet b in bullets)
                {
                    b.Move();
                    if (b.IsDestroyed) bulletsToBeRemoved.Add(b);
                }
                bullets.RemoveAll(b => bulletsToBeRemoved.Contains(b));

                // Handling Collisions
                /* If the Word Enemys collide with the bullet, they are destroyed.
                 * If the Word Enemys or their Bullets collide with the player, a life is lost.
                 * */
                collisionTimer.StartTime();

                foreach (Bullet bullet in bullets)
                {
                    if (bullet.isPlayersBullet)
                    {
                        foreach (Enemy enemy in enemies)
                        {
                            if (IsCollision(enemy.Borders, bullet.position))
                            {
                                enemy.IsDestroyed = true;
                                bullet.IsDestroyed = true;
                                score += enemy.points;
                            }
                        }
                    }
                    else
                    {
                        if (IsCollision(mainchara.Borders, bullet.position))
                        {
                            if (DateTime.Now - lifeLostTime > TimeSpan.FromSeconds(AfterLifeLostImmunityTime))
                            {
                                Debug.WriteLine("Player loses life!");
                                lives--;
                                lifeLost = true;
                                lifeLostTime = DateTime.Now;
                                isPaused = true;
                                mainchara.topleft.X = -10000; // Just to prevent loss of lives
                                mainchara.topleft.Y = -10000;
                                break;
                            }
                            else { } // Do nothing if still in regeneratory period

                        }
                    }
                }
                // Remove The 
                List<Enemy> tbr = new List<Enemy>();
                foreach (Enemy e in enemies)
                {
                    e.Update(ref bullets);
                    if (e.IsDestroyed) tbr.Add(e);
                }
                enemies.RemoveAll(e => tbr.Contains(e));

                collisionTimer.EndTime();
                Debug.WriteLine($"Collision Detection -> Time This Run {collisionTimer.GetLastDuration()}");
                Debug.WriteLine($"Collision Detection -> Average Time  {collisionTimer.GetAverageDuration()}");
            }

             if (isPaused && lifeLost)
            {
                if (DateTime.Now - lifeLostTime > TimeSpan.FromSeconds(LifeLostScreenDuration))
                {
                    isPaused = lifeLost = false;
                    mainchara.topleft = MainCharacterStartingPosition;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            spriteBatch.Begin();

            if (usesBackgroundImage && !isPaused)
            {
                if (isShill)
                    spriteBatch.Draw(shillBackgroundImage, new Vector2(0, 0), Color.White);
                else spriteBatch.Draw(codeBackgroundImage, new Vector2(0, 0), Color.White);
            }



            Debug.WriteLine("In Draw() -- spriteBatch begun");
            Debug.WriteLine($"In Draw() -> enemies.Count = {enemies.Count}");
            Debug.WriteLine($"In Draw() -> bullets.Count = {bullets.Count}");
            Debug.WriteLine($"In Draw() -> player coords = ({mainchara.Borders[0].X}, {mainchara.Borders[0].Y})");
            Debug.WriteLine("Enemy coordinates as follows: ");
            foreach (Enemy enemy in enemies)
            {
                Debug.WriteLine($"\t{enemy.position}\t{enemy.direction}");
            }

            if (inMenu && isPaused) menu.Draw(spriteBatch, isShill);


            // TODO: Add your drawing code here
            if (lives == 0) // if game over
            {
                spriteBatch.DrawString(Consolas12SF, "GAME OVER!", new Vector2(250, 200), Color.White);
                spriteBatch.DrawString(Consolas12SF, $"Points: {score}", new Vector2(250, 220), Color.Red);
                spriteBatch.DrawString(Consolas12SF, $"Level:  {level}", new Vector2(250, 240), Color.Red);

                isPaused = true; // Otherwise score count can change
            }
            else
            {
                if (isPaused && !lifeLost)
                {
                    Debug.WriteLine("In Draw -> isPaused");
                    spriteBatch.DrawString(Consolas12SF, "PAUSED", new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2,
                        GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2), Color.White);
                }
                else if (isPaused && lifeLost)
                {
                    spriteBatch.DrawString(Consolas12SF, "YOU DIED", new Vector2(250, 250), Color.White);
                    spriteBatch.DrawString(Consolas12SF, $"{lives} lives left", new Vector2(250, 270), Color.White);
                }
                else
                {
                    Debug.WriteLine("In Draw -> isNotPaused");
                    mainchara.Draw(spriteBatch);
                    foreach (Enemy enemy in enemies) enemy.Draw(spriteBatch);
                    foreach (Bullet bullet in bullets) bullet.Draw(spriteBatch);
                }

                spriteBatch.DrawString(Consolas12SF, $"Score: {score.ToString("X8")}", new Vector2(10, 10), Color.Red);
                //spriteBatch.DrawString(Consolas12SF, $"n. Enemies {enemies.Count}", new Vector2(10, 50), Color.Red);
                spriteBatch.DrawString(Consolas12SF, $"Level {Convert.ToString(level, 2).PadLeft(8, '0')}", new Vector2(10, 30), Color.Red);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
