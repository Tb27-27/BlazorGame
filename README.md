# Guild Shop

This is a blazor game I made for a frameworks assignment.
I wanted to make a game where you are a guild shopkeeper where you interact with guild adventurers, 
you draw a handful of cards,
then you use those cards to fill in up to 1 armour slot, 1 weapon slot and 3 consumable slots.
the cards can also be draw 2 additional cards or other things like that interact with your own hand instead of the guild adventurers.

Gameplay loop:

A guild adventurer walks into your shop, tells the quest he's going on, for example:
"I am going to kill a goblin burrow".
Burrows are dark so he needs something that lights the way, eg. a lantern or torch.
Goblins are quick, so he needs light or medium armor.
The burrows are quite narrow so he will need a short weapon, like a short sword or mace.
You give him the correct items to finish his quest, you get a 100% completion rate.
You give him the wrong items to finish his quest, you get a lower completion rate, down to 25%.
When the adventurer succeeds he returns, gives you materials, gold and experience points.
When the adventurer fails he does not return.
The next guild adventurer comes in and you repeat the transactions.
This happens 3 times and then the day is over and you see a log of what happened that day.
Then a new day happens and you start over again.