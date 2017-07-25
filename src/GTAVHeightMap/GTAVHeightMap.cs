using System;
using System.Drawing;
using System.IO;

namespace GTAVHeightMap
{
    public class GTAVHeightMap
    {
        private const float HighestPoint = 805.1942138671875f;

        private const float Game1X = 1972.606f;
        private const float Game1Y = 3817.044f;
        private const float Map1Lng = 2528;
        private const float Map1Lat = 1502;
        private const float Game2X = -1154.11f;
        private const float Game2Y = -2715.203f;
        private const float Map2Lng = 1497;
        private const float Map2Lat = 3658;
        
        private readonly Bitmap _heightMapBitmap;
        
        public GTAVHeightMap(Stream heightMapImageFileStream)
        {
            using (heightMapImageFileStream)
                _heightMapBitmap = new Bitmap(heightMapImageFileStream);
        }

        private static Tuple<int, int> TranslateGameToImageCoordinate(float x, float z)
        {
            var imageXConverted = Map1Lng + (x - Game1X) * (Map1Lng - Map2Lng) / (Game1X - Game2X);
            var imageYConverted = Map1Lat + (z - Game1Y) * (Map1Lat - Map2Lat) / (Game1Y - Game2Y);
            
            return new Tuple<int, int>((int)imageXConverted, (int)imageYConverted);
        }
        
        public float GetHeightAtCoordinate(float x, float z)
        {
            var translatedCoordinates = TranslateGameToImageCoordinate(x, z);
            var translatedX = translatedCoordinates.Item1;
            var translatedY = translatedCoordinates.Item2;

            if (translatedX < 0)
                translatedX = 0;
            else if (translatedX > _heightMapBitmap.Width)
                translatedX = _heightMapBitmap.Width;

            if (translatedY < 0)
                translatedY = 0;
            else if (translatedY > _heightMapBitmap.Height)
                translatedY = _heightMapBitmap.Height;

            var pixelAtCoordinate = _heightMapBitmap.GetPixel(translatedX, translatedY).GetBrightness();

            return pixelAtCoordinate * HighestPoint;
        }
    }
}