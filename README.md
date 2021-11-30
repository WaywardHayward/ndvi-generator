# ndvi-generator
A simple console app which will take a bitmaps for spectral band 3 and band 4 and generate a Normalized Difference Vegitation Index (NDVI) image.

More Information on the NDVI can be found [here](https://en.wikipedia.org/wiki/Normalized_Difference_Vegetation_Index).

## Getting Started

Clone the repository using the following command.

    git clone https://github.com/WaywardHayward/ndvi-generator.git

Run to code using the following command.

    cd ndvi-generator
    dotnet restore
    dotnet run

The application will then prompt you for a red and a nir bitmap.
Once the application is completed, you will see the NDVI image in same directory as the red image.

The colouring of the NDVI image is based on the following formula:

    NDVI = (NIR - RED) / (NIR + RED)

This is then converted to an RGB image using with Red representing a low NDVI value and Green representing a high NDVI value.

