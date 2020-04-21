# The Map

Find suitable resources in the Asset Store and build your own map. By setting Sorting Layer, I set the display level between objects.

# Round Switch

In each round, the player can only fire one item. When the prop launched is effective, switch to the next player. Specifically, the file corresponding to the file PlayerWeaponControl and each prop can be realized.

# The Character image

The character image is based on Worms. In order to make the game smoother, I have made animations such as standing, injury, and death.

# Movement

At the beginning of each round, the player can move left and right (AD control). Movement will deduct the endurance value, and the endurance value will not be able to move after running out. This function is realized through the files PlatformerCharacter2D, Platformer2DUserControl.

# Injured Animation

After the player is hit, the character will display the injured animation and the blood bar will decrease smoothly. I implemented this function through Coroutine, see the file PlayerHealthControl for details.

# Bomb

According to the angle and strength determined by the player, a bomb is fired. This function is implemented by Rigidbody's AddForce function. The specific content can be found in the file PlayerWeaponControl.

# Destroy effect

When the bomb touches the ground, it will destroy the nearby ground. This function is realized through the Destructible Sprite file.

# Skills

To make the game more interesting, I added two skills. The first skill is Qigong wave. The damage is the same as the accurate hit of the bomb, but it is not affected by gravity and will not fall. For balance, I stipulate that players can only launch Qigong waves in parallel. When colliding with the player, it will cause damage. In addition to attacking props, players can also teleport by throwing planes. When the plane hits the ground (not the player), teleportation is initiated. During the round, players can choose this skill in the upper right corner. If you do not choose, it will be considered as throwing a bomb this round. The above two skills are implemented by the files Hameha and Plane respectively.

# Game Restart
When any of the two players dies, the game ends and the restart button appears. Click this button to restart the game (the players spawn with full HP and the map is reloaded). See the file ResetGame for details.
