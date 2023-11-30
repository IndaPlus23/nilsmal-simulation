using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace particles
{
    public class Particles : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D particleTexture;
        private List<Particle> particles;

        public Particles()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            particles = new List<Particle>();
            MouseState mouseState = Mouse.GetState();
            GenerateParticles(mouseState, 10000);

            base.Initialize();
        }

        private void GenerateParticles(MouseState mouseState, int count)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < count; i++)
                {
                    double[] location = new double[] { mouseState.X, mouseState.Y };
                    double[] acceleration = new double[] { 0, 9.81 };
                    int lifespan = 100;

                    Particle newParticle = new Particle(location, 50, acceleration, lifespan);
                    particles.Add(newParticle);
                }
            }
        }

        private Texture2D CreatePixelTexture(GraphicsDevice graphicsDevice)
        {
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
            Color[] colorData = new Color[] { Color.White };
            texture.SetData(colorData);
            return texture;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            particleTexture = CreatePixelTexture(GraphicsDevice);
        }

        public void Update(double timeStep)
        {
            foreach (var particle in particles)
            {
                particle.Update(timeStep);
            }
            particles.RemoveAll(particle => particle.Lifespan <= 0);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouseState = Mouse.GetState();

            // Generate particles only when the left mouse button is pressed
            GenerateParticles(mouseState, 250);

            Update(gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            Random random = new Random();

            // Draw all particles
            foreach (var particle in particles)
            {
                float randomSize = (float)(random.NextDouble() * 1) + 1;
                _spriteBatch.Draw(particleTexture, new Vector2((float)particle.Location[0], (float)particle.Location[1]), null, Color.White, (float)particle.RotationAngle, Vector2.Zero, randomSize, SpriteEffects.None, 0f);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}