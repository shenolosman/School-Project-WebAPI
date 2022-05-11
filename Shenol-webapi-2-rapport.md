# *WebAPI Inlämninguppgift 2 Rapporten*

## Beskrivning

#### *Beskriv hur du löste att olika GeoComment versioner behövde föras in i databasen.Hade man kunnat lösa det på något annat sätt?*
    
- Jag skrev version hantering med service som ärvt från IConfigureOptions för att fatta hur många olika version tag har jag använd och det formulerar automatisk. Just den här uppgiften finns bara 2 versioner som de är 0.1 och 0.2. 

- Man kan skriva inuti Program.cs med handen version nummer och använder tag inom controller för swagger eller bara med version tag inom controller borde funka också utanför swagger kanske inte provat detta sättet.

#### *Beskriv ett annat sätt du hade kunnat versionera i stället för att använda en queryparameter. På vilket sätt hade det varit bättre/sämre för detta projekt?*

- Det finns några olika typ för versionera våra api som QueryStringApiVersionReader, HeaderApiVersionReader, MediaTypeApiVersionReader, UrlSegmentApiVersionReader. 
    
- Queryparameter och url parameter är lätt att läsa och se tydligt versionering tror jag. Med Header parameter kan missas och orsaka felaktig anrop.

#### *Ge exempel och förklaring på när man vill ha behörighetskontroll för en webbapi och när man inte vill ha det.*
    
- Jag har andvänd Authorize på en HttpPost metod för hindra någon som har inte behörighet att skriva inne på databasen.

![Authorize code sample](https://imgyukle.com/f/2022/05/11/RWaijh.png)

- Authorize används mest med PUT, POST, DELETE, PATCH men kan också används för GET anrop också att för känsliga data med behörighetskontroll.
