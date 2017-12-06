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
    public static class Mapcreator
    {
        public static Texture2D[] maptexture = new Texture2D[5];
        public static void createmap(Texture2D texture, PenumbraComponent penumbra)
        {
            penumbra.Hulls.Clear();
            penumbra.Lights.Clear();
            penumbra.Lights.Add(Player.light);
            Random rnd = new Random();
            Player.color = new Color(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));


            Map.width = texture.Width + 2;
            Map.height = texture.Height + 2;

            Map.tile = new int[Map.width][];
            for (int i = 0; i < Map.width; i++)
            {
                Map.tile[i] = new int[Map.height];
            }

            Color[] rawData = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(rawData);

            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    int i = 0;
                    Color c = rawData[y * texture.Width + x];
                    if (c.R == 255 & c.G == 255 & c.B == 255) { i = 0; }
                    if (c.R == 0 & c.G == 0 & c.B == 0) { i = 1; }
                    if (c.R == 255 & c.G == 0 & c.B == 0) { Player.x = x; Player.y = y; }
                    if (c.R == 0 & c.G == 255 & c.B == 0) {i = 2; }
                    if (c.R == 0 & c.G == 0 & c.B == 255) { i = 3; }
                    Map.tile[x + 1][y + 1] = i;
                    //Map.tile[x + 1][texture.Height - y] = i;
                }
            }

            List<Vector2> list = new List<Vector2>();
            for (int x = 0; x < Map.width; x++)
            {
                for (int y = 0; y < Map.height; y++)
                {
                    if (Map.tile[x][y] == 1)
                    {
                        if (Map.tile[x + 1][y] != 1) { list.Add(new Vector2(x + 0.5f, y)); }
                        if (Map.tile[x][y - 1] != 1) { list.Add(new Vector2(x, y - 0.5f)); }
                        if (Map.tile[x - 1][y] != 1) { list.Add(new Vector2(x - 0.5f, y)); }
                        if (Map.tile[x][y + 1] != 1) { list.Add(new Vector2(x, y + 0.5f)); }
                    }
                }
            }
            while (list.Count > 0)
            {
                List<Vector2> hulllist = new List<Vector2>();
                Vector2 pos = list[0];
                Vector2 op = list[0];
                int dr = 0;
                hulllist.Add(pos * 32);
                for (int i = 0; i < list.Count;)
                {
                    if (list[i].X == pos.X + 1 & list[i].Y == pos.Y& dr!=2)
                    {
                        dr = 1;
                        op = pos;
                        pos = list[i];
                        list.Remove(op);
                        i = 0;
                    }
                    else if (list[i].X == pos.X + 0.5f & list[i].Y == pos.Y - 0.5f)
                    {
                        if (pos.Y == op.Y)
                        {
                            dr = 2;
                            hulllist.Add(new Vector2(pos.X + 0.5f, pos.Y) * 32);
                        }
                        else
                        {
                            dr = 1;
                            hulllist.Add(new Vector2(pos.X, pos.Y-0.5f) * 32);
                        }
                        //dr = 0;
                        op = pos;
                        pos = list[i];
                        list.Remove(op);
                        i = 0;
                    }
                    else if (list[i].X == pos.X & list[i].Y == pos.Y - 1&dr != 1)
                    {
                        dr = 2;
                        op = pos;
                        pos = list[i];
                        list.Remove(op);
                        i = 0;
                    }
                    else if (list[i].X == pos.X - 0.5f & list[i].Y == pos.Y - 0.5f)
                    {
                        if (pos.Y == op.Y)
                        {
                            dr = 2;
                            hulllist.Add(new Vector2(pos.X - 0.5f, pos.Y) * 32);
                        }
                        else
                        {
                            dr = 1;
                            hulllist.Add(new Vector2(pos.X, pos.Y - 0.5f) * 32);
                        }
                        //dr =0;
                        op = pos;
                        pos = list[i];
                        list.Remove(op);
                        i = 0;
                    }
                    else if (list[i].X == pos.X - 1 & list[i].Y == pos.Y & dr != 2)
                    {
                        dr = 1;
                        op = pos;
                        pos = list[i];
                        list.Remove(op);
                        i = 0;
                    }
                    else if (list[i].X == pos.X - 0.5f & list[i].Y == pos.Y + 0.5f)
                    {
                        if (pos.Y == op.Y)
                        {
                            dr = 2;
                            hulllist.Add(new Vector2(pos.X - 0.5f, pos.Y) * 32);
                        }
                        else
                        {
                            dr = 1;
                            hulllist.Add(new Vector2(pos.X, pos.Y + 0.5f) * 32);
                        }
                        //dr = 0;
                        op = pos;
                        pos = list[i];
                        list.Remove(op);
                        i = 0;
                    }
                    else if (list[i].X == pos.X & list[i].Y == pos.Y + 1 & dr != 1)
                    {
                        dr = 2;
                        op = pos;
                        pos = list[i];
                        list.Remove(op);
                        i = 0;
                    }
                    else if (list[i].X == pos.X + 0.5f & list[i].Y == pos.Y + 0.5f)
                    {
                        if (pos.Y == op.Y)
                        {
                            dr = 2;
                            hulllist.Add(new Vector2(pos.X + 0.5f, pos.Y) * 32);
                        }
                        else
                        {
                            dr = 1;
                            hulllist.Add(new Vector2(pos.X, pos.Y + 0.5f) * 32);
                        }
                        //dr = 0;
                        Vector2 p = pos;
                        pos = list[i];
                        list.Remove(p);
                        i = 0;
                    }
                    else
                    {
                        i++;
                    }

                }
                list.Remove(pos);
                Hull hull = new Hull(hulllist.ToArray());
                penumbra.Hulls.Add(hull);
            }
        }

        static Vector2[] getneibours(int x, int y)
        {
            List<Vector2> list = new List<Vector2>();
            if (Map.tile[x + 1][y] == 1 & neighboring_air(x + 1, y)) { list.Add(new Vector2(x + 1, y)); }
            if (Map.tile[x][y - 1] == 1 & neighboring_air(x, y - 1)) { list.Add(new Vector2(x, y - 1)); }
            if (Map.tile[x - 1][y] == 1 & neighboring_air(x - 1, y)) { list.Add(new Vector2(x - 1, y)); }
            if (Map.tile[x][y + 1] == 1 & neighboring_air(x, y + 1)) { list.Add(new Vector2(x, y + 1)); }
            return list.ToArray();
        }

        static bool neighboring_air(int x, int y)
        {
            bool b = false;
            if (Map.tile[x + 1][y] == 0) { b = true; }
            if (Map.tile[x][y - 1] == 0) { b = true; }
            if (Map.tile[x - 1][y] == 0) { b = true; }
            if (Map.tile[x][y + 1] == 0) { b = true; }
            return b;
        }
    }
}
