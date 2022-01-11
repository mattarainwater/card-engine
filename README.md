I forgot how much I had Godot baked in, so this won't compile, but the code for the systems part is here:

Basically there's three layers:

System - all the game logic (damage, heal, draw cards, etc.)

Action - A state change that the systems operate with, no logic, just data (DrawCardsAction, DamageAction)

Model - The state of the game - also no logic, just data (Player health, cards in hand, etc.)

Your UI code or systems can use the ActionSystem class to "perform" an action, which will then get posted to all listening systems who will then perform whatever logic they're responsible for (including maybe performing more actions).

Probably the most complicated part (and the part I've messed with the most) is the difference between ability (in the model) and action (in the... actions). Basically an ability represents a serializable effect an object (usually a card) *can* have and then based on user or system input generates an action.

so a card might have "DamageAbility - 1-3 dmg - target 1 enemy" which given user and system input will boil down to "DamageAction 2 damage to enemy 2"
