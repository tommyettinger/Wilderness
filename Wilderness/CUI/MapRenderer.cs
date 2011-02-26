using System;
using System.Collections.Generic;
using System.Text;

using Wilderness.Environment;

namespace Wilderness.CUI
{
    class MapRenderer
    {
        public static ConsoleColor[] availableColors =
        {
            ConsoleColor.Black, ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.DarkGreen,
            ConsoleColor.DarkCyan, ConsoleColor.DarkBlue, ConsoleColor.DarkMagenta, ConsoleColor.DarkGray,
            ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan,
            ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.Gray, ConsoleColor.White
        };
        public MapRenderer()
        {
            Console.Title = "Wilderness";
            Console.CursorVisible = false;
            currentMap = new LocalMap(10, 10,
              "####  ####" +
              "#  ####  #" +
              "#        #" +
              "##      ##" +
              " #      # " +
              " #      # " +
              "##      ##" +
              "#        #" +
              "#  ####  #" +
              "####  ####");
        }

        public void RenderLocalMap()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Tile curr = new Tile();
            try
            {
                for (int i = 0; i < currentMap.width; i++)
                {
                    for (int j = 0; j < currentMap.height; j++)
                    {
                        Console.SetCursorPosition(i, j);
                        curr = currentMap.getVisibleTileAt(i, j);
                        Console.ForegroundColor = availableColors[curr.colors >> 4];
                        Console.BackgroundColor = availableColors[curr.colors & 0x0F];
                        Console.Write(curr.shown);
                    }
                }
                Console.ResetColor();
            }
            catch (IndexOutOfRangeException)
            {
                Console.ResetColor();
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("ARRAY DISPLAY ERROR");
            }
        }

        public LocalMap currentMap { get; set; }
    }
}
