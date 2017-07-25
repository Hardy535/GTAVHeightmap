using System;
using System.IO;

namespace GTAVHeightMap
{
    public class Program
    {
        // Height map from: https://www.reddit.com/r/GTAV/comments/2e30dh/gta_v_heightmap/
        private const string HeightMapFilePath = "Resources/HeightMap.png";
        
        public static void Main(string[] args)
        {
            var xCoord = float.Parse(args[0]);
            var yCoord = float.Parse(args[1]);

            float heightAtCoordinate;
            using (var gtavHeightMap = new GTAVHeightMap(File.OpenRead(HeightMapFilePath)))
                heightAtCoordinate = gtavHeightMap.GetHeightAtCoordinate(xCoord, yCoord);

            Console.WriteLine($"Height at coördinate X: {xCoord}, Y: {yCoord} is: {heightAtCoordinate}");
            Console.ReadLine();
        }
    }
}
