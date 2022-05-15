# Game Of Life

I denna uppgift ska ni ta fram en variant av (Conway's Game of Life)[https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life].



## Regler.

Simuleringen sker på ett tvådimensionellt rutnät. Rutorna (cellerna) kan vara på eller av. Brädets utseende förändras enligt följande regler:

+ En cell föds om den har exakt tre grannar. Som grannar räknas direkt intill-liggande rutor horisontellt, lodrätt eller diagonalt.
+ En cell dör om den har färre än två grannar (isolering) eller om den har fler än tre grannar (trängsel).
+ I övrigt förblir cellen oförändrad. 

Observera att huruvida en cell skall förändras skall beräknas innan någon cell förändras. Man måste med andra ord räkna ut hela brädet innan man går över till nästa tur (generation).

