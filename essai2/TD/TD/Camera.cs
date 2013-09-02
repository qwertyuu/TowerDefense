using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TD
{
    class Camera
    {
        public Matrix viewMatrix;
        public Vector2 position = Vector2.Zero;
        public int mapWidth;
        public int mapHeight;
        const int treshold = 3;
        int speed = 1000;
        int maxX = GraphicsDeviceManager.DefaultBackBufferWidth - treshold;
        int minX = treshold;
        int maxY = GraphicsDeviceManager.DefaultBackBufferHeight - treshold;
        int minY = treshold;

        public void Update(MouseHandler mouse, GameTime gametime, InGameUI uI)
        {
            if (mouse.position.X > maxX)
                position.X += (float)(speed * gametime.ElapsedGameTime.TotalSeconds);

            else if (mouse.position.X < minX)
                position.X -= (float)(speed * gametime.ElapsedGameTime.TotalSeconds);

            if (mouse.position.Y > maxY)
                position.Y += (float)(speed * gametime.ElapsedGameTime.TotalSeconds);

            else if (mouse.position.Y < minY)
                position.Y -= (float)(speed * gametime.ElapsedGameTime.TotalSeconds);

            if (position.X + GraphicsDeviceManager.DefaultBackBufferWidth >= mapWidth)
                position.X = mapWidth - GraphicsDeviceManager.DefaultBackBufferWidth;

            else if (position.X < 0)
                position.X = 0;

            if (position.Y + (GraphicsDeviceManager.DefaultBackBufferHeight - uI.textBounds[0].Height) >= mapHeight)
                position.Y = mapHeight - (GraphicsDeviceManager.DefaultBackBufferHeight - uI.textBounds[0].Height);

            else if (position.Y < 0)
                position.Y = 0;

            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }

        public static Texture2D CreateCircle(int radius)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(Game1.graphics.GraphicsDevice, outerRadius, outerRadius);

            Color[,] data = new Color[outerRadius , outerRadius];

            // Colour the entire texture transparent first.
            int dimention = data.GetLength(0) / 2;
            for (int y = -dimention; y < dimention; y++)
            {
                for (int x = -dimention; x < dimention; x++)
                {
                    if (Math.Pow(x,2) + Math.Pow(y,2) <= Math.Pow(radius, 2))
                    {
                        data[x + dimention, y + dimention] = Color.White;
                    }
                }
            }
            Color[] data2 = new Color[data.Length];
            int index = 0;
            foreach (var item in data)
            {
                data2[index] = item;
                index++;
            }

            //// Work out the minimum step necessary using trigonometry + sine approximation.
            //double angleStep = 5f / radius;

            //for (double angle = 0; angle < Math.PI * 4; angle += angleStep)
            //{
            //    // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
            //    int x = (int)Math.Round(radius + radius * Math.Cos(angle));
            //    int y = (int)Math.Round(radius + radius * Math.Sin(angle));

            //    data[y * outerRadius + x] = Color.White;
            //}
            texture.SetData(data2);
            return texture;
        }

        public Camera()
        {
            mapWidth = Map.map.GetLength(0) * Cell.size;
            mapHeight = Map.map.GetLength(1) * Cell.size;
        }
    }
}
