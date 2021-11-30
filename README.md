# nvdi-generator
A simple console app which will take a bitmaps for spectral band 3 and band 4 and generate a Normalized Vegitation Difference Index (NVDI) image.

## Getting Started

Clone the repository using the following command.

    git clone https://github.com/WaywardHayward/nvdi-generator.git

Run to code using the following command.

    cd nvdi-generator
    dotnet restore
    dotnet run

The application will then prompt you for a red and a nir bitmap.
Once the application is completed, you will see the NVDI image in same directory as the red image.

