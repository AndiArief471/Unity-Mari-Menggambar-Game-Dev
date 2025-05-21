// You wake up in a mysterious forest.

// A path splits in four directions.

// ->Choices

// ==Choices==
// + [Follow the glowing lights] -> Good
// + [Venture into the dark cave] -> Bad
// + [Wander aimlessly] -> Repeat

// ==Good==
// Good Choices...
// You got the good end
// -> END

// ==Bad==
// .....
// Not a good choices
// You got a bad end
// -> END

// ==Repeat==
// You loop back to the same spot.
// Choose again?
// -> Choices

    // #speaker Grin
    // Hello, traveler. Welcome to our village.
    
    // #speaker Grin
    // I am the elder who watches over this land.
    
    // #speaker Player
    // Goodbye, elder.
    
-> intro

=== intro ===
#speaker Player
Where are we, Grin?

#speaker Grin
Looks like the edge of the Forgotten Woods. You sure this is the way?

#speaker Player
Not exactly... but my compass stopped working an hour ago.

#speaker Grin
Figures. Well, what now?

...


*   [Ask grin for advice] 
    -> ask_grin
*   [Look around the area]
    -> look_around
*   [Head deeper into the woods]
    -> go_deeper
    
=== look_around ===
#speaker Player
Let's check our surroundings first.

#speaker Grin
Good idea. Maybe we'll find some clue—or a threat.

-> END

=== ask_grin ===
#speaker Player
What do you think we should do?

#speaker Grin
I say we move. Standing still is just asking to be eaten by shadows.

-> END

=== go_deeper ===
#speaker Player
No use waiting. Let's move deeper.

#speaker Grin
Brave or foolish, I'm still with you.

-> END