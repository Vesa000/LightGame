using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using Microsoft.Xna.Framework.Audio;
namespace lightgame
{
    public static class Play
    {
        public static SoundEffect goalsound;
        public static SoundEffect teleportsound;

        public static bool mainmenu = true;
        public static bool escmenu = false;
        public static int level = 0;
        
        public static KeyboardState state = Keyboard.GetState();
        public static KeyboardState oldstate = Keyboard.GetState();
        public static MouseState mouseState = Mouse.GetState();
        public static MouseState oldmouseState = Mouse.GetState();

        public static Light[] balllight = new Light[500];
        public static Vector2[] ball = new Vector2[500];
        public static Vector2[] ballvel = new Vector2[500];
        public static int lightnumber = 0;
        public static Vector2 dragstart = new Vector2(0, 0);

        public static void game(PenumbraComponent penumbra)
        {
            oldstate = state;
            oldmouseState = mouseState;

            state = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (mainmenu)
            {
                if (mouseState.LeftButton == ButtonState.Released & oldmouseState.LeftButton == ButtonState.Pressed)
                {
                    if (mouseState.Position.X > 200 & mouseState.Position.X < 600 & mouseState.Position.Y > 120 & mouseState.Position.Y < 150)
                    {
                        mainmenu = false;
                        escmenu = false;
                        level = 0;
                        Mapcreator.createmap(Mapcreator.maptexture[level], penumbra);
                        teleportsound.Play();
                    }
                    if (mouseState.Position.X > 200 & mouseState.Position.X < 600 & mouseState.Position.Y > 160 & mouseState.Position.Y < 190)
                    {
                        Mouse.SetPosition(mouseState.Position.X, 135);
                    }
                }
            }
            else
            {//notmainmenu
                if (escmenu)
                {
                    if (mouseState.LeftButton == ButtonState.Released & oldmouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.Position.X > 200 & mouseState.Position.X < 600 & mouseState.Position.Y > 120 & mouseState.Position.Y < 150)
                        {
                            escmenu = false;
                        }
                        if (mouseState.Position.X > 200 & mouseState.Position.X < 600 & mouseState.Position.Y > 160 & mouseState.Position.Y < 190)
                        {
                            Mapcreator.createmap(Mapcreator.maptexture[level], penumbra);
                            teleportsound.Play();
                            escmenu = false;
                        }
                        if (mouseState.Position.X > 200 & mouseState.Position.X < 600 & mouseState.Position.Y > 200 & mouseState.Position.Y < 230)
                        {
                            mainmenu = true;
                        }
                    }
                }
                else
                {//notescmenu
                    if (Player.y > Map.height + 100) { Mapcreator.createmap(Mapcreator.maptexture[level], penumbra);teleportsound.Play(); }
                    if (Player.x > 0 & Player.x < Map.width & Player.y> 0 & Player.y< Map.height)
                    {
                        if (Map.tile[(int)Player.x][(int)Player.y] == 2)
                        {
                            goalsound.Play();
                            level++;
                            if (level > 4)
                            {
                                mainmenu = true;
                            }
                            else
                            {
                                Mapcreator.createmap(Mapcreator.maptexture[level], penumbra);
                            }
                        }
                    }
                    if(mouseState.LeftButton == ButtonState.Pressed&oldmouseState.LeftButton == ButtonState.Released)
                    {
                        dragstart = mouseState.Position.ToVector2();
                    }
                    if (mouseState.LeftButton == ButtonState.Released & oldmouseState.LeftButton == ButtonState.Pressed)
                    {
                        if(dragstart!=new Vector2(0,0))
                        {
                            if (lightnumber > 499) { lightnumber = 0; }

                            Random rnd = new Random();
                            balllight[lightnumber] = new PointLight();
                            balllight[lightnumber].Scale = new Vector2(500f);
                            balllight[lightnumber].Color = new Color(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                            balllight[lightnumber].Radius = 16;
                            balllight[lightnumber].Intensity = 1f;
                            balllight[lightnumber].ShadowType = ShadowType.Solid;
                            penumbra.Lights.Add(balllight[lightnumber]);

                            ball[lightnumber] = new Vector2(Player.x,Player.y-1);
                            ballvel[lightnumber] = mouseState.Position.ToVector2()-dragstart;
                            ballvel[lightnumber] *= 0.001f;

                            lightnumber++;
                        }
                    }
                    for(int i = 0;i<500;i++)
                    {
                        if(ball[i]!=new Vector2(0,0))
                        {
                            ballvel[i].Y+= 0.025f;
                            if (ball[i].X + ballvel[i].X - 1f > 0 & ball[i].X + ballvel[i].X + 1f < Map.width & ball[i].Y + ballvel[i].Y - 1f > 0 & ball[i].Y + ballvel[i].Y + 2f < Map.height)
                            {
                                if (Map.tile[(int)(ball[i].X+ballvel[i].X)][(int)(ball[i].Y+ballvel[i].Y)] == 1)
                                {
                                    ballvel[i].X *= 0.75f;
                                    ballvel[i].Y *= -0.75f;
                                }
                                if (Map.tile[(int)(ball[i].X + ballvel[i].X)][(int)(ball[i].Y + ballvel[i].Y + 1f)] == 1)
                                {
                                    ballvel[i].X *= 0.75f;
                                    ballvel[i].Y -= 0.025f;
                                    ballvel[i].Y *= -0.75f;
                                }
                                if (Map.tile[(int)(ball[i].X + ballvel[i].X + 0.5f)][(int)(ball[i].Y + ballvel[i].Y)] == 1&ballvel[i].X>0)
                                {
                                    ballvel[i].X *= -0.75f;
                                    ballvel[i].Y *= 0.75f;
                                }
                                if (Map.tile[(int)(ball[i].X + ballvel[i].X - 0.5f)][(int)(ball[i].Y + ballvel[i].Y)] == 1&ballvel[i].X < 0)
                                {
                                    ballvel[i].X *= -0.75f;
                                    ballvel[i].Y *= 0.75f;
                                }
                            }
                            ball[i] += ballvel[i];
                            balllight[i].Position = ball[i]*32;
                            balllight[i].Intensity -= 0.0005f;
                            if (balllight[i].Intensity < 0.1f)
                            {
                                balllight[i].Enabled = false;
                                ball[i] = new Vector2(0, 0);
                            }

                        }
                    }

                    if (state.IsKeyDown(Keys.Escape)) { escmenu = true; }
                    if (state.IsKeyDown(Keys.LeftShift)) { Camera.zoom *= 1.05f; }
                    if (state.IsKeyDown(Keys.LeftControl)) { Camera.zoom *= 0.95f; }
                    if (state.IsKeyDown(Keys.Space)&oldstate.IsKeyUp(Keys.Space)) { penumbra.Debug = !penumbra.Debug; }
                    if (state.IsKeyDown(Keys.Up)) { Camera.y += 5; }
                    if (state.IsKeyDown(Keys.Right)) { Camera.x -= 5; }
                    if (state.IsKeyDown(Keys.Down)) { Camera.y -= 5; }
                    if (state.IsKeyDown(Keys.Left)) { Camera.x += 5; }


                    if (state.IsKeyDown(Keys.W))
                    {
                        if (Player.x > 0 & Player.x < Map.width & Player.y + Player.movement.Y + 1 > 0 & Player.y + Player.movement.Y + 1 < Map.height)
                        {
                            if (Map.tile[(int)Player.x][(int)(Player.y + 1f)] == 1)
                            {
                                Player.movement.Y = -0.6f;
                            }
                        }
                    }
                    if (state.IsKeyDown(Keys.A)) { Player.movement.X -= 0.01f; }
                    if (state.IsKeyDown(Keys.S)) { }
                    if (state.IsKeyDown(Keys.D)) { Player.movement.X += 0.01f; }

                    if (Player.movement.X > 7) { Player.movement.X = 7; }
                    if (Player.movement.X < -7) { Player.movement.X = -7; }
                    Player.movement.X *= 0.95f;

                    Player.movement.Y += 0.025f;

                    if (Player.x + Player.movement.X - 0.5f > 0 & Player.x + Player.movement.X + 0.5f < Map.width & Player.y + Player.movement.Y - 0.5f > 0 & Player.y + Player.movement.Y + 0.5f < Map.height)
                    {
                        if (Map.tile[(int)((Player.x + Player.movement.X) - 0.5f)][(int)Player.y] == 1 & Player.movement.X < 0 || Map.tile[(int)((Player.x + Player.movement.X) + 0.5f)][(int)Player.y] == 1 & Player.movement.X > 0)
                        {
                            Player.movement.X = 0f;
                        }
                        if (Map.tile[(int)Player.x][(int)((Player.y + Player.movement.Y) + 0.5f)] == 1 & Player.movement.Y > 0 || Map.tile[(int)Player.x][(int)((Player.y + Player.movement.Y) - 0.5f)] == 1 & Player.movement.Y < 0)
                        {
                            Player.movement.Y = 0f;
                        }
                    }
                    Player.x += Player.movement.X;
                    Player.y += Player.movement.Y;

                    //Player.color.R += (byte)rnd.Next(-5,5);
                    //if (Player.color.R < 50) { Player.color.R = 50; }
                    //if (Player.color.R > 250) { Player.color.R = 250; }
                    //Player.color.G += (byte)rnd.Next(-5, 5);
                    //if (Player.color.G < 50) { Player.color.G = 50; }
                    //if (Player.color.G > 250) { Player.color.G = 250; }
                    //Player.color.B += (byte)rnd.Next(-5, 5);
                    //if (Player.color.B < 50) { Player.color.B = 50; }
                    //if (Player.color.B > 250) { Player.color.B = 250; }

                    Camera.x = Player.x * 32-16;// + 383;//fix this later
                    Camera.y = Player.y * 32-16;// + 225;
                }
            }
        }
    }
}
