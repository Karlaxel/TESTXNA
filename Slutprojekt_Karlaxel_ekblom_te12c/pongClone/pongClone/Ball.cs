// kontroller för spelet tryck 1 för att köra mot ai tryck 2 för att köra 2 spelare tryck 4 för att pausa för spelet tryck 3 för att unpausa spelet 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Karlaxel_Ekblom_TE12C
{
    class Boll : Microsoft.Xna.Framework.Game
    {
        
        public SoundEffect Kollisionljudeffekt;
        public Vector2 Bollposition;
        public Texture2D Bolltexture;
        public int bredd, höjd, Spelare1_Poäng, Spelare2_Poäng;
        public float Hastighet;
        public bool RiktningVänsterneråt, RiktningHögerNeråt, RiktningVänsterUpåt, RiktningHögerUpåt;
        public int counter;

        // konstruktor för bollen
        public Boll()
        {
            Content.RootDirectory = ("Content");
            Hastighet = 8f;
            bredd = 20; höjd = 20;
            Spelare1_Poäng = 0;  Spelare2_Poäng = 0;
            RiktningVänsterneråt = true;   RiktningHögerNeråt = false;  RiktningVänsterUpåt = false; RiktningHögerUpåt = false;
            Bolltexture = null;   Bollposition = Vector2.Zero;
            Kollisionljudeffekt = Content.Load<SoundEffect>("Ljudeffekt");
            counter = 0;
        }
        // Bollen ritas ut 
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Bolltexture, Bollposition, Color.White);
        }

        // bollen updaterings funktion
        public void update()
        {
            // vänsterupåt
            if (RiktningVänsterUpåt)
            {
                Bollposition.Y -= Hastighet;
                Bollposition.X -= Hastighet;
            }

            // vänsterneråt
            if (RiktningVänsterneråt)
            {
                Bollposition.Y += Hastighet;
                Bollposition.X -= Hastighet;
            }

            // högerupåt
            if (RiktningHögerUpåt)
            {
                Bollposition.Y -= Hastighet;
                Bollposition.X += Hastighet;
            }

            // högerneråt
            if (RiktningHögerNeråt)
            {
                Bollposition.Y += Hastighet;
                Bollposition.X += Hastighet;
            }
            // rör sig vänster upåt och studsar på toppvägen
            if (RiktningVänsterUpåt && Bollposition.Y <= 0 + 25)
            {
                RiktningVänsterneråt = true;
                RiktningVänsterUpåt = false;
                Kollisionljudeffekt.Play();
            }
            // rör sig vänsterneråt och studsar på vänstra vägen
            else if (RiktningVänsterneråt && Bollposition.X <= 0)
            {
                Spelare2_Poäng = Spelare2_Poäng + 1;
                RiktningVänsterneråt = false;
                RiktningHögerNeråt = true;
                Hastighet = 0;
   

                Bollposition.X = (1024 / 2) - bredd / 2;
                Bollposition.Y = (768 / 2) - höjd / 2;
            }
            // rör sig vänsterupåt och studsar på vänstra vägen
            else if (RiktningVänsterUpåt && Bollposition.X <= 0)
            {
                
                Spelare2_Poäng = Spelare2_Poäng + 1;
                RiktningHögerUpåt = true;
                RiktningVänsterUpåt = false;
                Hastighet = 0;
            


                Bollposition.X = (1024 / 2) - bredd / 2;
                Bollposition.Y = (768 / 2) - höjd / 2;
            }

            // rör sig vänsterneråt och studsar på bottenvägen
            else if (RiktningVänsterneråt && Bollposition.Y >= 768 - 45)
            {
                RiktningVänsterUpåt = true;
                RiktningVänsterneråt = false;
                Kollisionljudeffekt.Play();
            }
            // rör sig högerupåt och studsar på högra vägen
            else if (RiktningHögerNeråt && Bollposition.X >= 1024 - bredd)
            {
                Spelare1_Poäng = Spelare1_Poäng + 1;
                RiktningVänsterneråt = true;
                RiktningHögerNeråt = false;
                Hastighet = 0;

                Bollposition.X = (1024 / 2) - bredd / 2;
                Bollposition.Y = (768 / 2) - höjd / 2;
            }
            // rör sig höger upåt och studsar på toppvägen
            else if (RiktningHögerUpåt && Bollposition.Y <= 0 + 25)
            {
                RiktningHögerNeråt = true;
                RiktningHögerUpåt = false;
                Kollisionljudeffekt.Play();
            }
            // rör sig högerneråt och studsar på bottenvägen
            else if (RiktningHögerNeråt && Bollposition.Y >= 768 - 45)
            {
                RiktningHögerUpåt = true;
                RiktningHögerNeråt = false;
                Kollisionljudeffekt.Play();
            }

            // rör sig högerupåt och träffar vänstra vägen
            else if (RiktningHögerUpåt && Bollposition.X >= 1024 - bredd)
            {
                Spelare1_Poäng = Spelare1_Poäng + 1;
                RiktningVänsterUpåt = true;
                RiktningHögerUpåt = false;
                Hastighet = 0;

                Bollposition.X = (1024 / 2) - bredd / 2;
                Bollposition.Y = (768 / 2) - höjd / 2;
            }

        }

    }

    
}
