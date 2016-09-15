# DiscountOffers
.Net Core Solution to CodeEval Public Challenge 48: ["Discount Offers"](https://www.codeeval.com/public_sc/48/)

# Requirements - Windows
* [.Net Core SDK](https://www.microsoft.com/net/core#windows)
* If using an IDE, Visual Studio 2015 Premium, Pro, or Community with Update 3 & [.Net Core 1.00 VS 2015 Tooling Preview 2](https://www.microsoft.com/net/core#windows)

# Requirements - Linux (Ubuntu 16.04 Tested)
* Follow the latest instructions [here](https://www.microsoft.com/net/core#ubuntu)
* .Net Core SDK (`sudo apt-get install dotnet-dev-1.0.0-preview2-003131`)

# Running the Solution (Linux or Windows Command Line)
1. Open your command line app of choice (*cough* PowerShell *cough*) 
2. Navigate to the \src\DiscountOffers\ directory on Windows or /src/DiscountOffers on Linux
3. Type `dotnet restore` - You should see dotnet restoring all the NuGet packages. If not, troubleshoot your .Net Core install.
4. Run
  1. (Windows) Type `dotnet run .\InputSample\InputSample.txt`
  2. (Linux) Type `dotnet run ./InputSample/InputSample.txt`

# Running the Tests (Linux or Windows Command Line)
1. Open your command line app of choice (*cough* PowerShell *cough*) 
2. Navigate to the \src\DiscountOffers.Tests\ directory on Windows or /src/DiscountOffers.Tests/ on Linux
3. Type `dotnet restore` - You should see dotnet restoring all the NuGet packages. If not, troubleshoot your .Net Core install.
4. Type `dotnet test` - You should see the tests excute and the summary output.

