//Elias was here =)


// kontroller för spelet tryck 1 för att köra mot ai tryck 2 för att köra 2 spelare tryck 4 för att pausa för spelet tryck 4 för att unpausa spelet 
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Karlaxel_Ekblom_TE12C
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
     
        // bakgrundsång
        Song bakgrundsång;
        // fonts
        SpriteFont Poängvissare;
        SpriteFont information;
        
       
        
        //textures
        Texture2D topbumper;
        Texture2D bottombumper;


        //introducerar tak och golv till spelet
        Vector2 tBumperpos;
        Vector2 bBumperpos;
        Vector2 p1PoängvisarePos;
        Vector2 p2PoängvisarePos;
       
        
        // bollvärden som gör så att man kan byta mellan 1 spelare och 2 spelare samt å upp information font
        bool GameMode = true;
        bool pause = false;

        // positioner till information font
        Vector2 informationPOS;
     
        // skapr nya instanser av klasserna 
        Paddlar spelare1 = new Paddlar();
        Paddlar spelare2 = new Paddlar();
        Boll bollen = new Boll();


        // storlekten blir definerad och spelet instanseras 
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
            Content.RootDirectory = "Content";




        }
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            

            //Tilldela texturer till alla objekt
            spelare1.textur = Content.Load<Texture2D>("Paddeltextur_Slutprojekt");
            spelare2.textur = Content.Load<Texture2D>("Paddeltextur_Slutprojekt");
            topbumper = Content.Load<Texture2D>("topbumper");
            bottombumper = Content.Load<Texture2D>("bottombumper");
            bollen.Bolltexture = Content.Load<Texture2D>("PongBoll");


           // fonter blir tilldelade 
            Poängvissare = Content.Load<SpriteFont>("Poängvisare");
            information = Content.Load<SpriteFont>("Pausfont");

            // startpositioner till spelare 1 / 2
            spelare1.Paddelposition.X = 50;
            spelare1.Paddelposition.Y = graphics.GraphicsDevice.Viewport.Height / 2 - (spelare1.Paddlarnahöjd / 2);
            spelare2.Paddelposition.X = graphics.GraphicsDevice.Viewport.Width - 50 - (spelare2.Paddlarnabredd);
            spelare2.Paddelposition.Y = graphics.GraphicsDevice.Viewport.Height / 2 - (spelare2.Paddlarnahöjd / 2);

            //startpositioner för poängvisaren 
            p1PoängvisarePos.X = 200;
            p1PoängvisarePos.Y = 30;
            p2PoängvisarePos.X = 850;
            p2PoängvisarePos.Y = 30;
            
            // positioner till information fonten
            informationPOS.X = 50;
            informationPOS.Y = 720;
         
            // tilldella startpositioner för bollen 
            bollen.Bollposition.X = (1024 / 2) - bollen.bredd / 2;
            bollen.Bollposition.Y = (768 / 2) - bollen.höjd / 2;

            // tilldelar startpositioner för tak och golv
            tBumperpos.Y = 0;
            tBumperpos.X = 0;
            bBumperpos.X = 0;
            bBumperpos.Y = 768 - 25;


            // tilldelar musik och spela bakgrundmusik
            bakgrundsång = Content.Load<Song>("Castlevania NES-05 - Heart of Fire (Dracula Main Castle BGM) [myfreemp3.eu]");
            MediaPlayer.Play(bakgrundsång);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

        }
        // random varibel för AI hastighet
        private Random RY = new Random();

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
 
            // un pausar spelet 
            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                bollen.Hastighet = 8.0f;
                spelare1.paddelhastighet = 20;
                spelare2.paddelhastighet = 20;
                pause = false;
            }
            // paus knapp när för spelet
            else if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                bollen.Hastighet = 0.0f;
                spelare2.paddelhastighet = 0;
                spelare1.paddelhastighet = 0;
                pause = true;
            
            }

            // Knappar som gör att man kan köra en / två spelare
            // byter till en spelare
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                GameMode = !GameMode;
                
            }
            // byter till 2 spelare modet 
            else if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                GameMode = false;
            }

           
            // byter till en spelare (mot ai)
            if (GameMode == true)
            {

                // Randomiser AI hastighet 
                float ARY = Math.Abs(bollen.Bollposition.Y - spelare2.Paddelposition.Y);
                float AIY = RY.Next((int)ARY);
                
                // lägger till antalet pixlar paddel kan röra sig
                if (AIY < 4)
                {
                    AIY += 6;
                }
                
                if ( spelare2.Paddelposition.Y < bollen.Bollposition.Y)
                {
                 spelare2.Paddelposition.Y += ARY ;
                }

                
                if (spelare2.Paddelposition.Y > bollen.Bollposition.Y)
                {
                    spelare2.Paddelposition.Y -= ARY;
                }
             
            }


            if (GameMode == false)
            {
                
            }
           

            
            // funktioner som updaterar bland annat bollens postion och ser till att paddlarna inte hamnar utanför skärmen
            PlayerInput();
            spelare1.Update();
            spelare2.Update();
            bollen.update();
           

            // vid kollsion med paddel 1
            if (kollsionmedpaddel1())
            {
                if (bollen.RiktningVänsterneråt)
                {
                    bollen.RiktningVänsterneråt = false;
                    bollen.RiktningHögerNeråt = true;
                    bollen.Kollisionljudeffekt.Play();

                }
                else if (bollen.RiktningVänsterUpåt)
                {
                    bollen.RiktningVänsterUpåt = false;
                    bollen.RiktningHögerUpåt = true;
                    bollen.Kollisionljudeffekt.Play();
                }
                bollen.Hastighet = bollen.Hastighet + 0.4f;
            }

            // kollision med paddel2
            if (kollísionmedpadel2())
            {
                if (bollen.RiktningHögerNeråt)
                {
                    bollen.RiktningHögerNeråt = false;
                    bollen.RiktningVänsterneråt = true;
                    bollen.Kollisionljudeffekt.Play();
                }
                else if (bollen.RiktningHögerUpåt)
                {
                    bollen.RiktningHögerUpåt = false;
                    bollen.RiktningVänsterUpåt = true;
                    bollen.Kollisionljudeffekt.Play();
                }
                bollen.Hastighet = bollen.Hastighet + 0.4f;

            }

      

            
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            // ritar ut paddlar samt bollen 
            spelare1.Draw(spriteBatch);
            spelare2.Draw(spriteBatch);
            bollen.Draw(spriteBatch);
          
            // rita tak och golv
            spriteBatch.Draw(topbumper, tBumperpos, Color.White);
            spriteBatch.Draw(bottombumper, bBumperpos, Color.White);

            // ritar ut fonts 
            spriteBatch.DrawString(Poängvissare, bollen.Spelare1_Poäng.ToString(),p1PoängvisarePos,Color.Green);
            spriteBatch.DrawString(Poängvissare, bollen.Spelare2_Poäng.ToString(), p2PoängvisarePos, Color.Green);

            // spelet pausas och fonten ritas ut 
            if ( pause == true)
            {
                spriteBatch.DrawString(information, "press 3 to pause the game, press 4 to unpaus the game, press 2 if you want to play multiplayer, press 1 to play vs AI", informationPOS, Color.Green);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void PlayerInput()
        {

            // kontroller för spelare 1
            if (Keyboard.GetState(spelare1.spelarnummer).IsKeyDown(Keys.W))
            {
                spelare1.Paddelposition.Y -= spelare1.paddelhastighet;
            }
            else if (Keyboard.GetState(spelare1.spelarnummer).IsKeyDown(Keys.S))
            {
                spelare1.Paddelposition.Y += spelare1.paddelhastighet;
            }

            // kontroller för spelare 2
            if (Keyboard.GetState(spelare1.spelarnummer).IsKeyDown(Keys.Up))
            {
                spelare2.Paddelposition.Y -= spelare2.paddelhastighet;
            }
            else if (Keyboard.GetState(spelare1.spelarnummer).IsKeyDown(Keys.Down))
            {
                spelare2.Paddelposition.Y += spelare2.paddelhastighet;
            }

        }



        //funktion för kollision med paddel 1
        public bool kollsionmedpaddel1()
        {
            if (bollen.Bollposition.Y >= spelare1.Paddelposition.Y && bollen.Bollposition.X > spelare1.Paddelposition.X && bollen.Bollposition.X < (spelare1.Paddelposition.X + spelare1.Paddlarnabredd) && bollen.Bollposition.Y < (spelare1.Paddelposition.Y + spelare1.Paddlarnahöjd))
            {
                return true;

            }
            else
                return false;
        }

        // funktion för kollision med paddel 2
        public bool kollísionmedpadel2()
        {
            if (bollen.Bollposition.Y >= spelare2.Paddelposition.Y && bollen.Bollposition.X > spelare2.Paddelposition.X && bollen.Bollposition.X < (spelare2.Paddelposition.X + spelare2.Paddlarnabredd) && bollen.Bollposition.Y < (spelare2.Paddelposition.Y + spelare2.Paddlarnahöjd))
            {
                return true;

            }
            else
                return false;


            // viktigt lära sig cast inom c / c++ / c# / Java
            // strut det kan vara bra att lära sig 

        }

    }
}

