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
    public static class Drawing
    {
        public static Texture2D playertexture;
        public static Texture2D balltexture;
        public static Texture2D whitetexture;
        public static SpriteFont font;

        public static float fps = 0;
        public static float ups = 0;

        public static void drawgame(SpriteBatch sb, GraphicsDevice gd, Matrix matrix, PenumbraComponent penumbra, GameTime gameTime)
        {
            if (Play.mainmenu)
            {
                gd.Clear(Color.CornflowerBlue);
                sb.Begin();
                sb.Draw(Drawing.whitetexture, new Rectangle(200, 120, 400, 30), Color.Green);
                sb.DrawString(Drawing.font, "Start Game", new Vector2(250, 120), Color.White);
                sb.Draw(Drawing.whitetexture, new Rectangle(200, 160, 400, 30), Color.Red);
                sb.DrawString(Drawing.font, "Exit", new Vector2(350, 160), Color.White);
                sb.End();
            }
            else
            {
                if (Play.escmenu)
                {
                    gd.Clear(Color.CornflowerBlue);
                    sb.Begin();
                    sb.Draw(Drawing.whitetexture, new Rectangle(200, 120, 400, 30), Color.Green);
                    sb.DrawString(Drawing.font, "Continue", new Vector2(250, 120), Color.White);
                    sb.Draw(Drawing.whitetexture, new Rectangle(200, 160, 400, 30), Color.Orange);
                    sb.DrawString(Drawing.font, "Restart", new Vector2(250, 160), Color.White);
                    sb.Draw(Drawing.whitetexture, new Rectangle(200, 200, 400, 30), Color.Red);
                    sb.DrawString(Drawing.font, "Exit", new Vector2(250, 200), Color.White);
                    sb.End();
                }
                else
                {
                    penumbra.BeginDraw();
                    gd.Clear(Color.White);
                    penumbra.Transform = matrix;

                    sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, matrix);
                    for (int i = 0; i < 500; i++)
                    {
                        if (Play.ball[i] != new Vector2(0, 0))
                        {
                            sb.Draw(balltexture, new Vector2(Play.ball[i].X * 32 - 16, Play.ball[i].Y * 32 - 16), Play.balllight[i].Color);
                        }
                    }

                    sb.Draw(Drawing.playertexture, new Vector2(Player.x * 32 - 32, Player.y * 32 - 32), Player.color);
                    for (int x = 0; x < Map.width; x++)
                    {
                        for (int y = 0; y < Map.height; y++)
                        {
                            if (Map.tile[x][y] == 2)
                            {
                                sb.Draw(whitetexture, new Vector2(x * 32 - 16, y * 32 - 16), Color.Green);
                            }
                        }
                    }

                    sb.End();


                    // Draw items affected by lighting here ...
                    penumbra.Draw(gameTime);
                    if (Play.level == 0)
                    {
                        sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, matrix);
                        sb.DrawString(Drawing.font, "WASD = move", new Vector2(100, 0), Color.White);
                        sb.DrawString(Drawing.font, "Shift,Ctrl = Zoom", new Vector2(100, 50), Color.White);
                        sb.DrawString(Drawing.font, "Mouse drag = Throw balls", new Vector2(100, 100), Color.White);
                        sb.End();
                    }
                    if (Play.level == 1)
                    {
                        sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, matrix);
                        sb.DrawString(Drawing.font, "Level 2", new Vector2(100, 0), Color.White);
                        sb.End();
                    }
                    if (Play.level == 2)
                    {
                        sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, matrix);
                        sb.DrawString(Drawing.font, "Level 3", new Vector2(100, 0), Color.White);
                        sb.End();
                    }
                    if (Play.level == 3)
                    {
                        sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, matrix);
                        sb.DrawString(Drawing.font, "Level 4", new Vector2(100, 0), Color.White);
                        sb.End();
                    }
                    if (Play.level == 4)
                    {
                        sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null, matrix);
                        sb.DrawString(Drawing.font, "Level 5", new Vector2(100, 0), Color.White);
                        sb.End();
                    }

                    sb.Begin();
                    sb.DrawString(Drawing.font, "FPS: " + fps, new Vector2(0, 0), Color.White);
                    //sb.DrawString(Drawing.font, "UPS: " + ups, new Vector2(0, 30), Color.White);
                    sb.End();
                    // Draw items NOT affected by lighting here ... (UI, for example)
                }
            }
        }















        public static void drawlights(SpriteBatch sp, GraphicsDevice gd, Matrix matrix)
        {
            float sx = Player.x;
            float sy = Player.y;
            float ox = 0;
            float oy = 0;
            float nx = 0;
            float ny = 0;

            for (double a = 0; a < 2 * Math.PI; a += 0.05f)
            {
                bool found = false;
                for (float l = 0; l < 50 & !found; l += 0.1f)
                {
                    nx = sx + (float)(l * Math.Cos(a));
                    ny = sy + (float)(l * Math.Sin(a));

                    if (Map.tile[(int)Math.Floor(nx)][(int)Math.Floor(ny)] == 1) { found = true; }
                }
                //sp.Draw(playertexture, new Vector2(nx, ny));
                VertexPositionColor[] vertices = new VertexPositionColor[3];
                vertices[0] = new VertexPositionColor(new Vector3(sx * 10, sy * 10, 0), Color.Red);
                vertices[1] = new VertexPositionColor(new Vector3(ox * 10, oy * 10, 0), Color.Green);
                vertices[2] = new VertexPositionColor(new Vector3(nx * 10, ny * 10, 0), Color.Blue);

                VertexBuffer vertexBuffer = new VertexBuffer(gd, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
                vertexBuffer.SetData<VertexPositionColor>(vertices);
                gd.SetVertexBuffer(vertexBuffer);
                RasterizerState rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.None;
                gd.RasterizerState = rasterizerState;
                BasicEffect basicEffect = new BasicEffect(gd);
                basicEffect.VertexColorEnabled = true;
                basicEffect.View = matrix;

                foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    gd.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
                }

                ox = nx;
                oy = ny;
            }
        }
    }
}
