# Use Case : RPG

## Actors

	EnemyAI

## Precondition

	Player must be within the Aggressive Vicinity.
	
## Post-condition

	Enemy must either die or player leave the Vicinity and return to Roaming.
	
## Normal Flow

	1. Enemy roaming their designated vicinity.
	2. Enemy found player within the Aggressive Vicinity.
	3. Enemy start attacking player.
	4. Enemy must end combat by either kill the player or the player kill the AI.
	
## ALternate Flow
	
	A. Player leave the Aggressive Vicinity before either Enemy or Player die.
	B. Enemy AI will resume roaming their designated Vicinity.
