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
    class Paddlar : Microsoft.Xna.Framework.Game
    {
        // Paddelklassen 
        public Texture2D textur;
        public Vector2 Paddelposition;
        public PlayerIndex spelarnummer;
        public int Paddlarnabredd, Paddlarnahöjd, paddelhastighet;
        
        // Paddelfunktion 
        public Paddlar()
        {
            textur = null;
            Paddelposition = Vector2.Zero;
            spelarnummer = PlayerIndex.One;
            Paddlarnabredd = 25; Paddlarnahöjd = 175; paddelhastighet = 20;
        }
        
        // funktion som gör att paddeln inte kan gå utanför skärmen 
        public void Update()
        { 
            if (Paddelposition.Y <= 25)
                Paddelposition.Y = 25;
            if (Paddelposition.Y >= 768 - 200)
                Paddelposition.Y = 768 - 200;          
        }
        
        // konstruktor för paddeln 
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(textur, Paddelposition, Color.White);
        }
    }


}
