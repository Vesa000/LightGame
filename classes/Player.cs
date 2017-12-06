using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
namespace lightgame
{
    public static class Player
    {
        public static float x = 0;
        public static float y = 0;
        public static Vector2 movement = new Vector2(0, 0);
        public static Color color = new Color(255, 20, 20);
        public static Light light = new PointLight
        {
            Scale = new Vector2(1000f), // Range of the light source (how far the light will travel)
            Radius = 32,
            Intensity = 1F,
            ShadowType = ShadowType.Solid // Will not lit hulls themselves
        };
    }
}
