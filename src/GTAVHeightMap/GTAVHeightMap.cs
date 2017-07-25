using System;
using System.Drawing;
using System.IO;

namespace GTAVHeightMap
{
    public class GTAVHeightMap : IDisposable
    {
        // Map/Game translation information
        private const float HighestPoint = 805.1956176757812f;
        
        private const float Point1GameX = 1972.606f;
        private const float Point1GameY = 3817.044f;
        private const float Point1MapX = 2528;
        private const float Point1MapY = 1502;
        
        private const float Point2GameX = -1154.11f;
        private const float Point2GameY = -2715.203f;
        private const float Point2MapX = 1497;
        private const float Point2MapY = 3658;
        
        private readonly Bitmap _heightMapBitmap;
        
        public GTAVHeightMap(Stream heightMapImageFileStream)
        {
            using (heightMapImageFileStream)
                _heightMapBitmap = new Bitmap(heightMapImageFileStream);
        }

        // Logic from: http://gta5map.glokon.org/
        private static Tuple<int, int> TranslateGameToImageCoordinate(float x, float z)
        {
            var imageXConverted = Point1MapX + (x - Point1GameX) * (Point1MapX - Point2MapX) / (Point1GameX - Point2GameX);
            var imageYConverted = Point1MapY + (z - Point1GameY) * (Point1MapY - Point2MapY) / (Point1GameY - Point2GameY);
            
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

        public void Dispose()
        {
            _heightMapBitmap?.Dispose();
        }
    }
}
