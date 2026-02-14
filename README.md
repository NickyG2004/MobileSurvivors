
**Portfolio Page:** https://sites.google.com/view/nicholasgrossportfoliopage/developer-bio/mobile-survivors

## Tools & Technologies
| Category | Technology |
| :--- | :--- |
| **Engine** | Unity 6000.0.36f1 |
| **Input** | New Input System (Mobile Touch Optimization) |
| **UI System** | Unity UI Toolkit (Modern UXML/USS workflow) |
| **Architecture** | Strategy Pattern & Observer Pattern |

Sample_02_MobileSurvivorUpgradeSystem
Documentation

Sample Purpose: To experiment with a small-scale implementation of class inheritance and how it can serve as a defensive programming measure for creating a scalable, modular upgrade system.

Sample Scope:
Using class inheritance to set specific guidelines for future upgrade scripts. 
Understanding how to handle an ever-growing list of upgrades and how to apply them to the player with a weighted level of randomness to prevent the same upgrade multiple times in a row.

System Input: 
Creating a new upgrade script that inherits from the IUpgrade class. 
Filling out the desired logic of the upgrade behavior (if it's a stat change, or adding a new weapon)
Note: for a weapon, a WeaponData object is needed as well to store weapon stats.
Feed the scripts into the GameController in the Awake() method, where they are added to a master upgrade list to be randomly pulled from later upon level-ups.

System Output: 
The player will receive the created upgrade and will either have the player character's stats changed (longer health bar, faster movement) or the creation of a new weapon (new projectile sprites on screen)

Third Party Asset Sources: 
No use of third party asset for this project. I made everything in Aseprite.
Sample_02_MobileSurvivorUpgradeSystem
User Guide

Guide Content: How to create a new upgrade for the game.

Step Guide:
Create an Upgrade script: 
Create a new script and have it inherit from the IUpgrade class. Name the script appropriately and save it in your desired subfolder.
I.e., Name: WeaponCooldownSpeedUpgrade

Create the upgrade logic:
Inside the new script, implement all the methods and requirements of the IUpgrade class.
Then, using these inheritance tools, create the desired upgrade behavior inside the script
For weapon upgrades, the data is taken from a TryGetComponent check in the GameController, and its stats will be available for alteration when the custom upgrade method is called.

Add the New Script to the GameController
After adding the required dependencies for the weapon data, in the game controller Awake() function, add the new upgrade script to the master upgrade list.

Update Validation:
Remove or comment out the other upgrades from the Awake() method so they are not added to the master list, and then play the game until you level up and can see the new upgrade in action.
