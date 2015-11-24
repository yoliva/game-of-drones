1-	All the required nuget packages are included in the zip file.

2-	Run the following command in the Package Manager Console in order to seed the default data (rules, and fake players):
   	update-database -verbose

3-	back-end (ASP.NET WEB API 2):
	url: http://localhost:31349/api/v1/
		. Matches (matches/)
			-create/
			-matchStats/
			- winner/{matchId}
			- getAll/
			- match/{matchId}
			- evalRound/
		. Rules (rules/):
			-getAll/
			-updateDefault/{ruleId}
			-getCurrent/
			-create/
		. Players (players/):
			-allPlayers/
			-{playerId}
			-stats/{playerName}
			-statsComparisson/{player1Name}/{player2Name}

4-	front-end (AngularJS v1.3.15)
	-Start Page
	-Stats Page
	-Configuration Page
	-Game Page
	-Finish Page