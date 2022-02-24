# BlazorPowerBI

Il progetto consiste in una semplice applicazione Blazor WebAssembly in grado di incorporare, direttamente all’interno di un componente Razor, un report di PowerBi senza far uso del tag iframe autogenerato dalla piattaforma di reportistica, ma bensì sfruttando le API messe a disposizione da quest’ultima. Questa soluzione consente di avere maggior controllo e pulizia del codice che viene incorporato all’interno del progetto.

## Permessi e Autenticazione
Il seguente paragrafo è un breve riepilogo di come è stato impostato e configurato l’ambiente all’interno del quale andremo ad operare, partendo dal presupposto che vi sia un report di PowerBi già esistente.
#### Azure Active Directory
Grazie alle funzionalità offerte da Azure AD abbiamo la possibilità di centralizzare la gestione di autorizzazione e permessi che vogliamo fornire alla nostra applicazione. Per raggiungere tale scopo si è dunque operato nel seguente modo:
-	È stata creata una nuova registrazione associata all’app. Da qui è stato dunque creato un nuovo client secret, che in combinazione al tenant id e client id consentirà all’applicazione Blazor di autenticarsi. ![client secret](/docs/secrets.png)
-	Dopo di che l’applicazione è stata autorizzata a chiamare le API esposte da PowerBi. ![permessi API](/docs/api-permissions2.png)
-	In fine, è stato creato un gruppo, in modo tale da semplificare la gestione degli utenti. ![gruppo](docs/group.png)

#### PowerBi
Lato PowerBi, invece, al fine di completare il giro di autenticazione, sono state eseguite le seguenti operazioni:
-	Dai Workspaces è stato selezionato il report che si desidera collegare all’applicazione e premuto il tasto Access. ![PowerBi configurazione accesso](/docs/access.png)  
-	Nella schermata appena apertasi è stato aggiunto come amministratore il gruppo creato in precedenza.  ![PowerBi nuovo amministratore](/docs/access2.png)  

## Codice
Si andrà ora ad illustrare i passi fondamentali che sono stati affrontati durante l’implementazione dell’applicazione fino al raggiungimento del risultato desiderato.
Blazor ci permette di eseguire una parte del codice, generato a partire da quello C# definito da noi, direttamente all’interno del browser. La nostra soluzione è dunque divisa in due progetti: BlazorPowerBi.Server e BlazorPowerBi.Client, quest’ultimo sarà appunto la parte che verrà eseguita lato browser.

#### BlazorPowerBi.Server
Una volta creata la nuova applicazione Blazor WebAssembly, il primo passo è fare in modo che essa sia in grado di autenticarsi mediante Azure AD. Come già anticipato nel paragrafo precedente, per raggiungere tale scopo andremo ad utilizzare il client secret, il client id e il tenant id di Azure AD.  Aggiunti quindi in configurazione lato server i rispettivi parametri, andiamo a creare un nuovo controller PowerBiController utilizzarli al suo per recuperare un token valido da Azure Ad. ![recuperare il token](/docs/controller-getToken.png)

Sempre all’interno del controller andiamo a realizzare anche un metodo che restituisca al client le informazioni base relative al report che desideriamo recuperare da PowerBi, anche queste salvate in configurazione. ![recupeare informazioni report](/docs/controller-getReport.png)

#### BlazorPowerBi.Client
Spostandoci ora lato client, ciò che dobbiamo fare è recuperare tutte le informazioni relative al report e mostrarle all’interno di una pagina web. 

Continuando a procedere con ordine andiamo dunque subito a realizzare una pagina Razor che vada a richiamare il servizio PowerBi/config/{ReportName} esposto dal server. ![pagina razor](/docs/index.png)

Il componente Razor ComponentReport, che viene visualizzato all’interno della nostra pagina, avrà al suo interno un semplice div che farà da contenitore per il nostro report e svolgerà principalmente due funzioni principali:
-	Appena il componente viene inizializzato richiama il metodo del controller per recuperare il token da Azure Ad.
-	Una volta completato il proprio caricamento, invoca una funzione javascript denominata drawBi, alla quale verranno passati come parametri il token di Azure AD, l’id del report che desideriamo recuperare e le altre informazioni relative al DataSet.
 ![componente razor](/docs/component.png)

Per poter integrare completamente ed agevolmente il report di PowerBi, è necessario installare un [pacchetto Nuget]( https://github.com/Microsoft/PowerBI-JavaScript), il quale implementa al suo interno una libreria javascript per interfacciarsi con il portale.

Ciò che viene effettuato a questo punto all’interno del nostro javascript è innanzitutto autenticarsi e recuperare un token da PowerBi. Questo è possibile grazie al token già recuperato da Azure AD e alla configurazione precedentemente effettuata su i due portali. ![js recupero token da PowerBi](/docs/power-bi-report.js2.png)

Mediante questo secondo token è possibile finalmente richiamare le API di PowerBi per recuperare interamente il report e caricarlo all’interno del nostro div contenitore e, grazie al pacchetto installato in precedenza, sarà possibile utilizzarlo come se fosse all’interno della piattaforma. ![js recupero report](/docs/power-bi-report.js3.png)

A questo punto abbiamo finalmente raggiunto il risultato desiderato, e il report è caricato e pienamente fruibile all’interno della nostra pagina. ![report](/docs/report.png)
