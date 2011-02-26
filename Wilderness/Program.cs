using System;
using System.Collections.Generic;
using System.Text;

using Wilderness.Environment;
using Wilderness.CUI;

namespace Wilderness
{
    /**
     * Modified from nrkn 's SimpleRL ( https://github.com/nrkn/SimpleRL )
     */
    class Program
    {
        static readonly List<string> Map = new List<string>{
      "####  ####", 
      "#  ####  #", 
      "#        #", 
      "##      ##", 
      " #      # ", 
      " #      # ", 
      "##      ##", 
      "#        #", 
      "#  ####  #", 
      "####  ####" 
    };
        static int playerX = 2, playerY = 2;
        static MapRenderer mr = new MapRenderer();
        static void Main()
        {
            ConsoleKey key;
            Tile playerTile = new Tile('@', 0x80, new Guid("11111111111111111111111111111111"));
            mr.currentMap.addTileAt(playerX, playerY, playerTile, true);
            mr.RenderLocalMap();

            //keep processing key presses until the player wants to quit
            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Q)
            {
                //put the square they were standing on before back to normal
                mr.currentMap.removeTileAt(playerX, playerY, 0);
                //change the player's location if they pressed an arrow key
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MovePlayer(playerX, playerY - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        MovePlayer(playerX, playerY + 1);
                        break;
                    case ConsoleKey.LeftArrow:
                        MovePlayer(playerX - 1, playerY);
                        break;
                    case ConsoleKey.RightArrow:
                        MovePlayer(playerX + 1, playerY);
                        break;
                }
                //now add the player to the new square
                mr.currentMap.addTileAt(playerX, playerY, playerTile, true);
                mr.RenderLocalMap();
            }
        }

        static void MovePlayer(int newX, int newY)
        {
            //don't move if the new square isn't empty
            if (mr.currentMap.isPassable(newX, newY) != true) return;

            //set the new position
            playerX = newX;
            playerY = newY;
        }
    }
}