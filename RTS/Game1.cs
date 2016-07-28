﻿using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RTS.Abstract;
using RTS.Concrete;
using RTS.Mechanics;

namespace RTS
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        private HUDControl hud;
        private GameManager manager;
        private CollisionControl collisionControl;
        private Camera2D _camera;
        private bool clicked = false;
        Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            //graphics.PreferredBackBufferHeight = 768;
            //graphics.PreferredBackBufferWidth = 1366;
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            manager = new GameManager();
            _camera = new Camera2D(GraphicsDevice.Viewport);
            collisionControl = new CollisionControl(manager);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("debug");
            textures["GoldMine"] = Content.Load<Texture2D>("GoldMine");
            textures["Worker"] = Content.Load<Texture2D>("Worker");
            textures["Worker_MINE"] = Content.Load<Texture2D>("Worker_MINE");
            textures["Fighter"] = Content.Load<Texture2D>("Fighter");
            textures["Fighter_ATTACK"] = Content.Load<Texture2D>("Fighter");
            textures["Headquarters"] = Content.Load<Texture2D>("Headquarters");
            manager.Textures = textures;
            hud = new HUDControl()
            {
                posPoint = new Vector2(0, 0),
                size = new Point(300, 100),
                spriteBatch = spriteBatch,
                spriteFont = spriteFont,
                graphicsDevice = GraphicsDevice,
                graphics = graphics,
                camera = _camera,
                manager = manager,
                currentPlayer = manager.Players.GetCurrentPlayer()
            };
            hud.init();
            _camera.OnCameraChange += hud.RedrawHud;
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            // movement
            if (keyboardState.IsKeyDown(Keys.Up))
                _camera.ChangePosition(new Vector2(0, 250) * deltaTime,false);

            if (keyboardState.IsKeyDown(Keys.Down))
                _camera.ChangePosition(new Vector2(0, 250) * deltaTime, true);

            if (keyboardState.IsKeyDown(Keys.Left))
                _camera.ChangePosition(new Vector2(250, 0) * deltaTime, false);

            if (keyboardState.IsKeyDown(Keys.Right))
                _camera.ChangePosition(new Vector2(250, 0) * deltaTime, true);



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            if (mouseState.LeftButton == ButtonState.Released&&clicked==true)
            {
                clicked = false;
            }
            if (mouseState.LeftButton == ButtonState.Pressed&&clicked==false)
            {
                clicked = true;
                manager.Container.SelectGameObjectAtPoint(mouseState.X, mouseState.Y, manager.Players.GetCurrentPlayer());
                if(manager.Container.SelectedGameObject!=null) manager.ClickableAreas.CheckAreas(mouseState.Position);
            }
            if (mouseState.RightButton == ButtonState.Pressed&&Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                manager.Container.SelectedGameObject.actionControl.AddGoPoint(new Point(mouseState.X, mouseState.Y));
            }else if (mouseState.RightButton == ButtonState.Pressed)
            {
                manager.Container.SelectedGameObject.targetCoords = new Vector2(mouseState.X,mouseState.Y);
            }

            manager.UpdateOrganisms();
            
            collisionControl.InvokeActions();
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            var viewMatrix = _camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: viewMatrix);
            manager.DrawOrganisms(spriteBatch,GraphicsDevice, spriteFont, manager.Players.GetCurrentPlayer());
            hud.DrawHUD();
            DrawRectangle(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 10, 10), Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void DrawRectangle(Rectangle coords, Color color)
        {
            var rect = new Texture2D(GraphicsDevice, 1, 1);
            rect.SetData(new[] { color });
            spriteBatch.Draw(rect, coords, color);
        }
    }
}
