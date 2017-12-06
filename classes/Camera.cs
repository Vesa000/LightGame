using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace lightgame
{
    public static class Camera
    {
        public static float zoom = 1.234836f;
        public static float x = 0f;
        public static float y = 0f;
        //public static Matrix matrix(Viewport vp)
        //{
        //    Vector2 screencenter = new Vector2(vp.Width / 2, vp.Height / 2);
        //    Vector2 translation = -new Vector2(x, y) + screencenter;
        //    return Matrix.CreateTranslation(translation.X, translation.Y, 0) * Matrix.CreateScale(zoom) * Matrix.CreateTranslation(screencenter.X, screencenter.Y, 0);
        //}
        public static Matrix matrix(Viewport vp)
        {
            Vector2 screencenter = new Vector2(vp.Width / 2, vp.Height / 2);
            Vector2 translation = -new Vector2(x, y) + screencenter;
            return
                Matrix.CreateTranslation(new Vector3(-new Vector2(x, y), 0.0f)) *
                //Matrix.CreateTranslation(new Vector3(-screencenter, 0.0f)) *
                Matrix.CreateRotationZ(0f) *
                Matrix.CreateScale(zoom, zoom, 1) *
                Matrix.CreateTranslation(new Vector3(screencenter, 0.0f));
        }
    }
}
