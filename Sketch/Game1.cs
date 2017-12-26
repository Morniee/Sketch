using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sketch
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sketch _sketch;
        Circle _head, _lowerBody, _upperBody, _leftEye, _rightEye;
        Triangle _hat;
        Line _stick;

        const int WINDOW_WIDTH = 200;
        const int WINDOW_HEIGHT = 600;
        const float BODY_WIDTH_MULTIPLIER = 1.5f;
        const int EYE_RADIUS = 5;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
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
            InitializeShapes();
            AddShapesToSketch();
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
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
           
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            _sketch.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void InitializeShapes()
        {
            _head = new Circle(new Vector2(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 3), 35, Color.White, spriteBatch);
            _upperBody = new Circle(new Vector2(WINDOW_WIDTH / 2, _head.CenterPoint.Y + _head.Radius + _head.Radius * BODY_WIDTH_MULTIPLIER),
                                    _head.Radius * BODY_WIDTH_MULTIPLIER,
                                    Color.White,
                                    spriteBatch);
            _lowerBody = new Circle(new Vector2(WINDOW_WIDTH / 2, _upperBody.CenterPoint.Y + _upperBody.Radius + _upperBody.Radius * BODY_WIDTH_MULTIPLIER),
                                    _upperBody.Radius * BODY_WIDTH_MULTIPLIER,
                                    Color.White,
                                    spriteBatch);
            _leftEye = new Circle(new Vector2(WINDOW_WIDTH / 2 - EYE_RADIUS * 2, _head.CenterPoint.Y - EYE_RADIUS * 2),
                                  EYE_RADIUS,
                                  Color.Red,
                                  spriteBatch);
            _rightEye = new Circle(new Vector2(WINDOW_WIDTH / 2 + EYE_RADIUS * 2, _head.CenterPoint.Y - EYE_RADIUS * 2),
                                   EYE_RADIUS,
                                   Color.Red,
                                   spriteBatch);
            _hat = new Triangle(new Vector2(_head.CenterPoint.X - _head.Radius, _head.CenterPoint.Y - _head.Radius + _head.Radius / 5),
                                new Vector2(_head.CenterPoint.X + _head.Radius, _head.CenterPoint.Y - _head.Radius + _head.Radius / 5),
                                new Vector2(_head.CenterPoint.X, _head.CenterPoint.Y - _head.Radius * 2),
                                Color.Black,
                                spriteBatch);
            _stick = new Line(new Vector2(_lowerBody.CenterPoint.X - _lowerBody.Radius / 2, _lowerBody.CenterPoint.Y + _lowerBody.Radius),
                              new Vector2(_hat.Point3.X + _lowerBody.Radius, _hat.Point3.Y),
                              5,
                              Color.IndianRed,
                              spriteBatch);
        }

        private void AddShapesToSketch()
        {
            _sketch = new Sketch();
            _sketch.GeometricVisuals.Add(_head);
            _sketch.GeometricVisuals.Add(_upperBody);
            _sketch.GeometricVisuals.Add(_lowerBody);
            _sketch.GeometricVisuals.Add(_leftEye);
            _sketch.GeometricVisuals.Add(_rightEye);
            _sketch.GeometricVisuals.Add(_hat);
            _sketch.GeometricVisuals.Add(_stick);
        }
    }
}
