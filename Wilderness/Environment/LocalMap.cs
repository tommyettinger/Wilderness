using System;
using System.Collections.Generic;
using System.Linq;

namespace Wilderness.Environment
{
    public struct Tile
    {
        public char shown;
        public byte colors;
        public Guid contentID;

        public Tile(char show)
        {
            shown = show;
            colors = 0xF0;
            contentID = new Guid("00000000000000000000000000000000");
        }
        public Tile(char show, Guid data)
        {
            shown = show;
            colors = 0xF0;
            contentID = data;
        }
        public Tile(char show, byte color, Guid data)
        {
            shown = show;
            colors = color;
            contentID = data;
        }

    }
    class LocalMap
    {
        public Tile defaultTile = new Tile('a', 0xF0, new Guid("00000000000000000000000000000000"));
        public LocalMap(int width, int height)
        {
            this.width = width;
            this.height = height;
            mapData = new List<Tile>[width,height];
            try
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        this.mapData[i, j] = new List<Tile> { defaultTile };
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        this.mapData[i, j] = new List<Tile> { new Tile(' ', new Guid("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00")) };
                    }
                }
            }
        }

        public LocalMap(int width, int height, List<Tile>[,] mapData)
        {
            this.width = width;
            this.height = height;
            this.mapData = mapData;
        }

        public LocalMap(int width, int height, char[,] mapChars)
        {
            this.width = width;
            this.height = height;
            this.mapData = new List<Tile>[width, height];
            try
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        this.mapData[i, j] = new List<Tile> {new Tile(mapChars[i, j]) };
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        this.mapData[i, j] = new List<Tile>{new Tile(' ', new Guid("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00"))};
                    }
                }
            }
        }

        public LocalMap(int width, int height, string mapString)
        {
            this.width = width;
            this.height = height;
            this.mapData = new List<Tile>[width, height];
            try
            {
                char[] mapChars = mapString.ToCharArray();
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        this.mapData[j, i] = new List<Tile>{new Tile(mapChars[(width*i)+j])};
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        this.mapData[i, j] = new List<Tile>{new Tile(' ', new Guid("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00"))};
                    }
                }
            }
        }

        public Tile getVisibleTileAt(int x, int y)
        {
            Tile ret = new Tile();
            try
            {
                ret = mapData[x, y][0];
            }
            catch (IndexOutOfRangeException)
            {
                ret.contentID = new Guid("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFF00");
            }
            return ret;
        }

        public int addTileAt(int x, int y, Tile tileToAdd, bool onTop = true)
        {
            try
            {
                if (onTop == true)
                {
                    mapData[x, y].Insert(0, tileToAdd);
                    return 0;
                }
                else
                {
                    mapData[x, y].Add(tileToAdd);
                    return mapData[x, y].Count;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
        }

        public int removeTileAt(int x, int y, int z)
        {
            try
            {
                mapData[x, y].RemoveAt(z);
                return mapData[x, y].Count;
            }
            catch (ArgumentOutOfRangeException)
            {
                return -2;
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
        }

        public int width { get; private set; }

        public int height { get; private set; }

        public List<Tile>[,] mapData { get; private set; }

        public bool isPassable(int x, int y)
        {
            foreach (Tile t in mapData[x, y])
            {
                if (t.shown != ' ' && t.shown != '.')
                    return false;
            }
            return true;
        }
    }
}
