# The Map

I searched for proper assets in the Asset Store built the map. By setting the Sorting Layers, I set the display order between objects shown on the map.

# Round Switch

In each round, the player can attack by either throwing a bomb or using Kamehameha. When the attack is over, switch to the other player's turn. You can refer to the file PlayerWeaponControl for detailed implementation.

# The Character

The character is designed to be Worms. In order to make the game smoother, I have made animations such as stand, hurt, and dead.

# Movement

At the beginning of each round, the players can consume their stamina and move left or right (controlled by AD). When the stamina runs out, they will not able to move anymore. The movement is realized through the files PlatformerCharacter2D and Platformer2DUserControl.

# Injured Animation

When the player gets attacked, the character will display the injured animation and the HP bar will decrease smoothly. I implemented this function through Coroutine. See the file PlayerHealthControl for details.

# Bomb

According to the angle and strength determined by the player, a bomb is fired. This function is implemented by Rigidbody's AddForce function. It is implemented in the file PlayerWeaponControl.

# Destroy Effect

When the bomb touches the ground, it will destroy its nearby ground. The destroy effect is realized in the file Destructible Sprite.

# Skills

To make the game more interesting, I added two skills. The first skill is the Kamehameha. Since it is not affected by gravity, it does not fall. When colliding with the player, it will cause damage. Additionally, players can also teleport themselves through planes. When the plane hits the ground (not the player), the player moves to the plane's location at once. In each round, the player can choose his skill in the upper right corner. The bomb is set to be the default weapon. By default. These skills are implemented by the files Hameha and Plane respectively.

# Game Restart
When either of the two players dies, the game ends and the restart button appears. Click this button to restart the game (the players spawn with full HP and the map is reloaded). See the file ResetGame for details.
