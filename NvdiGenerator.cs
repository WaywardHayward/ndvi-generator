using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ndvi
{
    public class NdviGenerator
    {
        private readonly string _redFilePath;
        private readonly string _nirFilePath;

        public NdviGenerator(string redFile, string nirFile)
        {
            _redFilePath = redFile;
            _nirFilePath = nirFile;
        }

        /// <summary>
        /// Generates a nvdi image from the red and nir images with the data encoded in the RGB channel.
        /// </summary>
        /// <returns>The nvdi image with the NVDI encoded rgb Red being low and Green being High</returns>
        public Image<Rgba32> Generate()
        {
            var redImage = Image.Load<Rgba32>(File.OpenRead(_redFilePath));
            var nirImage = Image.Load<Rgba32>(File.OpenRead(_nirFilePath));
            var ndviImage = new Image<Rgba32>(redImage.Width, redImage.Height);

            for (int y = 0; y < redImage.Height; y++)
            {
                var redRowSpan = redImage.GetPixelRowSpan(y);
                var nirRowSpan = nirImage.GetPixelRowSpan(y);
                var nvdiRowSpan = ndviImage.GetPixelRowSpan(y);
                for (int x = 0; x < redImage.Width; x++)
                {
                    var redValue = redRowSpan[x].R;
                    var nirValue = nirRowSpan[x].R;
                    var ndviValue = (byte)((nirValue - redValue) * byte.MaxValue / (nirValue + redValue));
                    nvdiRowSpan[x] = GetNvdiColor(ndviValue);
                }
            }

            return ndviImage;
        }

        /// <summary>
        /// Gets the color for the nvdi pixel value.
        /// </summary>
        /// <returns>The nvdi color on a red to green scale</returns>
        private Rgba32 GetNvdiColor(byte ndviValue)
        {            
            var mappedRedValue = (ndviValue > byte.MaxValue / 2 ? 1 - 2 * (ndviValue - byte.MaxValue / 2) / byte.MaxValue : 1.0) * byte.MaxValue;
            var mappedGreenValue = (ndviValue > byte.MaxValue / 2 ? 1.0 : 2 * ndviValue / byte.MaxValue) * byte.MaxValue;
            var mappedBlueValue = 0;
            var ndviColor = new Rgba32((int)mappedRedValue, (int)mappedGreenValue, (int)mappedBlueValue);
            return ndviColor;
        }
    }
}