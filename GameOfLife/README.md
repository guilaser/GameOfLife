# Game Of Life

I denna uppgift ska ni ta fram en variant av (Conway's Game of Life)[https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life].



## Regler.

Simuleringen sker p� ett tv�dimensionellt rutn�t. Rutorna (cellerna) kan vara p� eller av. Br�dets utseende f�r�ndras enligt f�ljande regler:

+ En cell f�ds om den har exakt tre grannar. Som grannar r�knas direkt intill-liggande rutor horisontellt, lodr�tt eller diagonalt.
+ En cell d�r om den har f�rre �n tv� grannar (isolering) eller om den har fler �n tre grannar (tr�ngsel).
+ I �vrigt f�rblir cellen of�r�ndrad. 

Observera att huruvida en cell skall f�r�ndras skall ber�knas innan n�gon cell f�r�ndras. Man m�ste med andra ord r�kna ut hela br�det innan man g�r �ver till n�sta tur (generation).

