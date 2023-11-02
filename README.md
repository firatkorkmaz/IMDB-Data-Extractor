# IMDB Data Extractor
This is a C# program that extracts movie information from a given input data file which is created by using the Internet Movie Database: www.imdb.com

## General Information
This program analyzes a given **watchlist.csv** input file with a list of movie information and extracts specific features of all the movie data such as title, year, rate and rating count, then rewrites the list in a descended order of rating counts and saves this list in a new **watchlist.txt** file.

## Technologies
This project is created with:
* Microsoft Visual Studio
  * C# Console Application

## Setup & Run
To run this project, open the **IMDBDataExtractor.sln** solution file with Visual Studio, then build and run this solution, or use C# compiler to directly compile the **Program.cs** file and run the created executable (.exe) file:
```
csc Program.cs
```
The input file related to this program is given in both the root directory and the execution working directory of this solution: **.../bin/Debug/net6.0/**
