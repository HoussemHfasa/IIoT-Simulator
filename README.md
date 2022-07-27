# Programmierprojekt: IIoT-Simulator

* Teammitglieder:
	1. Mjellma Salihi
	2. Houssem Hfasa
	3. Paul Schult
	4. Aleksandr Terekhov
* Team: 5
* Semester: Sommersemester 2022

Hier eine Kurzbeschreibung Ihres Projektes in 2-3 Sätzen

## Installation

Mit dem IIot Simulator kann man Sensoren, die beispielsweise in Industrieprojekten
eingesetzt werden, schon vor dem Einsatz testen. Es werden Sensordaten simuliert 
und an einen MQTT-Broker gesendet. Dabei kann der Benutzer Sensorgruppen erstellen,
die Sensordaten selbst erzeugen und anzeigen lassen.


Hier Anweisungen hinterlegen die fuer eine Installation notwendig sind. Diese muessen 100% vollstaendig sein

## Verwendung der Software

Die Software kann über Visual Studio geöffnet und gestartet werden.


Wie verwendet man diese Software? Welches Programm muss man starten?

## Links, Hinweise etc.

Das Programm 'IIoTSimulatorUI' beinhaltet die finale Software. 

Die Software wird über Visal Studio gestartet und der Benutzer gelangt vorerst 
auf die Startseite in der man über die Startleiste zu den Broker-Einstellungen gelangen kann.
Der Benutzer hat nun die Möglichkeit eine Verbindung zu dem Broker herzustellen.
Der Benutzer kann dann wählen ob er eine Verbindung mit dem Broker-Namen und
dem Port herstellt, oder ob er den Nutzernamen und das Passwort ebenfalls benötigt. 
Dies wird über einen Button, der einen Haken setzt, festgelegt.

Hat der Benutzer eine Broker-Verbindung hergestellt, geht es zurück in die 
Startseite in der er eine bestehende Sensorgruppe aus dem Dateiexplorer
geladen oder eine neue erstellt werden kann. 

Durch Eingabe in die Textfelder werden die Namen für den Stamm, Unterordner
und die Sensoren vergeben. Es können mehrere Stämme, Unterordner , Unterordner von
Unterordner und mehrere Sensoren erstellt werden. Bei der Sensorwahl gelangt man auf ein neues
Fenster in der man die Datenerzeugungmethode wählt. Hat man diese gewählt gelangt man auf
ein neues Fenster in der man die Werte für die Sensordaten frei wählt.
Die Sensorwerte können dann durch Klicken des Buttons 'Aktualisieren' aktualisiert werden
und so in einemm Liniengraf angezeigt werden. Man hat dann die Möglichkeit die Sensorgruppe zu speichern 
oder den Sensorwerten Fehler hinzuzufügen.

Will man Fehler hinzufügen, gibt man die Fehlerwerte ein und durch den Button
'Aktualisieren' können diese erneut angezeigt werden. Durch den Button 'Hinzufügen'
werden die Sensordaten dem Sensor hinzugefügt und man gelangt zurück auf das Fenster
in der man die Sensorgruppe erstellt.

Jetzt kann man die Sensorgruppe weiter bearbeiten oder gleich
speichern und durch den Button 'Simualtion starten', auf das Fenster der
Simulation gelangen.

In der Simualtion werden alle Sensorwerte der erstellten Sensoren angezeigt.
Man hat nun die Möglichkeit die Daten einzeln an den Broker zu senden oder
einen Zeitintervall in Sekunden festzulegen, in dem die Sensordaten
vom Programm an den Broker gesendet werden sollen. Die gesendeten Sensordaten werden 
dabei angezeigt.


//Info
Null-Referece-Exception wenn man keinen Oberordner erstellt

Erstellt man eine neue Sensorgruppe, wird eine Info-Text angezeigt
der darauf hinweist ein übergeordnetes Element auszuwählen um dem ein
untergeordnetes Element hinzuzufügen. Wird dennoch kein Element ausgewählt,
bekommt man eine Null-referece-Exception, das Programm stürzt jedoch nicht ab,
man kann mit F5 wieder zurück auf das Fenster gelangen und das Programm
weiter ausführen. -Der Fehler wurde aufgefangen und es wird eine Fehlermeldung 
ausgegeben, das Programm
gibt jedoch trotzdem diese Exception.

1. Markdown Syntax: https://docs.gitlab.com/ee/user/markdown.html
2. Git fuer Windows: https://git-scm.com/download/win
3. Nutzen Sie zur Organisation von Teamaufgaben das Kanban-Board unter Issues/Boards im gitlab!
