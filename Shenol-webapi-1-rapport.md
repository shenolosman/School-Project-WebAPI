# *Inlämninguppgift 1 Rapporten*


## **Vad är REST?**

Rest står för Representational state transfer och om en api bemöter alla prinsiper av REST det kallas RESTful. RESTful systemet använder mest HTTP protokelen (Verber är GET, POST, DELETE, PUT) for att kommunicera och kan använda Json, Html, Xml eller annan typ för data transfer.

Det finns några begränsningar för att bli eller kallas som REST. Och de är:

- Client-server
- Stateless
- Cacheable
- Basic UI
- Layered System
- Code On Demand


### **Webbapi är RESTful**


I denna uppgiften jag har skrivit 4 metoder att klara uppgiften. Alla metoder är oberoende varandra och det menas de kan körs seperat. 

I programmet finns metoder med hitta genom id, skapa nytt kommentar och filterar inmatat variablar. Man kan skapa nya kommeterar utan att filtera eller checka nåt annat eller man kan filterar given variablar och om det finns i databasen det hemtar ut datan till användaren eller returnerar fel meddelandet. 

Därför denna webbapi är *RESTful*.