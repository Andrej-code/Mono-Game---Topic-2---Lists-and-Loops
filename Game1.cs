using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Mono_Game___Topic_2___Lists_and_Loops
{
    enum Screen
    {
        Intro, Game
    }
 

    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState mouseState;
        MouseState previousMouseState;
        Random generator;
        Rectangle window;

        Texture2D spaceBackgroundTextrue;

        List<Texture2D> textures;
        List<Rectangle> planetRect;
        List<Texture2D> planetTexture;

        float seconds;
        float respawnTime;

        Screen screen;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            screen = Screen.Intro;
        
            window = new Rectangle(0,0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            generator = new Random();
            planetRect = new List<Rectangle>();
            textures = new List<Texture2D>();
            planetTexture = new List<Texture2D>();

            // Planets are 25 x 25
            for (int i = 0; i < 25; i++)
            {
                planetRect.Add(new Rectangle(generator.Next(0, window.Width - 25), generator.Next(0, window.Height - 25), 25, 25));
                
            }


            seconds = 0f;
            respawnTime = 3f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spaceBackgroundTextrue = Content.Load<Texture2D>("Images/space_background");

            for (int i = 1; i <= 13; i++)
            {
                textures.Add(Content.Load<Texture2D>("Images/16-bit-planet" + i));
            }

            for(int i = 0; i < planetRect.Count; i++)
            {
                planetTexture.Add(textures[generator.Next(textures.Count)]);

            }


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            if(screen == Screen.Intro)
            {

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    screen = Screen.Game;
                }

            }

            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(seconds > respawnTime)
            {
                // Creates planet Rectangle
                planetRect.Add(new Rectangle(generator.Next(0, window.Width - 25), generator.Next(0, window.Height - 25), 25, 25));

                // Creates planet Texture
                planetTexture.Add(textures[generator.Next(textures.Count)]);

                seconds = 0f;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                for(int i = 0; i < planetRect.Count; i++)
                {
                    if (planetRect[i].Contains(mouseState.Position))
                    {
                        planetRect.RemoveAt(i);
                        planetTexture.RemoveAt(i);
                        i--;
                    }
                }
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();



            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(spaceBackgroundTextrue, window, Color.White);

            }
            if (screen == Screen.Game)
            {
                _spriteBatch.Draw(spaceBackgroundTextrue, window, Color.White);


                //foreach(Rectangle planet in planetRect)
                //{
                //    _spriteBatch.Draw(planetTexture[0], planet, Color.White);
                //}

                for (int i = 0; i < planetRect.Count; i++)
                {

                    _spriteBatch.Draw(planetTexture[i], planetRect[i], Color.White);

                }
            }
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
