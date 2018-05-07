# Intro

This is the codebase for our Family Soccer Score Prediction Mangement Tool. I used this as a study project to familiarize myself with .NET Core, Entity Framework. The project is structerized in a standard .NET Core 2 layout and uses Razor pages for rendering HTML pages.

As this runs on a small footprint server and I wanted it containerized I was forced to use the Entity Framework Provider npsql from [here](http://www.npgsql.org/efcore/index.html) to avoid 
installing MS sql server.



# How to build and run:

    > docker-compose build
    > docker-compose up



