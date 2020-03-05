# BowlingGame
**4 Projects:**
* BowlingGame
* BowlerApp
* BowlingCommon
* BowlingGameTester

**BowlingGame -- Bowling Engine:** 
A functionally complete bowling engine for simulating a 10 pin game.  Is a class library that can be used with any front-end. 

***NOTE:*** *If used with a web application, a middleware layer should be employed to provide data transformation view models of the data.  ie. json.  Also, I have set the engine up in a way that makes it easy to wrap API calls in REST endpoints to return the json.*

**BowlerApp -- Console Bowling Front-End:** 
A simple menu based front-end to highlight the use of the engine.  Utilizes a Bowler metaphor to play the game.  *NOTE: this is only an example front-end.  Any user interface can be used.*

**BowlingCommon -- Common Shared Classes:**  These are common classes that are potentially useful for clients of the Bowling Engine.

**BowlingGameTest -- Unit Tests:**  Provides for the basic Bowling Engine unit testing.  

***NOTE:*** *I have only recently begun using unit tests in my career.  As such, I am only testing what I consider to be essential functionality and only in what may be considered a rudimentary way.*
