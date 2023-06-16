Applicazione BE (Solution .NET 6 WebApi):

	La soluzione implementa due controller: "Auth" per la gestione dell'autenticazione e "Company" per la gestione CRUD delle aziende
	
	La lista delle credenziali di mockup per effettuare l'accesso è scolpita nella variabile "_users", all'interno del servizio "Auth"
	
	La lista delle aziende di mockup è scolpita nella variabile "_companies", all'interno del servizio "Company"
	
	L'autenticazione avviene tramite JwtSecurityTokenHandler, con controllo a livello Middleware.
	Tale controllo è previsto per tutti i metodi che nei controller sono marcati con attributo "Auth"
	
	La proprietà "Expires" del token viene valorizzata di default, nel caso in cui il client non comunichi esplicitamente una durata differente, a 5 minuti
	
	I servizi "Auth" e "Company" vengono configurati a runtime come Singleton: Tale approccio permette di lasciare invariati i valori per le variabili utili
	all'autenticazione utente e alle aziende in lista ad ogni nuova richiesta da parte del client
	
	Istruzioni per l'esecuzione:
		- APRIRE CON VISUAL STUDIO IL FILE "Movesion Challenge\Code\BE\MovesionChallengeWebApi\MovesionChallengeWebApi.sln"
		- COMPILARE ED ESEGUIRE L'APPLICAZIONE
