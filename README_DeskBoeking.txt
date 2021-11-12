1. Manual:

Start de solution. 

User Form:
Als eerste form is er een User form te zien. Deze form is een MDI child form.
De form bevat een combobox met  een lijst van al bestaande users die in de database staan. 
Ook is er een textveld die het mogelijk maakt om een nieuwe user toe te voegen in de database.
Als eerste stap moet er een keuze gemaakt worden of een bestaande gebruiker wordt gekozen 
(via de combobox) of een nieuwe gebruiker worden aangemaakt.
De gebruikers hebben bewust geen paswoord om de app zo laagdrempeling mogelijk te houden.

Druk daarna in the User Form op 'Next'. Nu wordt de boeking form geopened.

Boeking form:
De User Form geeft de Usernaam en Id door aan de Boeking form via een property.
Er wordt ook een eigen event gestuurd van User from naar Boeking form: op de console 
wordt dan Usernaam, Id en een text boodschap doorgegeven van User form aan Boeking form 
('Usernaam en id doorgegeven van User form aan Boeking form (Id=1, Usernaam=jan)'. Dit event 
dient als voorbeeld en heeft geen invloed op het runnen van de app.
 
In de Boeking form zijn er 2 tabs aanwezig: 

De eerste tab bevat de mogelijkheid om een nieuwe boeking te maken, de andere toont de 
persoonlijke boekingen van de user.
Een boeking maken doet men door eerst op de kalender een datum te kiezen en daarnaa een plaats
 te kiezen in de lijst met plaatsen. De plaatsnaam zijn een string omwille van flexibiliet; in de 
app als volgt gecodeerd 'Gebouw_Verdiep_Zone_Nummer'.
Wanneer een van deze plaatsen geselecteerd wordt, toont een plattegrond waar de plaats zich bevindt. 
Ook kan een bepaalde desk "faciliteiten" hebben, bijvoorbeeld: docking station, printer, ...
Als laatste stap voor de boeking moet er op de knop 'Boek' gedrukt worden. Er kan enkel een plaats 
per boeking worden geboekt.
En een gebruiker kan meerdere keren een boeking doen voor dezelfde dag. Dit maakt het mogelijk 
dat een gebruiker een plaats boekt voor bijvoorbeeld een gast of een bezoeker. 

De tweede tab toont de persoonlijke boekingen van de user.
In de persoonlijke boekingen kunnen alle boekingen van de gebruiker worden gezien. Naast elke 
plaats staat de datum van de boeking. Boekingen in het verleden worden niet getoond.
Een boeking verwijderen kan door een boeking te selecteren en dan op de knop 'verwijder de 
boeking' te drukken.

Bezetting form:
De Bezetting form wordt geopend door in de menubalk 'Bezetting' te kiezen.
Het toont voor een bepaalde datum hoeveel plaatsen geboek werden.

Indien u een andere gebruiker wil kiezen, druk dan het 'User' menu in de parent form.


2. Overzicht opdrachttaken
1. Je koppelt het project aan een databank met minstens 3 datatabellen, waar minstens 1 een
index bevat naar een andere tabel 
> 3 tabellen: Users, Boekingen, Desks
Boekingen tabel heeft index naar Users en een index naar Desks.

2. Je maakt een ergonomische userinterface in Windows Forms
zie app
  
3. Je maak gebruikt van een Mdi-form en minstens 1 child form
ja, FormMain (=parent), children: User form, Boeking form, Bezetting form.

4. Je maakt minstens eenmaal gebruikt van tab-bladen:
zie Boeking form.

5. Je creeert en gebruikt minstens eenmaal een eigen event
zie FormUser.cs: method 'button1_Click' (zender)
zie FormBoeking.cs: 'UpdateEvent', method 'FormBoeking' (ontvanger)

6. Je gebruikt minstens eenmaal een lambda expressie
FormUser.cs: method 'button1_Click'
FormBoeking.cs: method 'FormBoeking_UpdateControlsTab1'

7. Je maakt gebruik van Linq
FormUser: 'button1_Click', 'FormUser_Load'
FormBoeking: 'FormBoeking_UpdateControlsTab1', 'FormBoeking_UpdateControlsTab', 
 'FormBoeking_UpdateImage', 'button1_Click', 'button2_Click'
FormBEzetting: 'FormBezetting_UpdateControls'

8. Je zort dat alle fouten op een correct manier worden afgehandeld...
FormUser.cs: 'button1_Click'

9. Alle onderdelen van het programma lopen foutloos
zie app

10. Het gebruiken van technieken niet gezien (bv API)...
FormBoeking: 'FormBoeking_UpdateImage' -> ImageConverter API.


3. Referenties:
https://stackoverflow.com/questions/7462748/how-to-run-code-when-form-is-shown
https://stackoverflow.com/questions/479329/how-to-bind-a-liststring-to-a-datagridview-control
https://stackoverflow.com/questions/9576868/how-to-put-image-in-a-picture-box-from-a-byte-in-c-sharp
https://stackoverflow.com/questions/11597373/the-specified-type-member-date-is-not-supported-in-linq-to-entities-exception
https://stackoverflow.com/questions/253843/refresh-datagridview-when-updating-data-source
https://stackoverflow.com/questions/479329/how-to-bind-a-liststring-to-a-datagridview-control
https://stackoverflow.com/questions/612689/a-generic-list-of-anonymous-class
https://blog.matrixpost.net/using-list-tuples-in-c/
https://stackoverflow.com/questions/7462748/how-to-run-code-when-form-is-shown
https://stackoverflow.com/questions/3429128/how-to-get-the-selected-date-of-a-monthcalendar-control-in-c-sharp
https://stackoverflow.com/questions/2581265/for-a-datagridview-how-do-i-get-the-values-from-each-row/45633603

https://stackoverflow.com/questions/7462748/how-to-run-code-when-form-is-shown
https://stackoverflow.com/questions/11597373/the-specified-type-member-date-is-not-supported-in-linq-to-entities-exception
https://shabdar.org/2021/09/01/store-image-in-sql-server-datab/

https://stackoverflow.com/questions/5325797/the-entity-cannot-be-constructed-in-a-linq-to-entities-query