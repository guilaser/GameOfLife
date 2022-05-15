# Game Of Life

I denna uppgift ska ni ta fram en variant av [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life).

## Regler

Simuleringen sker på ett tvådimensionellt rutnät. Rutorna (cellerna) kan vara på eller av. Brädets utseende förändras enligt följande regler:

+ En cell föds om den har exakt tre grannar. Som grannar räknas direkt intill-liggande rutor horisontellt, lodrätt eller diagonalt.
+ En cell dör om den har färre än två grannar (isolering) eller om den har fler än tre grannar (trängsel).
+ I övrigt förblir cellen oförändrad. 

Observera att huruvida en cell skall förändras skall beräknas innan någon cell förändras. Man måste med andra ord räkna ut hela brädet innan man går över till nästa tur (generation).

## Kod
Uppgift ska genomföras grafiskt i Windows Forms. Till hjälp får ni ett befintligt projekt med färdigställda metoder som ni kan ha nytta av. I projektet finns en form med en timer..så du behöver inte lägga till en timer. 

Följande metoder finns:

+ gameTimer_Tick :denna metoden anropas när en viss tidsintervall har gått. Tidsintervallen kan ändras och själva timern (gameTimer) kan startas och stoppas.
+ GameOfLifeForm_Paint: denna metoden används för att måla spelet. Här behöver du ha koden som ritar de olika celler. Förmodligen vill du måla om spelet vid varje tidsintervall. Metoden kan inte anropas direkt, utan metoden **Invalidate()** måste anropas, vilket i sin tur anropar GameOfLifeForm_Paint().   

Genom hela uppgiften har ni nytta av [2D arrayer i C#](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays). Kolla länken för att se hur dessa används.

## Arbetsgång

Denna är en svår uppgift och det rekommenderas att ni arbetar i följande steg till en början:

1: Repetera hur ni ritar en ifylld rektangel i Windows Forms och skriv koden för att göra detta.   
2: Skapa en 2D array som representerar alla celler i spelet. En array av int är lämplig, och att ni representerar en levande cell med en 1, en död cell med en 0. Fyll array:n med test data.  
3: Skriv koden måste rita ut array:n på skärmen. Tips: ni har nytta av en nästlade for-sliinga här.  
4: Fyll arrayn med slumpmässiga 1:or och 0:or.

Nu har du en bra början till spelet. Vad behövs mer? 

+ Ni behöver byta från en generation till nästa. Ett sätt att genomföra beräkningarna är att ni har två 2-D arrayer - en av dessa används för att behålla nästa generationen av celler. 
+ Ni behöver implementera reglerna som gäller för när en cell lever eller dör.

## Förslag på förbättringar

Bli ni klara kan ni förstås förbättra spelet - här är några förslag:

+ Lägg till en startscreen - spelet börjas inte förrän spelaren klickar på en knapp.
+ Låt användaren välja vilka positioner är levande genom att klicka på skärmen eller annan metod.

## Till slut
Fungerar ditt spel borde du ser några av GOLs kända mönster, t.ex. block och loaf, som blir stabila genom spelets gång. Sedan finns oscillatorer som växlar mellan olika former. 

Lycka till!



"# GameOfLife" 
"# GameOfLife" 
