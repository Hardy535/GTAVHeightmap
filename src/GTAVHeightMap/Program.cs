using System;
using System.IO;

namespace GTAVHeightMap
{
    public class Program
    {
        private const string HeightMapFilePath = "Resources/HeightMap.png";
        
        public static void Main(string[] args)
        {
            var xCoord = float.Parse(args[0]);
            var yCoord = float.Parse(args[1]);
            
            var gtavHeightMap = new GTAVHeightMap(File.OpenRead(HeightMapFilePath));
            var heightAtCoordinate = gtavHeightMap.GetHeightAtCoordinate(xCoord, yCoord);

            Console.WriteLine($"Height at coördinate X: {xCoord}, Y: {yCoord} is: {heightAtCoordinate}");
            Console.ReadLine();
        }
    }
}
