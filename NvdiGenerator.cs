using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace nvdi
{
    public class NvdiGenerator
    {
        private readonly string _redFilePath;
        private readonly string _nirFilePath;

        public NvdiGenerator(string redFile, string nirFile)
        {
            _redFilePath = redFile;
            _nirFilePath = nirFile;
        }

        /// <summary>
        /// Generates a nvdi image from the red and nir images with the data encoded in the Red RGB channel.
        /// </summary>
        /// <returns>The nvdi image with the NVDI encoded in the red channel</returns>
        public Image<Rgba32> Generate()
        {
            var redImage = Image.Load<Rgba32>(File.OpenRead(_redFilePath));
            var nirImage = Image.Load<Rgba32>(File.OpenRead(_nirFilePath));
            var nvdiImage = new Image<Rgba32>(redImage.Width, redImage.Height);

            for (int y = 0; y < redImage.Height; y++)
            {
                var redRowSpan = redImage.GetPixelRowSpan(y);
                var nirRowSpan = nirImage.GetPixelRowSpan(y);
                var nvdiRowSpan = nvdiImage.GetPixelRowSpan(y);
                for (int x = 0; x < redImage.Width; x++)
                {
                    var redValue = redRowSpan[x].R;
                    var nirValue = nirRowSpan[x].R;
                    nvdiRowSpan[x] = new Rgba32((byte)((nirValue - redValue) * 255 / (nirValue + redValue)));
                }
            }

            return nvdiImage;
        }

    }
}